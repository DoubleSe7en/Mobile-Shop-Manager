using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileShopManagerDesktopApp.Models
{
    public class BillDetail
    {
        private int iDBill;
        private string idProduct;
        private int count;
        private string nameProduct;
        public int IDBill { get => iDBill; set => iDBill = value; }
        public string IdProduct { get => idProduct; set => idProduct = value; }
        public int Count { get => count; set => count = value; }
        public string NameProduct { get => nameProduct; set => nameProduct = value; }

        public BillDetail(int IDBill, string IdProduct, int Count)
        {
            this.IDBill = IDBill;
            this.IdProduct = IdProduct;
            this.Count = Count;
        }
        public BillDetail(DataRow row)
        {
            this.IDBill = (int)row["IDBill"];
            this.IdProduct = (string)row["IdProduct"];
            this.Count = (int)row["Count"];
        }
    }
}
