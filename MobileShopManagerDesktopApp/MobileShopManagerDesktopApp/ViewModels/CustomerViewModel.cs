using Caliburn.Micro;
using MobileShopManagerDesktopApp.DataAccessModel;
using MobileShopManagerDesktopApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MobileShopManagerDesktopApp.ViewModels
{
    public class CustomerViewModel: Screen
    {
        private ObservableCollection<Customer> _ListCustomer;
        private Customer _SelectedCustomer;
        private string _SearchStringCustomer;
        public ObservableCollection<Customer> ListCustomer { get => _ListCustomer; set { _ListCustomer = value; NotifyOfPropertyChange("ListCustomer"); } }
        public Customer SelectedCustomer { get => _SelectedCustomer; set { _SelectedCustomer = value; NotifyOfPropertyChange("SelectedCustomer"); } }

        public string SearchStringCustomer { get => _SearchStringCustomer; set{
                _SearchStringCustomer = value;
                NotifyOfPropertyChange("SearchStringCustomer");
                if(value == "")
                {
                    ListCustomer = DataAccess.LoadListCustomer();
                }
            }
        }

        public CustomerViewModel()
        {
            ListCustomer = new ObservableCollection<Customer>();
            ListCustomer = DataAccess.LoadListCustomer();
        }
        public void SearchCustomer()
        {
            if (SearchStringCustomer != "" && SearchStringCustomer != null)
            {
                ListCustomer = DataAccess.FindCustomer(SearchStringCustomer);
            }
        }

        public void AddCustomer()
        {
            if(SelectedCustomer == null || SelectedCustomer.Id == "")
            {
                MessageBox.Show("Chưa điền đủ thông tin.", "Chú ý");
                return;
            }
            object Check = DataProvider.Ins.ExecuteScalar("Select Count(*) from Customer Where ID = '" + SelectedCustomer.Id + "'");
            if ((int)Check > 0)
            {
                MessageBox.Show("Khách hàng có ID " + SelectedCustomer.Id + " đã tồn tại.", "Chú ý");
                return;
            }
            DataAccess.InsertCustomer(SelectedCustomer);
            ListCustomer = DataAccess.LoadListCustomer();
        }
        public void DeleteCustomer()
        {
            if (SelectedCustomer != null)
            {
                DataAccess.UpdateIsDeleted("Customer", 0, SelectedCustomer.Id);
                ListCustomer.Remove(SelectedCustomer);
                SelectedCustomer = null;
            }
        }
    }
}
