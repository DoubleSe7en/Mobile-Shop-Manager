using Caliburn.Micro;
using MobileShopManagerDesktopApp.DataAccessModel;
using MobileShopManagerDesktopApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MobileShopManagerDesktopApp.ViewModels
{
    public class AccountViewModel: Screen 
    {
        private ObservableCollection<Account> _ListAccount;
        private Account _SelectedAccount;
        private string _SearchStringAccount;
        private int _Index;
        public ObservableCollection<Account> ListAccount { get => _ListAccount; set { _ListAccount = value; NotifyOfPropertyChange("ListAccount"); } }
        public Account SelectedAccount { get => _SelectedAccount; set {
                if (value != null)
                {
                    int temp = 0;
                    foreach (var item in ListAccount)
                    {
                        temp++;
                        if (value.UserName == item.UserName)
                        {
                            Index = temp - 1;
                            break;
                        }
                    }
                }
                _SelectedAccount = value;
                NotifyOfPropertyChange("SelectedAccount");
              
            }
        }

        public string SearchStringAccount { get => _SearchStringAccount; set {
                _SearchStringAccount = value;
                NotifyOfPropertyChange("SearchStringAccount");
                if(value == "")
                {
                    ListAccount = DataAccess.LoadListAccount();
                }
            }
        }

        public int Index { get => _Index; set => _Index = value; }

        public AccountViewModel()
        {
            ListAccount = new ObservableCollection<Account>();
            ListAccount = DataAccess.LoadListAccount();
        }
        public void SearchAccount()
        {
            if (SearchStringAccount != "" && SearchStringAccount != null)
            {
                ListAccount = DataAccess.FindAccount(SearchStringAccount);
            }
            
        }
        public void AddAccount()
        {
            
            if (SelectedAccount == null || SelectedAccount.UserName == "" || SelectedAccount.IdEmployee == "" || SelectedAccount.TypeAccount == "" || SelectedAccount.PassWord == "")
            {
                MessageBox.Show("Chưa đủ thông tin.", "Chú ý");
                return;
            }
            if (SelectedAccount.TypeAccount != "Admin" && SelectedAccount.TypeAccount != "Nhân viên" && SelectedAccount.TypeAccount != "Quản lý")
            {
                MessageBox.Show("Loại tài khoản không tồn tại.", "Chú ý");
                return;
            }
            object Check = DataProvider.Ins.ExecuteScalar("Select Count(*) from Account Where UserName = '" + SelectedAccount.UserName + "'");
            if ((int)Check > 0)
            {
                MessageBox.Show("Tên đăng nhập " + SelectedAccount.UserName + " đã tồn tại.", "Chú ý");
                return;
            }
            Check = DataProvider.Ins.ExecuteScalar("Select Count(*) from Employee Where ID = '" + SelectedAccount.IdEmployee + "'");
            if ((int)Check == 0)
            {
                MessageBox.Show("Không tồn tại nhân viên có ID  " + SelectedAccount.IdEmployee, "Chú ý");
                return;
            }
            DataAccess.InsertAccount(SelectedAccount);
            ListAccount = DataAccess.LoadListAccount();
        }
        public void DeleteAccount()
        {
            if(SelectedAccount != null)
            {
                DataProvider.Ins.ExecuteNonQuery("Delete from Account Where UserName = '" + SelectedAccount.UserName + "'");
                ListAccount = DataAccess.LoadListAccount();
            }
        }

        public void EditAccount()
        {
            if (SelectedAccount != null)
            {
                ObservableCollection<Account> ListAccountTemp = DataAccess.LoadListAccount();
                if (Index != -1 && (SelectedAccount.UserName != ListAccountTemp[Index].UserName ||
                    SelectedAccount.IdEmployee != ListAccountTemp[Index].IdEmployee ||
                    SelectedAccount.PassWord != ListAccountTemp[Index].PassWord))
                {
                    MessageBox.Show("Chỉ có thể sửa loại tài khoản.", "Chú ý");
                    return;
                }
                if (SelectedAccount.TypeAccount != "Admin" && SelectedAccount.TypeAccount != "Nhân viên" && SelectedAccount.TypeAccount != "Quản lý")
                {
                    MessageBox.Show("Loại tài khoản không tồn tại.", "Chú ý");
                    return;
                }
                DataProvider.Ins.ExecuteNonQuery("Update Account set TypeAccount = N'" + SelectedAccount.TypeAccount + "' where UserName = '" + SelectedAccount.UserName + "'");
                ListAccount = DataAccess.LoadListAccount();
            }
        }
       
    }
}
