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

        CustomerFactory GetCustomer = new CustomerFactory();

        AirlineFactory GetAirline = new AirlineFactory();

        


        public DataReceivingUnit(int customersNum,int airlinesNum, int flightsNum, int countriesNum, int ticketsNum)
        {
            this.NumberOfCustomers = customersNum;
            this.NumberOfAirlines = airlinesNum;
            this.NumberOfCountries = countriesNum;
            this.NumberOfFlights = flightsNum;
            this.NumberOfTicketsPerCustomer = ticketsNum;
        }

        public List<Country> GetCountries()//to finish
        {
            GetRestCountries restCountries = new GetRestCountries();
            List<Country> filteredCountries = new List<Country>();
            List<Country> filteredCountries1 = restCountries.Countries.ToList();
            Random rnd = new Random();
            for (int i = 0; i < NumberOfCountries; i++)
            {
                filteredCountries.Add(restCountries.Countries[rnd.Next(0, restCountries.Countries.Count)]);
            }
            //int counter = 0;
            //while (filteredCountries1.Count != NumberOfCountries)
            //{
            //    foreach (Country country in filteredCountries1)
            //    {
            //        filteredCountries1.RemoveAt(rnd.Next(0, filteredCountries1.Count));
            //    }
            //}
            return filteredCountries;
        }

        public List<AirLine> GetAirLines()
        {
            List<Country> countries = GetCountries();
            List<AirLine> airLines = new List<AirLine>();
            foreach (Country country in countries)
            {
               airLines.Add(GetAirline.Generate(country));
            }
            return airLines;
        }


        public List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();
            for (int i = 0; i < this.NumberOfCustomers; i++)
            {
                customers.Add(GetCustomer.CreateNew());
            }
            return customers;
        }


        public List<Flight> GetFlights()//to finish
        {
            List<Flight> flights = new List<Flight>();
            return flights;
        }
    }
}
