using LeduPasaulyje.Models;
using LeduPasaulyje.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LeduPasaulyje.Commands
{
    public class NullifyObjectCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action execute;
        public NullifyObjectCommand(Action execute)
        {
            this.execute = execute;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            execute.Invoke();
        }
    }
}
