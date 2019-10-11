using Caliburn.Micro;
using MobileShopManagerDesktopApp.Models;
using MobileShopManagerDesktopApp.Views;
using MobileShopManagerDesktopApp.DataAccessModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Collections.ObjectModel;
using System.Windows;
using System.ComponentModel;

namespace MobileShopManagerDesktopApp.ViewModels
{
    public class MainViewModel : Conductor<object>
    {
        private static LoginView _LoginView;
        private static Account _AccountLogin;

        private HomeViewModel _HomeViewModel;
        private InfoAccountViewModel _InfoAccountViewModel;
        private ChangePasswordViewModel _ChangePasswordViewModel;
        private CartViewModel _CartViewModel;
        private CategoryViewModel _CategoryViewModel;
        private ManagerViewModel _ManagerViewModel;
        private string _SearchString;
        public static LoginView LoginView { get => _LoginView; set => _LoginView = value; }
        public static Account AccountLogin { get => _AccountLogin; set => _AccountLogin = value; }
        public HomeViewModel HomeViewModel { get => _HomeViewModel; set => _HomeViewModel = value; }
        public InfoAccountViewModel InfoAccountViewModel { get => _InfoAccountViewModel; set => _InfoAccountViewModel = value; }
        public ChangePasswordViewModel ChangePasswordViewModel { get => _ChangePasswordViewModel; set => _ChangePasswordViewModel = value; }
        public CartViewModel CartViewModel { get => _CartViewModel; set => _CartViewModel = value; }
        public CategoryViewModel CategoryViewModel { get => _CategoryViewModel; set => _CategoryViewModel = value; }
        public string SearchString { get => _SearchString; set {
                _SearchString = value;
                NotifyOfPropertyChange("SearchString");
            }
        }

        public ManagerViewModel ManagerViewModel { get => _ManagerViewModel; set => _ManagerViewModel = value; }

        public MainViewModel()
        {
            this.HomeViewModel = new HomeViewModel();
            this.InfoAccountViewModel = new InfoAccountViewModel();
            this.ChangePasswordViewModel = new ChangePasswordViewModel();
            this.CartViewModel = new CartViewModel();
            this.HomeViewModel.CartViewModel = this.CartViewModel;
            this.ManagerViewModel = new ManagerViewModel();
            this.SearchString = "";
        }
          
            

        
        public void Loaded(MainView mainView) 
        {
            if (AccountLogin.TypeAccount != "Admin" && AccountLogin.TypeAccount != "Quản lý")
            {
                mainView.Management.Visibility = System.Windows.Visibility.Collapsed;
            }
            ActivateItem(this.HomeViewModel);
        }
   
        public void HomeClick()
        {
            ActivateItem(this.HomeViewModel);
            this.SearchString = "";
        }

        public void CategoryClick(Button btn)
        {
            string IdCategory = "";
            
            switch(btn.Name)
            {
                case "btnApple":
                    {
                        IdCategory = "AP";
                        break;
                    }
                case "btnSamSung":
                    {
                        IdCategory = "SM";
                        break;
                    }
                case "btnNokia":
                    {
                        IdCategory = "NK";
                        break;
                    }
                case "btnSony":
                    {
                        IdCategory = "SN";
                        break;
                    }
                case "btnHTC":
                    {
                        IdCategory = "HTC";
                        break;
                    }
                case "btnLG":
                    {
                        IdCategory = "LG";
                        break;
                    }
                case "btnOther":
                    {
                        IdCategory = "";
                        break;
                    }
            }
            this.CategoryViewModel = new CategoryViewModel(IdCategory);
            this.CategoryViewModel.CartViewModel = this.CartViewModel;
            ActivateItem(this.CategoryViewModel);
            this.SearchString = "";
        }
        public void ShowInfoAccount()
        {
          
            if (AccountLogin != null && InfoAccountViewModel.AccountLogin == null)
            {
                Employee employee = DataAccess.USP_GetEmployeeByID(AccountLogin.IdEmployee);
                InfoAccountViewModel.Employee = employee;
                InfoAccountViewModel.AccountLogin = AccountLogin;
            }
            ActivateItem(this.InfoAccountViewModel);
            this.SearchString = "";
        }
       
        public void Logout(MainView mainView)
        {
            if (LoginView != null)
            {
                LoginView.Show();
                mainView.Close();
                var login = LoginView.DataContext as LoginViewModel;
                if (login.IsRememberPassword == false)
                {
                    LoginView.PassWord.Clear();
                }

            }
        }
        public void ChangePassword()
        {
            if(AccountLogin != null && ChangePasswordViewModel.AccountLogin == null)
            {
                ChangePasswordViewModel.AccountLogin = AccountLogin;
                ChangePasswordViewModel.HomeViewModel = this.HomeViewModel;
            }
            ActivateItem(ChangePasswordViewModel);
            this.SearchString = "";
        }

        public void ShowCart()
        {

            this.CartViewModel.IdEmployee = AccountLogin.IdEmployee;
            //this.CartViewModel.IdEmployee = "NV01";
            ActivateItem(this.CartViewModel);
            this.SearchString = "";
        }
       public void Search()
       {
            if (SearchString != "" && SearchString != null)
            {
                SearchViewModel searchView = new SearchViewModel(SearchString);
                ActivateItem(searchView);
            }
        }

        public void Management()
        {
            ActivateItem(this.ManagerViewModel);
        }
    }
}
