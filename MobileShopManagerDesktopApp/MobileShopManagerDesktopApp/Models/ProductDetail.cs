using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileShopManagerDesktopApp.Models
{
    public class ProductDetail
    {
        private string idProduct;
        private string screen;
        private string operatingSystem;
        private string rearCamera;
        private string fontCamera;
        private string cPU;
        private string ram;
        private string rom;
        private string memoryCard;
        private string sim;
        private string battery;
        private string manufacturer;
        private string orgin;
        private string warranttyPeriod;

        public string IdProduct { get => idProduct; set => idProduct = value; }
        public string Screen { get => screen; set => screen = value; }
        public string OperatingSystem { get => operatingSystem; set => operatingSystem = value; }
        public string RearCamera { get => rearCamera; set => rearCamera = value; }
        public string FontCamera { get => fontCamera; set => fontCamera = value; }
        public string CPU { get => cPU; set => cPU = value; }
        public string Ram { get => ram; set => ram = value; }
        public string Rom { get => rom; set => rom = value; }
        public string MemoryCard { get => memoryCard; set => memoryCard = value; }
        public string Sim { get => sim; set => sim = value; }
        public string Battery { get => battery; set => battery = value; }
        public string Manufacturer { get => manufacturer; set => manufacturer = value; }
        public string Orgin { get => orgin; set => orgin = value; }
        public string WarranttyPeriod { get => warranttyPeriod; set => warranttyPeriod = value; }

        public ProductDetail(string IDProduct, string Screen, string OperatingSystem, string RearCamera, string FontCamera, string CPU, string Ram, string Rom, string MemoryCard, string Sim, string Battery, string Manufacturer, string Orgin, string WarranttyPeriod)
        {
            this.IdProduct = IDProduct;
            this.Screen = Screen;
            this.OperatingSystem = OperatingSystem;
            this.RearCamera = RearCamera;
            this.FontCamera = FontCamera;
            this.CPU = CPU;
            this.Ram = Ram;
            this.Rom = Rom;
            this.MemoryCard = MemoryCard;
            this.Sim = Sim;
            this.Battery = Battery;
            this.Manufacturer = Manufacturer;
            this.Orgin = Orgin;
            this.WarranttyPeriod = WarranttyPeriod;
        }
        public ProductDetail(DataRow row)
        {
            this.IdProduct = (string)row["IDProduct"];
            this.Screen = (string)row["Screen"];
            this.OperatingSystem = (string)row["OperatingSystem"];
            this.RearCamera = (string)row["RearCamera"];
            this.FontCamera = (string)row["FontCamera"];
            this.CPU = (string)row["CPU"];
            this.Ram = (string)row["Ram"];
            this.Rom = (string)row["Rom"];
            this.MemoryCard = (string)row["MemoryCard"];
            this.Sim = (string)row["Sim"];
            this.Battery = (string)row["Battery"];
            this.Manufacturer = (string)row["Manufacturer"];
            this.Orgin = (string)row["Orgin"];
            this.WarranttyPeriod = (string)row["WarranttyPeriod"];
        }
    }
}
