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
            List<Country> countries = restCountries.Countries.ToList();
            Random rnd = new Random();
            for (int i = 0; i < NumberOfCountries; i++)
            {
                int index = rnd.Next(0, countries.Count);
                filteredCountries.Add(countries[index]);
                countries.RemoveAt(index);
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

        private List<Ticket> GetTickets(List<Customer> customers, List<Flight> flights)
        {
            List<Ticket> tickets = new List<Ticket>();
            Random rnd = new Random();
            foreach(Customer customer in customers)
            {
                
                for (int i = 0; i < NumberOfTicketsPerCustomer; i++)
                {
                    int flightIndex = rnd.Next(0, flights.Count);
                    try
                    {
                       tickets.Add(TicketFactory.GenerateTicket(customer, flights[flightIndex]));
                    }
                    catch (ExceptionTicketSoldOut e)
                    {
                        ErrorLogger.Logger(e);

                        flights.RemoveAt(flightIndex);
                        i++;
                    }
                    catch (ExceptionFlightNotFound e)
                    {
                        ErrorLogger.Logger(e);

                        flights.RemoveAt(flightIndex);
                        i++;
                    }
                    catch (Exception e)
                    {
                        ErrorLogger.Logger(e);
                        throw e;
                    }
                }
            }
            return tickets;
        }

        public void GenerateData()//todo
        {
            List<Country> countries = GetCountries();
            List<Customer> customers = GetCustomers();
            List<AirLine> airlines = GetAirLines();
            List<Flight> flights = GetFlights(airlines,countries);
            List<Ticket> tickets = GetTickets(customers, flights);
            //purchase
          //send to db
        }
    }

   
}
