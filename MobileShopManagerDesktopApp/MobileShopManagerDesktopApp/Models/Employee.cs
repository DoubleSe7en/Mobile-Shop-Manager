using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileShopManagerDesktopApp.Models
{
    public class Employee: Screen
    {
        private string id;
        private string name;
        private DateTime birthday;
        private string gender;
        private string cMND;
        private DateTime dateStarted;
        private double salary;
        private string phoneNumber;
        private int isDeleted;
        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public DateTime Birthday { get => birthday; set => birthday = value; }
        public string Gender { get => gender; set => gender = value; }
        public string CMND { get => cMND; set => cMND = value; }
        public DateTime DateStarted { get => dateStarted; set => dateStarted = value; }

        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public double Salary { get => salary; set => salary = value; }
        public int IsDeleted { get => isDeleted; set => isDeleted = value; }

        public Employee(string Id, string Name, DateTime Birthday, string Gender, string CMND, DateTime DateStarted, double Salary, string PhoneNumber, int IsDeleted)
        {
            this.Id = Id;
            this.Name = Name;
            this.Birthday = Birthday;
            this.Gender = Gender;
            this.CMND = CMND;
            this.DateStarted = DateStarted;
            this.Salary = Salary;
            this.PhoneNumber = PhoneNumber;
            this.IsDeleted = IsDeleted;
        }
        public Employee(DataRow row)
        {
            this.Id = (string)row["Id"];
            if (row["Name"].ToString() != "")
            {
                this.Name = (string)row["Name"];
            }
            if (row["Birthday"].ToString() != "")
            {
                this.Birthday = (DateTime)row["Birthday"];
            }
            if (row["Gender"].ToString() != "")
            {
                this.Gender = (string)row["Gender"];
            }
            if (row["CMND"].ToString() != "")
            {
                this.CMND = (string)row["CMND"];
            }
            if (row["DateStarted"].ToString() != "")
            {
                this.DateStarted = (DateTime)row["DateStarted"];
            }
            if (row["Salary"].ToString() != "")
            {
                this.Salary = (double)row["Salary"];
            }
            if (row["PhoneNumber"].ToString() != "")
            {
                this.PhoneNumber = (string)row["PhoneNumber"];
            }
            this.IsDeleted = (int)row["IsDeleted"];
        }
    }
}
