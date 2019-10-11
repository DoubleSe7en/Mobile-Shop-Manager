using Caliburn.Micro;
using MobileShopManagerDesktopApp.Models;
using MobileShopManagerDesktopApp.DataAccessModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileShopManagerDesktopApp.ViewModels
{
    public class DetailViewModel: Screen
    {
        private ProductOrder _SelectedProduct;
        private ObservableCollection<string> _ListPath;
        private ProductDetail _ProductDetail;
        private CartViewModel _CartViewModel;
        public ObservableCollection<string> ListPath { get => _ListPath; set => _ListPath = value; }
        public ProductOrder SelectedProduct { get => _SelectedProduct; set => _SelectedProduct = value; }
        public ProductDetail ProductDetail { get => _ProductDetail; set { _ProductDetail = value; NotifyOfPropertyChange("ProductDetail"); }  }

        public CartViewModel CartViewModel { get => _CartViewModel; set => _CartViewModel = value; }

        public DetailViewModel(ProductOrder product)
        {
            SelectedProduct = product;
            ListPath = new ObservableCollection<string>();
            LoadPathPhotoList();
            ProductDetail = DataAccess.USP_GetDetailProductByID(SelectedProduct.Info.Id);
        }
        private void LoadPathPhotoList()
        {
            DirectoryInfo di = new DirectoryInfo(@"..\..\Images_Product\" + SelectedProduct.Info.Name);
            if (di.Exists == false)
            {
                di = new DirectoryInfo(@"..\Images_Product\" + SelectedProduct.Info.Name);
            }
            foreach (var fi in di.GetFiles())
            {
                ListPath.Add(fi.FullName);
            }
            ListPath.RemoveAt(0);
        }
        public void AddToCart()
        {
            if (this.CartViewModel != null && SelectedProduct != null && SelectedProduct.AmountRemaining > 0)
            {
                SelectedProduct.Count = 1;
                this.CartViewModel.AddProductOrder(SelectedProduct);
            }
        }
    }
}
