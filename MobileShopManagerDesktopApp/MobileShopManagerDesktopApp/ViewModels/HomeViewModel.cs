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
    public class HomeViewModel : Screen
    {
        private ObservableCollection<ProductOrder> _ListProductSale;
        private ObservableCollection<ProductOrder> _ListProductHot;

        private ProductOrder _SelectedProduct;
        private CartViewModel _CartViewModel;
        
        
        public ObservableCollection<ProductOrder> ListProductSale { get => _ListProductSale; set => _ListProductSale = value; }
        public ProductOrder SelectedProduct { get => _SelectedProduct;
            set {
                _SelectedProduct = value;
                NotifyOfPropertyChange("SelectedProduct");
            }
        }
        public CartViewModel CartViewModel { get => _CartViewModel; set => _CartViewModel = value; }
        public ObservableCollection<ProductOrder> ListProductHot { get => _ListProductHot; set => _ListProductHot = value; }

        public HomeViewModel()
        {
            LoadData();
        }

       
        public void LoadData()
        {
            ListProductSale = new ObservableCollection<ProductOrder>();
            ListProductHot = new ObservableCollection<ProductOrder>();
            ListProductSale = DataAccess.LoadProductSale();
            ListProductHot = DataAccess.LoadProductHot();
        }
        public void ShowSelectedProduct()
        {
            DetailViewModel detail = new DetailViewModel(SelectedProduct);
            detail.CartViewModel = this.CartViewModel;
            var ParentConductor = (Conductor<object>)(this.Parent);
            ParentConductor.ActivateItem(detail);
        }
        public void AddToCart()
        {
            if(this.CartViewModel != null && SelectedProduct != null && SelectedProduct.AmountRemaining > 0)
            {
                SelectedProduct.Count = 1;
                this.CartViewModel.AddProductOrder(SelectedProduct);
            }
        }
    }
}
