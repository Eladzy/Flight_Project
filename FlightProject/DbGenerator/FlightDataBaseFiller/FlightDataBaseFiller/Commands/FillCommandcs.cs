using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlightDataBaseFiller.Commands
{
    class FillCommandcs : ICommand
    {
        bool CanRun { get; set; }
        public event EventHandler CanExecuteChanged;

        public FillCommandcs()
        {

        }

        public bool CanExecute(object parameter)
        {
            return CanRun;
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
