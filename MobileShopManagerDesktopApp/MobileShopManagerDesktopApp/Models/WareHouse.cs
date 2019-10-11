using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileShopManagerDesktopApp.Models
{
    public class WareHouse
    {
        private int id;
        private string idProduct;
        private int amountAdded;
        private int amountRemaining;
        private DateTime dateAdded;
        private string orgin;
        private string nameProduct;

        public int Id { get => id; set => id = value; }
        public string IdProduct { get => idProduct; set => idProduct = value; }
        public int AmountAdded { get => amountAdded; set => amountAdded = value; }
        public int AmountRemaining { get => amountRemaining; set => amountRemaining = value; }
        public DateTime DateAdded { get => dateAdded; set => dateAdded = value; }
        public string Orgin { get => orgin; set => orgin = value; }
        public string NameProduct { get => nameProduct; set => nameProduct = value; }

        public WareHouse(int Id, string IdProduct, int AmountAdded, int AmountRemaining, DateTime DataAdded, string Orgin)
        {
            this.Id = Id;
            this.IdProduct = IdProduct;
            this.AmountAdded = AmountAdded;
            this.AmountRemaining = AmountRemaining;
            this.DateAdded = DateAdded;
            this.Orgin = Orgin;
        }
        public WareHouse(DataRow row)
        {
            this.Id = (int)row["Id"];
            this.IdProduct = (string)row["IdProduct"];
            this.AmountAdded = (int)row["AmountAdded"];
            this.AmountRemaining = (int)row["AmountRemaining"];
            if (row["DateAdded"].ToString() != "")
            {
                this.DateAdded = (DateTime)row["DateAdded"];
            }
            if (row["Orgin"].ToString() != "")
            {
                this.Orgin = (string)row["Orgin"];
            }
         
        }
    }
}
