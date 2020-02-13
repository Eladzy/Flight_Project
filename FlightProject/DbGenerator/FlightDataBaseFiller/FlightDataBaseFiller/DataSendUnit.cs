using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagerProject;

namespace FlightDataBaseFiller
{
    public static class DataSendUnit
    {
        private static LoginToken<Administrator> Token;
        private static LoggedInAdminFacade Facade;
       static DataSendUnit()
       {
            Administrator administrator = new Administrator();

            Token = new LoginToken<Administrator>
            {
                User = administrator
            };
            Facade = (LoggedInAdminFacade)FlightCenter.GetInstance().GetFacade(Token);
       }
        public static Task AddData(List<Customer> customers, List<AirLine> airLines, List<Country> countries, List<Flight> flights, int ticketsPerCustomer)
        {
            Task t = new Task(() =>
            {
                Debug.WriteLine("1st add data task");
                Addcountries(countries);
                AddAirlines(airLines);
                AddCustomers(customers);
                AddFlights(flights, airLines);
                BuyTickets(ticketsPerCustomer, customers, flights);
            });
           
            return t;
         }
        private static void Addcountries(List<Country>countries)
        {
            countries.ForEach(c => Facade.AddCountry(Token, c));
        }
        private static void AddCustomers(List<Customer>customers)
        {
            customers.ForEach(c => Facade.CreateNewCustomer(Token, c));
        }
        private static void AddAirlines(List<AirLine>airLines)
        {
            airLines.ForEach(a => Facade.CreateNewAirline(Token, a));
        }
        private static void AddFlights(List<Flight>flights, List<AirLine> airLines)
        {
            foreach(AirLine airLine in airLines)
            {
                LoginToken<AirLine> airlineToken = new LoginToken<AirLine>
                {
                    User = airLine
                };
                var airlineFacade = (LoggedInAirLineFacade)FlightCenter.GetInstance().GetFacade(airlineToken);
                List<Flight> filteredFlights = flights.Where(f => f.AirLine_Id == airLine.Id).ToList();
                filteredFlights.ForEach(f => airlineFacade.CreateFlight(airlineToken, f));//keep an eye
            }
            
        }
        private static void BuyTickets(int ticketsPerCustomer,List<Customer>customers,List<Flight>flights)
        {
           
                Debug.WriteLine("final task");
                if (customers.Count == 0 || flights.Count == 0)
                    return;
                List<Ticket> tickets = new List<Ticket>();
                Random rnd = new Random();
            foreach (Customer customer in customers)
            {

                for (int i = 0; i < ticketsPerCustomer; i++)
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

        }
        
    }
}
