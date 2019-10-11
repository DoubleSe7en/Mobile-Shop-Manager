using Caliburn.Micro;
using MobileShopManagerDesktopApp.Views;
using MobileShopManagerDesktopApp.DataAccessModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Security.Cryptography;
using MobileShopManagerDesktopApp.Models;

namespace MobileShopManagerDesktopApp.ViewModels
{
    public class LoginViewModel: Screen
    {
        public IWindowManager manager = new WindowManager();
        private string _UserName;
        public string UserName { get => _UserName; set { _UserName = value; } }

        private string _Password;
        public string Password { get => _Password; set { _Password = value; } }

        private bool _IsRememberPassword;
        public bool IsRememberPassword { get => _IsRememberPassword; set { _IsRememberPassword = value; } }


        public LoginViewModel()
        {
            UserName = "";
            Password = "";
            IsRememberPassword = false;

        }


        #region method

        //Binding password
        public void PasswordChanged(PasswordBox p)
        {
            Password = p.Password;
        }

        //Đăng nhập
        public void Login(Window loginView)
        {
            
            if(UserName == "" || Password == "")
            {
                MessageBox.Show("Bạn đã để trống thông tin.", "Chú ý");
                return;
            }
            string passEnCode = DataAccess.EnCodePassWord(Password);


            Account account = DataAccess.USP_GetAccountByUserName(UserName, passEnCode);
            if (account != null)
            {
                loginView.Hide();
                MainViewModel main = new MainViewModel();
                if (MainViewModel.LoginView == null)
                {
                    MainViewModel.LoginView = loginView as LoginView;
                }
                MainViewModel.AccountLogin = account;
                manager.ShowWindow(main, null, null);
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu", "Chú ý");
                return;
            }
        }

        //Thoát
        public void Quit(Window loginView)
        {
            loginView.Close();
        }

        
        #endregion
    }
}

