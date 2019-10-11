using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileShopManagerDesktopApp.ViewModels
{
    public class ManagerViewModel: Conductor<object>
    {
        public ManagerViewModel()
        {
            ActivateItem(new BillViewModel());
        }
        public void Employee()
        {
            ActivateItem(new EmployeeManagerViewModel());
        }
        public void Customer()
        {
            ActivateItem(new CustomerViewModel());
        }
        public void Account()
        {
            ActivateItem(new AccountViewModel());
        }
        public void WareHouse()
        {
            ActivateItem(new WareHouseViewModel());
        }
        public void Bill()
        {
            ActivateItem(new BillViewModel());
        }
        public void Product()
        {
            ActivateItem(new ProductViewModel());
        }
    }
}
