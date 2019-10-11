using Caliburn.Micro;
using MobileShopManagerDesktopApp.DataAccessModel;
using MobileShopManagerDesktopApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileShopManagerDesktopApp.ViewModels
{
    public class BillViewModel: Screen
    {
        private ObservableCollection<Bill> _ListBill;
        private ObservableCollection<BillDetail> _ListBillDetail;
        private Bill _SelectedBill;
        private DateTime _DateStart;
        private DateTime _DateEnd;
        private string _SearchStringBill;
       
        public DateTime DateStart { get => _DateStart; set => _DateStart = value; }
        public DateTime DateEnd { get => _DateEnd; set => _DateEnd = value; }
        public ObservableCollection<Bill> ListBill { get => _ListBill; set { _ListBill = value; NotifyOfPropertyChange("ListBill"); } }
        public ObservableCollection<BillDetail> ListBillDetail { get => _ListBillDetail; set { _ListBillDetail = value; NotifyOfPropertyChange("ListBillDetail"); } }
        public string SearchStringBill { get => _SearchStringBill; set
            {
                _SearchStringBill = value;
                NotifyOfPropertyChange("SearchStringBill");
                if(value == "")
                {
                    ListBill = DataAccess.USP_GetListBill(DateStart, DateEnd);
                }
            }
        }
        public Bill SelectedBill { get => _SelectedBill; set {
                _SelectedBill = value;
                NotifyOfPropertyChange("SelectedBill");
                if (value != null)
                {
                    ListBillDetail = DataAccess.USP_GetBillDetail(value.Id);
                }
            }
        }

       

        public BillViewModel()
        {
            ListBillDetail = new ObservableCollection<BillDetail>();
            DateStart = new DateTime(2017, 01, 01);
            DateEnd = DateTime.Now.AddDays(1);
            ListBill = new ObservableCollection<Bill>();
            ListBill = DataAccess.USP_GetListBill(DateStart, DateEnd);
            SearchStringBill = "";
        }

        public void Filter()
        {
            ListBill = new ObservableCollection<Bill>();
            ListBill = DataAccess.USP_GetListBill(DateStart, DateEnd);
        }
        public void SearchBill()
        {
            if (SearchStringBill != "" && SearchStringBill != null)
            {
                ListBill = DataAccess.FindBill(SearchStringBill, DateStart, DateEnd);
                ListBillDetail.Clear();
            }
        }
        public void DeleteBill()
        {
            if (SelectedBill != null)
            {
                DataAccess.UpdateIsDeleted("Bill", 0, SelectedBill.Id);
                ListBill.Remove(SelectedBill);
                ListBillDetail.Clear();
            }
        }
    }
}
