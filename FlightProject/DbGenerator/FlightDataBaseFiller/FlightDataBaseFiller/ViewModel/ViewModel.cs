using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Prism;
using Prism.Commands;

namespace FlightDataBaseFiller.ViewModel
{
    class ViewModel
    {
        Dispatcher dispatcher { get; set; }
        public DelegateCommand Command { get; set; }
        private DispatcherTimer Timer=new DispatcherTimer();
        public ViewModel()
        {
            
            this.Timer.Interval = TimeSpan.FromMilliseconds(500);
            this.Timer.Tick += VerifyValues;
            this.Timer.Start();
            Command = new DelegateCommand(ExecuteCommand, CanExecuteCommand);
        }

       

        private void VerifyValues(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
