using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileShopManagerDesktopApp.Models
{
    public class Product
    {
        private string id;
        private string name;
        private string idCategory;
        private double price;
        private double salePercent;
        private int isDeleted;
        private string pathAvatar;

        public string Id { get => id; set => id = value; }
        public string IdCategory { get => idCategory; set => idCategory = value; }
        public string Name { get => name; set { name = value; } }
        public double Price { get => price; set { price = value;  } }

        public double SalePercent { get => salePercent; set => salePercent = value; }
        public int IsDeleted { get => isDeleted; set => isDeleted = value; }
        public string PathAvatar { get => pathAvatar; set => pathAvatar = value; }

        public Product(string Id, string Name, string IdCategory, double Price, double SalePercent, int IsDeleted)
        {
            this.Id = Id;
            this.IdCategory = IdCategory;
            this.Name = Name;
            this.Price = Price;
            this.SalePercent = SalePercent;
            this.IsDeleted = IsDeleted;
        }

        public Product(DataRow row)
        {
            this.Id = (string)row["Id"];
            this.IdCategory = (string)row["IdCategory"];
            this.Name = (string)row["Name"];
            this.Price = (double)row["Price"];
            this.SalePercent = (double)row["SalePercent"];
            this.IsDeleted = (int)row["IsDeleted"];
        }
    }
}
