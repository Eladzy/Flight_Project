using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Prism;
using Prism.Commands;
using System.ComponentModel;
using System.Configuration;
using System.Collections.ObjectModel;

namespace FlightDataBaseFiller//inotifypropertychanged
{
    public class ViewModel:IDataErrorInfo
    {
        Dispatcher dispatcher { get; set; }

        public ObservableCollection<string> Status = new ObservableCollection<string>();

        private int numCustomers;
        public int NumCustomers {
            get
            {
                return numCustomers;
            }
            set
            {
                numCustomers = Math.Abs(value);
            } 
        }

        private int numAirlines;
        public int NumAirlines
        {
            get
            {
                return numAirlines;
            }
            set
            {
                numAirlines = Math.Abs(value);
            }
        }


        private int numCountries;
        public int NumCountries
        {
            get
            {
                return numCountries;
            }
            set
            {
                numCountries = Math.Abs(value);
            }
        }

        private int numFlights;
        public int NumFlights
        {
            get
            {
                return numFlights;
            }
            set
            {
                numFlights = Math.Abs(value);
            }
        }

        private int ticketsPerCustomer;
        public int TicketsPerCustomer
        {
            get
            {
                return ticketsPerCustomer;
            }
            set
            {
                ticketsPerCustomer = Math.Abs(value);
            }
        }

        public DelegateCommand Command { get; set; }

        public string Error
        {
            get
            {
                return string.Empty;
            }
        }

        public string this[string propertyName]
        {
            get
            {
                return GetErrorForProperty(propertyName);
            }
        }

        private string GetErrorForProperty(string propertyName)
        {
           
            switch (propertyName)
            {
                case ("NumCustomers") :
                    if (NumCustomers * TicketsPerCustomer > NumFlights * NumAirlines* int.Parse(ConfigurationManager.AppSettings["TicketsPerFlight"]))
                    {
                        return "Tickets and customers ratio exceeds the amount of available tickets";
                    }
                    //else if (NumCustomers == 0)
                    //{
                    //    return "Amount of customers must be filled";
                    //}
                    return string.Empty;
                case ("NumAirlines"):
                    //if (NumAirlines == 0)
                    //{
                    //    return "Select amount of airline companies";
                    //}
                    //else if()
                    return string.Empty;
                    
                case ("NumCountries"):
                    if (numCountries == 0)
                        return "Select any amount of countries";
                        return string.Empty;
                case ("NumFlights"):
                    if (NumCustomers * TicketsPerCustomer > NumFlights * NumAirlines * NumAirlines * Int32.Parse(ConfigurationManager.AppSettings["TicketsPerFlight"]))
                    {
                        return "Tickets and customers ratio exceeds the amount of available tickets";
                    }
                    //if (NumFlights == 0)
                    //    return "Select amount of flights";
                    else return string.Empty;
                case ("TicketsPerCustomer"):
                    if (NumCustomers * TicketsPerCustomer > NumFlights * NumAirlines * NumAirlines * Int32.Parse(ConfigurationManager.AppSettings["TicketsPerFlight"]))
                    {
                        return "Tickets and customers ratio exceeds the amount of available tickets";
                    }
                    return string.Empty;
                default: return string.Empty; ;
            }
           
        }

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
           //FormValueValidation(this.NumAirlines,this.NumCountries,this.NumCustomers,this.NumFlights,this.TicketsPerCustomer) bool?
           
        }
    }
}
