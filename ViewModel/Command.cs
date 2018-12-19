using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp.Model
{
    class Command : ICommand
    {
        Action<object> _executeMethod;
        public event EventHandler CanExecuteChanged;

        public Command(Action<object> executeMethod)
        {
            this._executeMethod = executeMethod;
        }


        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _executeMethod(parameter);
        }
    }
}
