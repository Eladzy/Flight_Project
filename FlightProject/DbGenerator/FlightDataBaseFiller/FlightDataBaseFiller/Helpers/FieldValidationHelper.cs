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
        private static bool DataRatio(int NumCustomers,int TicketsPerCustomer,int NumFlights,int NumAirlines)
        {
            if (NumCustomers * TicketsPerCustomer > NumFlights * NumAirlines * Int32.Parse(ConfigurationManager.AppSettings["TicketsPerFlight"]))
            {
                return true;
            }
            return false;
        }
        public static string InputValidation(string propertyName, int NumCustomers, int TicketsPerCustomer, int NumFlights, int NumAirlines,int numCountries)
        {
            //connect with dictionary to make sure error values are empty
            string result = null;
            switch (propertyName)
            {
                case ("NumCustomers"):

                    if (DataRatio( NumCustomers, TicketsPerCustomer,NumFlights, NumAirlines))
                    {
                        result = "Tickets and customers ratio exceeds the amount of available tickets";
                        return result;
                    }
                    result = string.Empty;
                    return result;
                case ("NumAirlines"):

                    result = string.Empty;
                    return result;

                case ("NumCountries"):

                    if (numCountries == 0)
                    {
                        result = "Select any amount of countries";
                        return result;
                    }
                    else if (numCountries > 249)
                    {
                        result = "Maximum amount of countries is 249 ";

                        return result;
                    }

                    result = string.Empty;
                    return result; ;
                case ("NumFlights"):

                    if (DataRatio(NumCustomers, TicketsPerCustomer, NumFlights, NumAirlines)||(NumFlights>0&&NumAirlines==0))
                    {
                        result = "Tickets and customers ratio exceeds the amount of available tickets";
                        return result;
                    }
                    result = string.Empty;
                    return result;
                case ("TicketsPerCustomer"):

                    if (DataRatio(NumCustomers, TicketsPerCustomer, NumFlights, NumAirlines))
                    {
                        result = "Tickets and customers ratio exceeds the amount of available tickets";
                        return result;
                    }
                    result = string.Empty;
                    return result;
                default: return string.Empty; ;
            }
        }
    }
}
