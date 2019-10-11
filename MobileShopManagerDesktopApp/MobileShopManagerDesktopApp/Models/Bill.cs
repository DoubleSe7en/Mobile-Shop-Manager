using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileShopManagerDesktopApp.Models
{
    public class Bill
    {
        private int id;
        private string idCustomer;
        private string idEmployee;
        private DateTime dateCheckOut;
        private double totalPayment;
        private int isDeleted;
        private string nameEmployee;
        private string nameCustomer;


        public int Id { get => id; set => id = value; }
        public string IdCustomer { get => idCustomer; set => idCustomer = value; }
        public DateTime DateCheckOut { get => dateCheckOut; set => dateCheckOut = value; }
        public string IdEmployee { get => idEmployee; set => idEmployee = value; }
        public double TotalPayment { get => totalPayment; set => totalPayment = value; }
        public int IsDeleted { get => isDeleted; set => isDeleted = value; }
        public string NameEmployee { get => nameEmployee; set => nameEmployee = value; }
        public string NameCustomer { get => nameCustomer; set => nameCustomer = value; }

        public Bill(int Id, string IdCustomer, string IDEmployee, DateTime DateCheckOut, double TotalPayment, int IsDeleted)
        {
            this.Id = Id;
            this.IdCustomer = IdCustomer;
            this.IdEmployee = IDEmployee;
            this.DateCheckOut = DateCheckOut;
            this.TotalPayment = TotalPayment;
            this.IsDeleted = IsDeleted;
        }
        public Bill(DataRow row)
        {
            this.Id = (int)row["Id"];
            
            if (row["idCustomer"].ToString() != "")
            {
                this.IdCustomer = (string)row["IdCustomer"];
            }
            else
            {
                IdCustomer = "NULL";
            }
            this.IdEmployee = (string)row["IdEmployee"];
            this.DateCheckOut = (DateTime)row["DateCheckOut"];
            this.TotalPayment = (double)row["TotalPayment"];
            this.IsDeleted = (int)row["IsDeleted"];

        }
    }
}
