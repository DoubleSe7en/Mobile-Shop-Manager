using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MobileShopManagerDesktopApp.ViewModels;

namespace MobileShopManagerDesktopApp.Views
{
    /// <summary>
    /// Interaction logic for MyCotrolBarView.xaml
    /// </summary>
    public partial class MyCotrolBarView : UserControl
    {
        public MyControlBarViewModel ViewModel { get; set; }
        public MyCotrolBarView()
        {
            InitializeComponent();
            this.DataContext = ViewModel = new MyControlBarViewModel();
        }
    }
}
