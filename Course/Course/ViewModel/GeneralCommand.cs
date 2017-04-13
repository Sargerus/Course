using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Course.ViewModel
{
    public class GeneralCommand : ICommand
    {
        private readonly Action _excecute;
        private readonly Func<bool> _canExcecute;

        public GeneralCommand(Action excecute)
            : this(excecute, null)
        {

        }

        public GeneralCommand(Action excecute, Func<bool> canExecute)
        {
            if (excecute == null) throw new NullReferenceException();
            _excecute = excecute;
            _canExcecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            if (_canExcecute != null)
               return _canExcecute();
            return true;
        }

        public void Execute(object parameter)
        {
            _excecute();
        }
    }
}
