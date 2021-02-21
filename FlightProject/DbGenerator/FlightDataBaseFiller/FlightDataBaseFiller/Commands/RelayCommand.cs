using FlightDataBaseFiller.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlightDataBaseFiller.Commands
{
    class RelayCommand : ICommand
    {
        string propertyName; int NumCustomers; int TicketsPerCustomer; int NumFlights; int NumAirlines; int numCountries;

        public RelayCommand(string propertyName, int numCustomers, int ticketsPerCustomer, int numFlights, int numAirlines, int numCountries)
        {
            this.propertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
            NumCustomers = numCustomers;
            TicketsPerCustomer = ticketsPerCustomer;
            NumFlights = numFlights;
            NumAirlines = numAirlines;
            this.numCountries = numCountries;
        }


        //public RelayCommand(string propertyName, int NumCustomers, int TicketsPerCustomer, int NumFlights, int NumAirlines, int numCountries)
        //{

        //}

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return FieldValidationHelper.InputValidation(propertyName, NumCustomers, TicketsPerCustomer, NumFlights, NumAirlines, numCountries)!=null?true:false;

        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
