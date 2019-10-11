using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileShopManagerDesktopApp.Models
{
    public class Category
    {
        private string id;
        private string name;
        private int isDeteled;
        
        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int IsDeteled { get => isDeteled; set => isDeteled = value; }

        public Category(string Id, string Name, int IsDeleted)
        {
            this.Id = Id;
            this.Name = Name;
            this.IsDeteled = IsDeleted;
        }

        public Category(DataRow row)
        {
            this.Id = (string)row["Id"];
            this.Name = (string)row["Name"];
            this.IsDeteled = (int)row["IsDeleted"];
        }
    }
}


