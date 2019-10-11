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
    public class SearchViewModel: Screen
    {
        private ObservableCollection<ProductOrder> _ListProduct;
        private ProductOrder _SelectedProduct;
        private CartViewModel _CartViewModel;
        private string _SearchString;
        private string _StatusSearch;
        public ObservableCollection<ProductOrder> ListProduct { get => _ListProduct; set { _ListProduct = value; NotifyOfPropertyChange("ListProduct"); } }
        public ProductOrder SelectedProduct { get => _SelectedProduct; set => _SelectedProduct = value; }
   
        public CartViewModel CartViewModel { get => _CartViewModel; set => _CartViewModel = value; }
        public string SearchString { get => _SearchString; set => _SearchString = value; }
        public string StatusSearch { get => _StatusSearch; set { _StatusSearch = value; NotifyOfPropertyChange("StatusSearch"); } }

        public SearchViewModel(string SearchString)
        {
            this.SearchString = SearchString;
            LoadData();
        }
        public void LoadData()
        {
            ListProduct = new ObservableCollection<ProductOrder>();
            ListProduct = DataAccess.FindProductByName(SearchString);
            if(ListProduct.Count == 0)
            {
                Category category = DataAccess.FindCategory(SearchString);
                if(category != null)
                {
                    ListProduct = DataAccess.USP_GetProductByIDCategory(category.Id);
                }
            }
            if (ListProduct.Count == 0)
            {
                this.StatusSearch = "Không tìm thấy kết quả nào phù hợp với từ khóa " + this.SearchString;
            }
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
