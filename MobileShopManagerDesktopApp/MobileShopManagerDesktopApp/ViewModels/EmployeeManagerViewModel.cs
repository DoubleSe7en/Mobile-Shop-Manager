using MobileShopManagerDesktopApp.DataAccessModel;
using MobileShopManagerDesktopApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MobileShopManagerDesktopApp.ViewModels
{
    public class EmployeeManagerViewModel: Caliburn.Micro.Screen
    {
        private ObservableCollection<Employee> _ListEmployee;
        private Employee _SelectedEmployee;
        private string _SearchStringEmployee;
        private int _Index;
        
        public ObservableCollection<Employee> ListEmployee { get => _ListEmployee; set { _ListEmployee = value; NotifyOfPropertyChange("ListEmployee"); } }
        public Employee SelectedEmployee { get => _SelectedEmployee; set {

                if (value != null)
                {
                    int temp = 0;
                    foreach (var item in ListEmployee)
                    {
                        temp++;
                        if(value.Id == item.Id)
                        {
                            Index = temp - 1;
                            break;
                        }
                    }
                }
                _SelectedEmployee = value;
                NotifyOfPropertyChange("SelectedEmployee");
            } }

        public string SearchStringEmployee { get => _SearchStringEmployee; set {
                
                _SearchStringEmployee = value;
                NotifyOfPropertyChange("SearchStringEmployee");
                if(value == "")
                {
                    ListEmployee = DataAccess.LoadListEmployee();
                }
            }
        }

        public int Index { get => _Index; set { _Index = value; NotifyOfPropertyChange("Index"); } }

        public EmployeeManagerViewModel()
        {
            ListEmployee = new ObservableCollection<Employee>();
            ListEmployee = DataAccess.LoadListEmployee();
            SearchStringEmployee = "";
            Index = -1;
        }
        public void SearchEmployee()
        {
            if(SearchStringEmployee != "" && SearchStringEmployee != null)
            {
                ListEmployee = DataAccess.FindEmployee(SearchStringEmployee);
            }
        }
        public void AddEmployee()
        {
            if(SelectedEmployee == null || SelectedEmployee.Id == "")
            {
                MessageBox.Show("Chưa điền mã nhân viên.", "Chú ý");
                return;
            }
            object Check = DataProvider.Ins.ExecuteScalar("Select Count(*) from Employee Where Id = '" + SelectedEmployee.Id + "'");
            if((int)Check > 0)
            {
                MessageBox.Show("Mã nhân viên " + SelectedEmployee.Id + " đã tồn tại.", "Chú ý");
                return;
            }
            DataAccess.InsertEmployee(SelectedEmployee);
            ListEmployee = DataAccess.LoadListEmployee();
        }
        public void DeleteEmployee()
        {
            if (SelectedEmployee != null)
            {
                DataAccess.UpdateIsDeleted("Employee", 0, SelectedEmployee.Id);
                ListEmployee.Remove(SelectedEmployee);
                SelectedEmployee = null;
            }
        }
        public void EditEmployee()
        {
            if (SelectedEmployee != null)
            {
                ObservableCollection<Employee> ListEmployeeTemp = DataAccess.LoadListEmployee();
                if(Index != -1 && SelectedEmployee.Id != ListEmployeeTemp[Index].Id)
                {
                    MessageBox.Show("Không thể sửa ID nhân viên.", "Chú ý");
                    return;
                }
                DataAccess.USP_UpdateEmployee(SelectedEmployee);
                ListEmployee = DataAccess.LoadListEmployee();
            }
        }
    }
}
