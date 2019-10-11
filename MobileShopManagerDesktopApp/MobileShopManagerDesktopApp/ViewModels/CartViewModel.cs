using Caliburn.Micro;
using MobileShopManagerDesktopApp.DataAccessModel;
using MobileShopManagerDesktopApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MobileShopManagerDesktopApp.ViewModels
{
    public class CartViewModel: Screen
    {
        private ObservableCollection<ProductOrder> _ListProductOrder;
        private ProductOrder _SelectedProductOrder;
        private double _TotalPayment;
        private string _IdCustomer;
        private string _IdEmployee;
        public ProductOrder SelectedProductOrder { get => _SelectedProductOrder; set { _SelectedProductOrder = value; NotifyOfPropertyChange("SelectedProductOrder"); } }
        public ObservableCollection<ProductOrder> ListProductOrder { get => _ListProductOrder; set { _ListProductOrder = value; NotifyOfPropertyChange("ListProductOrder"); } }
        public double TotalPayment { get => _TotalPayment; set { _TotalPayment = value; NotifyOfPropertyChange("TotalPayment"); } }
        public string IdCustomer { get => _IdCustomer; set { _IdCustomer = value; NotifyOfPropertyChange("IdCustomer"); } }
        public string IdEmployee { get => _IdEmployee; set => _IdEmployee = value; }

        public CartViewModel()
        {
            TotalPayment = 0;
        }
        public void AddProductOrder(ProductOrder productOrder)
        {
            if(ListProductOrder == null)
            {
                ListProductOrder = new ObservableCollection<ProductOrder>();
            }
            foreach (ProductOrder item in ListProductOrder)
            {
                if (item.Info.Id == productOrder.Info.Id)
                {
                    item.Count += 1;
                    TotalPayment += item.PriceSale;
                    return;
                }
            }
            ListProductOrder.Add(productOrder);
            TotalPayment += productOrder.PriceSale;

        }
        public void CheckOut()
        {
            if (ListProductOrder == null)
            {
                MessageBox.Show("Chưa chọn sản phẩm", "Chú ý");
                return;
            }
            string caution = "";
            MessageBoxResult check;
            check = MessageBox.Show("Xác nhận thanh toán.", "Thông báo", MessageBoxButton.YesNo);
            if(check == MessageBoxResult.No)
            {
                return;
            }
            if(IdCustomer != null && IdCustomer != "")
            {
                Customer customer = DataAccess.USP_GetCustomerByID(IdCustomer);
                if(customer == null)
                {
                    caution = "Không tìm thấy khách hàng có mã " + IdCustomer;
                    MessageBox.Show(caution, "Chú ý");
                    return;
                }
                if(TotalPayment > 5000000)
                {
                    customer.Point += 50;
                }
                caution = "Khách hàng: " + customer.Name;
                caution += "\nĐiểm hiện tại: " + customer.Point;
                caution += "\nTổng tiền: " + TotalPayment + "đ";
                if(customer.Point >= 50)
                {
                    caution += "\nKhách hàng có muốn đổi điểm?";
                    check = MessageBox.Show(caution, "Thông báo", MessageBoxButton.YesNo);
                    if(check == MessageBoxResult.Yes)
                    {
                        TotalPayment -= TotalPayment * (customer.Point / 50) * 0.05;
                        customer.Point = customer.Point % 50;
                        caution = "Khách hàng: " + customer.Name
                            + "\nTổng tiền sau khi đổi điểm là: " + TotalPayment + "đ"
                            + "\nĐiểm hiện tại: " + customer.Point;
                    }
                }
              
                 if(check == MessageBoxResult.Yes)
                    MessageBox.Show(caution, "Thông báo", MessageBoxButton.OK);
                
                DataAccess.USP_UpdatePointCustomer(customer.Id, customer.Point);
            }
            else
            {
                caution = "Tổng tiền thanh toán: " + TotalPayment + "đ";
                MessageBox.Show(caution, "Thông báo", MessageBoxButton.OK);
            }
            if(IdCustomer == "")
            {
                IdCustomer = null;
            }
   
            DateTime DateCheckOut = DateTime.Now;
            if(IdCustomer == null || IdCustomer == "")
            {
                DataAccess.USP_InsertBillWithoutIdCustomer(IdEmployee, DateCheckOut, TotalPayment);
            }
            else
            {
                DataAccess.USP_InsertBill(IdCustomer ,IdEmployee, DateCheckOut, TotalPayment);
            }
            
            int IdBill = DataAccess.USP_GetIdBill(DateCheckOut);
            foreach (ProductOrder item in ListProductOrder)
            {
                DataAccess.USP_InsertBillDetail(IdBill, item.Info.Id, item.Count);
                int IDWareHouse = DataAccess.USP_GetIdWareHouse(item.Info.Id);
                DataAccess.USP_UpdateAmountRemaining(IDWareHouse, item.Count);
            }
            ListProductOrder = null;
            IdCustomer = null;
            TotalPayment = 0;
        }

        public void DeleteProductOrder()
        {
            foreach (ProductOrder item in ListProductOrder)
            {
                if(item.Info.Id == SelectedProductOrder.Info.Id)
                {
                    ListProductOrder.Remove(item);
                    TotalPayment -= item.PriceSale * item.Count;
                    break;
                }
            }
        }
        public void SubtractCount()
        {
            if(SelectedProductOrder.Count > 1)
            {
                SelectedProductOrder.Count--;
                TotalPayment -= SelectedProductOrder.PriceSale;
            }
        }
        public void AddCount()
        {
            int AmountRemaining = DataAccess.USP_GetAmountRemainingProductByID(SelectedProductOrder.Info.Id);
            if (AmountRemaining - SelectedProductOrder.Count - 1 < 0)
            {
                MessageBox.Show("Trong kho tạm thời đã hết sản phẩm.", "Chú ý");
                return;
            }
            SelectedProductOrder.Count++;
            TotalPayment += SelectedProductOrder.PriceSale;
        }
        public void ShowSelectedProduct()
        {
            DetailViewModel detail = new DetailViewModel(SelectedProductOrder);
            var ParentConductor = (Conductor<object>)(this.Parent);
            ParentConductor.ActivateItem(detail);
        }
    }
}
