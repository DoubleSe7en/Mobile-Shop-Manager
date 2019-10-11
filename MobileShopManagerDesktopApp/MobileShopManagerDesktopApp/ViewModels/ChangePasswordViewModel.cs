using Caliburn.Micro;
using MobileShopManagerDesktopApp.DataAccessModel;
using MobileShopManagerDesktopApp.Models;
using MobileShopManagerDesktopApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MobileShopManagerDesktopApp.ViewModels
{
    public class ChangePasswordViewModel : Screen
    {
        private Account _AccountLogin;
        private string _OldPassword;
        private string _NewPassword;
        private string _ConfirmPassword;
        private HomeViewModel _HomeViewModel;
 

        public Account AccountLogin { get => _AccountLogin; set => _AccountLogin = value; }
        public string OldPassword { get => _OldPassword; set => _OldPassword = value; }
        public string NewPassword { get => _NewPassword; set => _NewPassword = value; }
        public string ConfirmPassword { get => _ConfirmPassword; set => _ConfirmPassword = value; }
        public HomeViewModel HomeViewModel { get => _HomeViewModel; set => _HomeViewModel = value; }
       

        public ChangePasswordViewModel()
        {
            OldPassword = "";
            NewPassword = "";
            ConfirmPassword = "";
        }
        public void OldPasswordChanged(PasswordBox p)
        {
            OldPassword = p.Password;
        }
        public void NewPasswordChanged(PasswordBox p)
        {
            NewPassword = p.Password;
        }
        public void ConfirmPasswordChanged(PasswordBox p)
        {
            ConfirmPassword = p.Password;
        }
        public void OkClick()
        {
            string passEnCode;
            if (AccountLogin == null)
            {
                return;
            }
            if (OldPassword == "" || NewPassword == "" || ConfirmPassword == "")
            {
                MessageBox.Show("Bạn đã nhập thiếu thông tin.", "Chú ý");
                return;
            }
            passEnCode = DataAccess.EnCodePassWord(OldPassword);
            if (passEnCode != AccountLogin.PassWord)
            {
                MessageBox.Show("Mật khẩu cũ không đúng.", "Chú ý");
                return;
            }
            if (NewPassword != ConfirmPassword)
            {
                MessageBox.Show("Xác nhận mật khẩu sai.", "Chú ý");
                return;
            }
            MessageBoxResult check = MessageBox.Show("Xác nhận đổi mật khẩu.", "Thông báo", MessageBoxButton.OKCancel);
            if (check == MessageBoxResult.Cancel)
            {
                return;
            }
            else
            {
                MessageBox.Show("Đổi mật khẩu thành công.", "Thông báo");
                passEnCode = DataAccess.EnCodePassWord(NewPassword);
                DataAccess.USP_UpdateAccount(AccountLogin.UserName, passEnCode, AccountLogin.TypeAccount);
            }
        }
        
        public void CancelClick()
        {
            var ParentConductor = (Conductor<object>)(this.HomeViewModel.Parent);
            ParentConductor.ActivateItem(this.HomeViewModel);
        }

       
        
    }
}
