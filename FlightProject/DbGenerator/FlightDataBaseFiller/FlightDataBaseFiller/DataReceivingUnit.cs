using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagerProject;
namespace FlightDataBaseFiller
{
    class DataReceivingUnit
    {

        public int NumberOfCustomers { get; set; }
        public int NumberOfAirlines { get; set; }
        public int NumberOfFlights { get; set; }
        public int NumberOfCountries { get; set; }
        public int NumberOfTicketsPerCustomer { get; set; }


        


        public DataReceivingUnit(int customersNum,int airlinesNum, int flightsNum, int countriesNum, int ticketsNum)
        {
            this.NumberOfCustomers = customersNum;
            this.NumberOfAirlines = airlinesNum;
            this.NumberOfCountries = countriesNum;
            this.NumberOfFlights = flightsNum;
            this.NumberOfTicketsPerCustomer = ticketsNum;
        }

        private List<Country> GetCountries()//to finish
        {
            GetRestCountries restCountries = new GetRestCountries();
            List<Country> filteredCountries = new List<Country>();
            List<Country> filteredCountries1 = restCountries.Countries.ToList();
            Random rnd = new Random();
            for (int i = 0; i < NumberOfCountries; i++)
            {
                filteredCountries.Add(restCountries.Countries[rnd.Next(0, restCountries.Countries.Count)]);
            }
            
            return filteredCountries;
        }

        private List<AirLine> GetAirLines()
        {
            AirlineFactory GetAirline = new AirlineFactory();
            List<Country> countries = GetCountries();
            List<AirLine> airLines = new List<AirLine>();
            foreach (Country country in countries)
            {
               airLines.Add(GetAirline.Generate(country));
            }
            return airLines;
        }


        private List<Customer> GetCustomers()
        {
            CustomerFactory GetCustomer = new CustomerFactory();
            List<Customer> customers = new List<Customer>();
            for (int i = 0; i < this.NumberOfCustomers; i++)
            {
                customers.Add(GetCustomer.CreateNew());
            }
            return customers;
        }


         private List<Flight> GetFlights(List<AirLine>airlines,List<Country>countries)//to finish
         {
            FlightFactory flightFactory = new FlightFactory();
            Random rnd = new Random();
            List<Flight> flights = new List<Flight>();
            foreach (AirLine airline in airlines)
            {
                for (int i = 0; i < NumberOfFlights; i++)
                {
                    try
                    {
                        flights.Add(flightFactory.Generate(airline, countries[rnd.Next(0, countries.Count)], countries[rnd.Next(0, countries.Count)]));
                    }
                    catch (Exception e)//rethink
                    {

                        ErrorLogger.Logger(e);
                        i--;
                    }
                }
            }
            return flights;
         }
        public void GenerateData()
        {
            List<Country> countries = GetCountries();
        }
    }

   
}
