using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileShopManagerDesktopApp.Models
{
    public class Customer
    {
        private string id;
        private string name;
        private int point;
        private int isDeleted;

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int Point { get => point; set => point = value; }
        public int IsDeleted { get => isDeleted; set => isDeleted = value; }

        public Customer(string Id, string Name, int Point, int IsDeleted)
        {
            this.Id = Id;
            this.Name = Name;
            this.Point = Point;
            this.IsDeleted = IsDeleted;
        }

        public Customer(DataRow row)
        {
            this.Id = (string)row["Id"];
            this.Name = (string)row["Name"];
            this.Point = (int)row["Point"];
            this.IsDeleted = (int)row["IsDeleted"];
        }
    }
}
