using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileShopManagerDesktopApp.Models
{
    public class Account: Screen
    {
        private string userName;
        private string passWord;
        private string idEmployee;
        private string typeAccount;
        private string nameEmployee;
        public string UserName { get => userName; set { userName = value; } }
        public string PassWord { get => passWord; set { passWord = value;} }
        public string IdEmployee { get => idEmployee; set { idEmployee = value;  } }
        public string TypeAccount { get => typeAccount; set { typeAccount = value; } }
        public string NameEmployee { get => nameEmployee; set => nameEmployee = value; }

        public Account(string UserName, string PassWord, string IdEmployee, string TypeAccount)
        {
            this.UserName = UserName;
            this.PassWord = PassWord;
            this.IdEmployee = IdEmployee;
            this.TypeAccount = TypeAccount;
        }

        public Account(DataRow row)
        {
            this.UserName = (string)row["UserName"];
            this.PassWord = (string)row["PassWord"];
            this.IdEmployee = (string)row["IdEmployee"];
            this.TypeAccount = (string)row["TypeAccount"];
        }
    }
}
