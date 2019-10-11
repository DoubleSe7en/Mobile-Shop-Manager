using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MobileShopManagerDesktopApp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class RelayCommand<T> : ICommand
    {
        private readonly Predicate<T> _canExecute; //Lưu trữ điều kiện thực hiện hàm ủy thác
        private readonly Action<T> _execute; //Lưu trữ hàm ủy thác

        //Khi khởi tạo thì truyền điều kiện ủy thác và hàm hủy thác
        public RelayCommand(Predicate<T> canExecute, Action<T> execute)
        {
            if (execute == null)
                throw new ArgumentException("execute");
            _canExecute = canExecute;
            _execute = execute;
        }

        //Điều kiện chạy command
        public bool CanExecute(object parameter)
        {
            try
            {
                return _canExecute == null ? true : _canExecute((T)parameter);
            }
            catch
            {
                return true;
            }
        }

        //Hàm ủy thác khi gọi command
        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        //Tạo 1 event có tên tương ứng để ủy thác
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
