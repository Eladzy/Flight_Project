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
using FlightDataBaseFiller.Helpers;

namespace FlightDataBaseFiller
{
    public class ViewModel : IDataErrorInfo, INotifyPropertyChanged
    {


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




        private int numCustomers;//bound to the user input

        public int NumCustomers
        {
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

        private int numAirlines;//bound to the user input
        public int NumAirlines
        {
            get
            {
                return numAirlines;
            }
            set
            {
                numAirlines = Math.Abs(value);
                NotifyPropertyChanged("numAirlines");
            }
        }


        private int numCountries;//bound to the user input
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

        private int numFlights;//bound to the user input
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

        private int ticketsPerCustomer;//bound to the user input
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

        public static ObservableCollection<string> Updates { get; set; }


        public DelegateCommand Command { get; set; }

        public string Error
        {
            get
            {
                return string.Empty;
            }
        }//IdataErrorInfo property

        public string this[string propertyName]
        {
            get
            {
                return GetErrorForProperty(propertyName);
            }
        }//IdataErrorInfo property


        /// <summary>
        /// presenting errors on the GUI if there is a mismatch in the user input values
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private string GetErrorForProperty(string propertyName)
        {
            return FieldValidationHelper.InputValidation(propertyName, NumCustomers, TicketsPerCustomer, NumFlights, NumAirlines, NumCountries);
            //string result = null;
            //switch (propertyName)
            //{
            //    case ("NumCustomers"):

            //        if (DataRatio())
            //        {
            //            result = "Tickets and customers ratio exceeds the amount of available tickets";
            //            return result;
            //        }
            //        result = string.Empty;
            //        return result;
            //    case ("NumAirlines"):

            //        result = string.Empty;
            //        return result;

            //    case ("NumCountries"):

            //        if (numCountries == 0)
            //        {
            //            result = "Select any amount of countries";
            //            return result;
            //        }
            //        else if (numCountries > 249)
            //        {
            //            result = "Maximum amount of countries is 249 ";

            //            return result;
            //        }

            //        result = string.Empty;
            //        return result; ;
            //    case ("NumFlights"):

            //        if (DataRatio())
            //        {
            //            result = "Tickets and customers ratio exceeds the amount of available tickets";
            //            return result;
            //        }
            //        result = string.Empty;
            //        return result;
            //    case ("TicketsPerCustomer"):

            //        if (DataRatio())
            //        {
            //            result = "Tickets and customers ratio exceeds the amount of available tickets";
            //            return result;
            //        }
            //        result = string.Empty;
            //        return result;
            //    default: return string.Empty; ;
            //}

        }

        private DispatcherTimer Timer = new DispatcherTimer();

        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModel()
        {
            IsEnabled = false;//watch
            NumCustomers = 0;
            Updates = new ObservableCollection<string>();
            Updates.Clear();
            this.Timer.Interval = TimeSpan.FromMilliseconds(500);
            this.Timer.Tick += VerifyValues;
            this.Timer.Start();
            Command = new DelegateCommand(ExecuteCommand).ObservesProperty(() => IsEnabled);
        }

        private void ExecuteCommand()
        {
            DataReceivingUnit receivingUnit = new DataReceivingUnit(NumCustomers, NumAirlines, NumFlights, NumCountries, TicketsPerCustomer);
            receivingUnit.GenerateDataAsync();
        }
        

        private void VerifyValues(object sender, EventArgs e)
        {
            //make sure the number of bought tickets does not exceed the overall tickets
            //FormValueValidation(this.NumAirlines,this.NumCountries,this.NumCustomers,this.NumFlights,this.TicketsPerCustomer) bool?
            IsEnabled = false;

        }

        private void NotifyPropertyChanged(String propertyName = "ErrorCollection")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        //bool DataRatio()
        //{
        //    if (NumCustomers * TicketsPerCustomer > NumFlights * NumAirlines * Int32.Parse(ConfigurationManager.AppSettings["TicketsPerFlight"]))
        //    {
        //        return true;
        //    }
        //    return false;
        //}

       
    }
}
