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
    public class ProductViewModel
    {
        private ObservableCollection<Product> _ListProduct;

        public ObservableCollection<Product> ListProduct { get => _ListProduct; set => _ListProduct = value; }
        public ProductViewModel()
        {
            ListProduct = new ObservableCollection<Product>();
            ListProduct = DataAccess.LoadListProduct();
        }

    }
}
