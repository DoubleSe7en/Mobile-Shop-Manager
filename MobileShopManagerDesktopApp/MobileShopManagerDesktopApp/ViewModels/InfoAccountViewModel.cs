using Caliburn.Micro;
using MobileShopManagerDesktopApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileShopManagerDesktopApp.ViewModels
{
    public class InfoAccountViewModel: Screen
    {
        private Account _AccountLogin;
        private Employee _Employee;

        public Account AccountLogin { get => _AccountLogin; set => _AccountLogin = value; }
        public Employee Employee { get => _Employee; set => _Employee = value; }

        public InfoAccountViewModel()
        {
            
        }
    }
}
