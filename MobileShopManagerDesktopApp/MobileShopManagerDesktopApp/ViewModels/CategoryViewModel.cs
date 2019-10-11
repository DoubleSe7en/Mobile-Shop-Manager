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
    public class CategoryViewModel: Screen
    {
        private ObservableCollection<ProductOrder> _ListProduct;
        private ProductOrder _SelectedProduct;
        private string _IdCategory;
        private CartViewModel _CartViewModel;
        public ObservableCollection<ProductOrder> ListProduct { get => _ListProduct; set { _ListProduct = value; NotifyOfPropertyChange("ListProduct"); } }
        public ProductOrder SelectedProduct { get => _SelectedProduct; set => _SelectedProduct = value; }
        public string IdCategory { get => _IdCategory; set => _IdCategory = value; }
        public CartViewModel CartViewModel { get => _CartViewModel; set => _CartViewModel = value; }

        public CategoryViewModel(string IdCategory)
        {
            this.IdCategory = IdCategory;
            LoadData();
        }
        public void LoadData()
        {
            //Load danh sách loại sản phẩm
            ListProduct = new ObservableCollection<ProductOrder>();
            if (IdCategory == "")
                ListProduct = DataAccess.USP_GetProductByIDCategoryOther();
            else
                ListProduct = DataAccess.USP_GetProductByIDCategory(IdCategory);
        }
        public void AddToCart()
        {
            if (this.CartViewModel != null && SelectedProduct != null && SelectedProduct.AmountRemaining > 0)
            {
                SelectedProduct.Count = 1;
                this.CartViewModel.AddProductOrder(SelectedProduct);
            }
        }
        public void ShowSelectedProduct()
        {
            DetailViewModel detail = new DetailViewModel(SelectedProduct);
            detail.CartViewModel = this.CartViewModel;
            var ParentConductor = (Conductor<object>)(this.Parent);
            ParentConductor.ActivateItem(detail);
        }
    }
}
