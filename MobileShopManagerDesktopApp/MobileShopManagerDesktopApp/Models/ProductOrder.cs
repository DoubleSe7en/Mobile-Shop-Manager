using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileShopManagerDesktopApp.Models
{
    public class ProductOrder: Screen
    {
        private Product info;
        private int count;
        private double priceSale;
        private string priceNotSale;
        private string valueSale;
        private int amountRemaining;
        private string status;
        public Product Info { get => info; set => info = value; }
        public int Count { get => count; set { count = value; NotifyOfPropertyChange("Count"); } }
        public double PriceSale { get => priceSale; set => priceSale = value; }
        public string PriceNotSale { get => priceNotSale; set => priceNotSale = value; }
        public string ValueSale { get => valueSale; set => valueSale = value; }
        public int AmountRemaining { get => amountRemaining; set => amountRemaining = value; }
        public string Status { get => status; set => status = value; }

        public ProductOrder(Product Info, int Count)
        {
            this.Info = Info;
            this.Count = Count;
            this.PriceSale = Info.Price - Info.Price * Info.SalePercent*0.01;
            if(Info.SalePercent > 0)
            {
                PriceNotSale = Info.Price.ToString() + "đ";
                ValueSale = "-" + Info.SalePercent.ToString() + "%";
            }
            else
            {
                PriceNotSale = null;
                ValueSale = null;
            }

        }
    }
}
