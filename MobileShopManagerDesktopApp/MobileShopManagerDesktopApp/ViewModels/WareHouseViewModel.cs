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
    public class WareHouseViewModel: Screen
    {
        private ObservableCollection<WareHouse> _ListProductInWareHouse;
        private ObservableCollection<ProductOrder> _ListProductRemaining;
        private DateTime _DateStart;
        private DateTime _DateEnd;
        public ObservableCollection<WareHouse> ListProductInWareHouse { get => _ListProductInWareHouse; set { _ListProductInWareHouse = value; NotifyOfPropertyChange("ListProductInWareHouse"); } }
        public ObservableCollection<ProductOrder> ListProductRemaining { get => _ListProductRemaining; set => _ListProductRemaining = value; }
        public DateTime DateStart { get => _DateStart; set => _DateStart = value; }
        public DateTime DateEnd { get => _DateEnd; set => _DateEnd = value; }

        public WareHouseViewModel()
        {
            DateStart = new DateTime(2017, 01, 01);
            DateEnd = DateTime.Now.AddDays(1);
            ListProductInWareHouse = new ObservableCollection<WareHouse>();
            ListProductInWareHouse = DataAccess.LoadProductInWareHouse(DateStart, DateEnd);
            ListProductRemaining = new ObservableCollection<ProductOrder>();
            ListProductRemaining = DataAccess.USP_GetAmountRemaining_ProductInWareHouse();
        }

        public void Filter()
        {
            ListProductInWareHouse = new ObservableCollection<WareHouse>();
            ListProductInWareHouse = DataAccess.LoadProductInWareHouse(DateStart, DateEnd);
        }
    }
}
