using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Prism;
using Prism.Commands;

namespace FlightDataBaseFiller//inotifypropertychanged
{
    public class ViewModel
    {
        Dispatcher dispatcher { get; set; }

        public int NumCustomers { get; set; }

        public int NumAirlines { get; set; }

        public int NumCountries { get; set; }

        public int NumFlights { get; set; }

        public int TicketsPerCustomer { get; set; }

        public DelegateCommand Command { get; set; }
        private DispatcherTimer Timer=new DispatcherTimer();
        public ViewModel()
        {  
            this.Timer.Interval = TimeSpan.FromMilliseconds(500);
            this.Timer.Tick += VerifyValues;
            this.Timer.Start();
            Command = new DelegateCommand(ExecuteCommand, CanExecuteCommand);
        }

        private void ExecuteCommand()
        {
           // throw new NotImplementedException();
        }

        private bool CanExecuteCommand()
        {
            return false;
        }

        private void VerifyValues(object sender, EventArgs e)
        {
           //make sure the number of bought tickets does not exceed the overall tickets
           FormValueValidation(this.NumAirlines,this.NumCountries,this.NumCustomers,this.NumFlights,this.TicketsPerCustomer) bool?
           
        }
    }
}
