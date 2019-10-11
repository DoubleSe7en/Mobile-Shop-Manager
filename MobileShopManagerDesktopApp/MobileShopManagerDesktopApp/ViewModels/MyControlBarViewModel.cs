using MobileShopManagerDesktopApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MobileShopManagerDesktopApp.ViewModels
{
   
    public class MyControlBarViewModel
    {
        public ICommand CloseWindowCommand { get; set; }
        public ICommand MaximizeWindowCommand { get; set; }
        public ICommand MinimizeWindowCommand { get; set; }
        public ICommand MouseMoveCommand { get; set; }

        public ICommand LoadedCommand { get; set; }
        public MyControlBarViewModel()
        {
            CloseWindowCommand = new RelayCommand<UserControl>(p => { return p == null ? false : true; }, p => {
                FrameworkElement window = GetWindowParent(p);
                var w = window as Window;
                if (w != null)
                {
                    if (w is MainView)
                        Application.Current.Shutdown();
                    else
                     w.Close();
                }

            });
            MaximizeWindowCommand = new RelayCommand<UserControl>(p => { return p == null ? false : true; }, 
                p => { FrameworkElement window = GetWindowParent(p);
                var w = window as Window;
                if (w != null)
                {
                    if (w.WindowState != WindowState.Maximized)
                        w.WindowState = WindowState.Maximized;
                    else
                        w.WindowState = WindowState.Normal;
                }

            });
            MinimizeWindowCommand = new RelayCommand<UserControl>(p => { return p == null ? false : true; }, p => {
                FrameworkElement window = GetWindowParent(p);
                var w = window as Window;
                if (w != null && w.WindowState != WindowState.Minimized)
                {
                    w.WindowState = WindowState.Minimized;
                }

            });
            MouseMoveCommand = new RelayCommand<UserControl>(p => { return p == null ? false : true; }, p => {
                FrameworkElement window = GetWindowParent(p);
                var w = window as Window;
                if (w != null)
                {
                    w.DragMove();
                }
            });

            LoadedCommand = new RelayCommand<MyCotrolBarView>(p => { return p == null ? false : true; }, p => {
                FrameworkElement window = GetWindowParent(p);
                var w = window as Window;
                if (w is LoginView)
                {
                    p.myStackPanel.Children.Remove(p.btnMaximize);
                }

            });
        }
        FrameworkElement GetWindowParent(UserControl p)
        {
            FrameworkElement parent = p;
            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }
            return parent;
        }

    }

}
