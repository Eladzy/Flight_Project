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

namespace FlightDataBaseFiller
{
    public class ViewModel: IDataErrorInfo,INotifyPropertyChanged
    {
        Dispatcher ViewModelDispatcher { get; set; }

        private bool isEnabled;
        public bool IsEnabled
        {
            get
            {
                return isEnabled;
            }
            set
            {
                isEnabled = value;    
            }
        }

        private Dictionary<string, string> errorCollection
        {
            get
            {
                return ErrorCollection;
            }
            set
            {
                ErrorCollection = value;
                NotifyPropertyChanged("ErrorCollection");
            }
        }

        public Dictionary<string, string> ErrorCollection { get; set; }
       

        public  ObservableCollection<string> Status = new ObservableCollection<string>();

        private int numCustomers;
        public int NumCustomers {
            get
            {
                return numCustomers;
            }
            set
            {
                numCustomers = Math.Abs(value);
                NotifyPropertyChanged("numCustomers");
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
            string result = null;
            switch (propertyName)
            {
                case ("NumCustomers") :
                    if (!ErrorCollection.ContainsKey(propertyName))
                    {
                        ErrorCollection.Add(propertyName, "");
                    }
                    if (NumCustomers * TicketsPerCustomer > NumFlights * NumAirlines* int.Parse(ConfigurationManager.AppSettings["TicketsPerFlight"]))
                    {
                        result = "Tickets and customers ratio exceeds the amount of available tickets";
                        ErrorCollection[propertyName] = result;
                        return result;
                    }                
                    result=string.Empty;
                    ErrorCollection[propertyName]= result;
                    return result;
                case ("NumAirlines"):
                    if (!ErrorCollection.ContainsKey(propertyName))
                    {
                        ErrorCollection.Add(propertyName, "");
                    }               
                    result = string.Empty;
                    ErrorCollection[propertyName] = result;
                    return result;

                case ("NumCountries"):
                    if (!ErrorCollection.ContainsKey(propertyName))
                    {
                        ErrorCollection.Add(propertyName, "");
                    }
                    if (numCountries == 0)
                    {
                        result= "Select any amount of countries";

                        ErrorCollection[propertyName] = result;
                        return result; 
                    }
                    else if(numCountries > 249)
                    {
                        result = "Maximum amount of countries is 249 ";

                        ErrorCollection[propertyName] = result;
                        return result;
                    }
                      
                    result = string.Empty;
                    ErrorCollection[propertyName] = result;
                    return result; ;
                case ("NumFlights"):
                    if (!ErrorCollection.ContainsKey(propertyName))
                    {
                        ErrorCollection.Add(propertyName, "");
                    }
                    if (NumCustomers * TicketsPerCustomer > NumFlights * NumAirlines * NumAirlines * Int32.Parse(ConfigurationManager.AppSettings["TicketsPerFlight"]))
                    {
                        result = "Tickets and customers ratio exceeds the amount of available tickets";
                        ErrorCollection[propertyName] = result;
                        return result;
                    }
                   
                    result = string.Empty;
                    ErrorCollection[propertyName] = result;
                    return result;
                case ("TicketsPerCustomer"):
                    if (!ErrorCollection.ContainsKey(propertyName))
                    {
                        ErrorCollection.Add(propertyName, "");
                    }
                    if (NumCustomers * TicketsPerCustomer > NumFlights * NumAirlines * NumAirlines * Int32.Parse(ConfigurationManager.AppSettings["TicketsPerFlight"]))
                    {
                        result = "Tickets and customers ratio exceeds the amount of available tickets";
                        ErrorCollection[propertyName] = result;
                        return result; 
                    }
                    result = string.Empty;
                    ErrorCollection[propertyName] = result;
                    return result;
                default: return string.Empty; ;
            }
           
        }

        private DispatcherTimer Timer=new DispatcherTimer();

        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModel()
        {
            this.ErrorCollection = new Dictionary<string, string>
            {
                  {"NumCustomers","" },{"NumAirlines",""},{"NumFlights",""},{"NumCountries",""},{"TicketsPerCustomer",""}
            };            
            this.Timer.Interval = TimeSpan.FromMilliseconds(500);
            this.Timer.Tick += VerifyValues;
            this.Timer.Start();
            Command = new DelegateCommand(ExecuteCommand).ObservesProperty(()=>IsEnabled);
        }

        private void ExecuteCommand()
        {
            DataReceivingUnit receivingUnit = new DataReceivingUnit(NumCustomers,NumAirlines,NumFlights,NumCountries,TicketsPerCustomer);
            receivingUnit.GenerateDataAsync();
        }


        private void VerifyValues(object sender, EventArgs e)
        {
            //make sure the number of bought tickets does not exceed the overall tickets
            //FormValueValidation(this.NumAirlines,this.NumCountries,this.NumCustomers,this.NumFlights,this.TicketsPerCustomer) bool?
            IsEnabled = ErrorCollection.All(pair => string.IsNullOrEmpty(pair.Value));
           
        }

        private void NotifyPropertyChanged( String propertyName = "ErrorCollection")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
