using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
        private static object Key = new object();




        public DataReceivingUnit(int customersNum, int airlinesNum, int flightsNum, int countriesNum, int ticketsNum)
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
            List<Country> countries = restCountries.Countries.ToList();
            Random rnd = new Random();
            NumberOfCountries = NumberOfCountries <= 249 ? NumberOfCountries : 249;
            for (int i = 0; i < NumberOfCountries; i++)
            {
                int index = rnd.Next(0, countries.Count);
                filteredCountries.Add(countries[index]);
                countries.RemoveAt(index);
            }

            return filteredCountries;
        }

        private List<AirLine> GetAirLines(List<Country> countries)
        {
            lock (Key)
            {
                Random rnd = new Random();
                AirlineFactory GetAirline = new AirlineFactory();
                List<AirLine> airLines = new List<AirLine>();
                for (int i = 0; i < NumberOfAirlines; i++)
                {
                    airLines.Add(GetAirline.Generate(countries[rnd.Next(1, countries.Count)]));
                }
                return airLines;
            }
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






        public async void GenerateDataAsync()
        {
            List<Country> countries = new List<Country>();
            List<Customer> customers = new List<Customer>();
            List<AirLine> airlines = new List<AirLine>();
            List<Flight> flights = new List<Flight>();

            await Task.Run(() =>
            {
                countries = GetCountries();
                customers = GetCustomers();
                airlines = GetAirLines(countries);
                Debug.WriteLine("datarecieving task");


            }).ContinueWith((Task t) => DataSendUnit.AddData(customers, airlines, countries, NumberOfFlights, NumberOfTicketsPerCustomer).Start());

        }
    }


}
