using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataBaseFiller.Helpers
{
    static class FieldValidationHelper
    {
        public static Dictionary<string, string> ErrorCollection = new Dictionary<string, string>() 
        { { "numCustomers","" },{ "ticketsPerCustomer","" },{ "numFlights","" },{ "numAirlines","" },{ "numCountries","" } };

        private static bool DataRatio(int numCustomers,int ticketsPerCustomer,int numFlights,int numAirlines)
        {
            if (numCustomers * ticketsPerCustomer > numFlights * numAirlines * Int32.Parse(ConfigurationManager.AppSettings["TicketsPerFlight"]))
            {
                return true;
            }
            return false;
        }

        

        public static string InputValidation(string propertyName, int numCustomers, int ticketsPerCustomer, int numFlights, int numAirlines,int numCountries)
        {
          
            string result = null;
            switch (propertyName)
            {
                case ("NumCustomers"):

                    if (DataRatio( numCustomers, ticketsPerCustomer,numFlights, numAirlines))
                    {
                        result = "Tickets and customers ratio exceeds the amount of available tickets";
                        ErrorCollection[propertyName] = result;
                        return result;
                    }
                    result = string.Empty;
                    ErrorCollection[propertyName] = result;
                    return result;
                case ("NumAirlines"):

                    result = string.Empty;

                    return result;

                case ("NumCountries"):

                    if (numCountries == 0)
                    {
                        result = "Select any amount of countries";
                        ErrorCollection[propertyName] = result;
                        return result;
                    }
                    else if (numCountries > 249)
                    {
                        result = "Maximum amount of countries is 249 ";
                        ErrorCollection[propertyName] = result;
                        return result;
                    }

                    result = string.Empty;
                    ErrorCollection[propertyName] = result;
                    return result; ;
                case ("NumFlights"):

                    if (DataRatio(numCustomers, ticketsPerCustomer, numFlights, numAirlines)||(numFlights>0&&numAirlines==0))
                    {
                        result = "Tickets and customers ratio exceeds the amount of available tickets";
                        ErrorCollection[propertyName] = result;
                        return result;
                    }
                    result = string.Empty;
                    ErrorCollection[propertyName] = result;
                    return result;
                case ("TicketsPerCustomer"):

                    if (DataRatio(numCustomers, ticketsPerCustomer, numFlights, numAirlines))
                    {
                        result = "Tickets and customers ratio exceeds the amount of available tickets";
                        ErrorCollection[propertyName] = result;
                        return result;
                    }
                    result = string.Empty;
                    ErrorCollection[propertyName] = result;
                    return result;
                default: return string.Empty; 
            }
        }
    }
}
