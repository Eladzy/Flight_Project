﻿using System;
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
        private static AnonymousFacade _anonymousFacade = new AnonymousFacade();
        static DataSendUnit()
        {
            Administrator administrator = new Administrator();

            Token = new LoginToken<Administrator>
            {
                User = administrator
            };
            Facade = (LoggedInAdminFacade)FlightCenter.GetInstance().GetFacade(Token);
        }
        public static Task AddData(List<Customer> customers, List<AirLine> airLines, List<Country> countries, int numberOfFlights, int ticketsPerCustomer)
        {
            Task t = new Task(() =>
            {
                Debug.WriteLine("1st add data task");
                try
                {
                    Addcountries(countries);
                    ProgressUpdate.TransferMessage("...Countries added");
                }
                catch (Exception e)
                {

                    ProgressUpdate.TransferMessage("Error: addeing countries failed" + e.Message);
                }
                Debug.WriteLine("1st add data task");
                try
                {
                    AddAirlines(airLines);
                    ProgressUpdate.TransferMessage("...Airlines added");
                }
                catch (Exception e)
                {

                    ProgressUpdate.TransferMessage("Error: Adding airlines failed" + e.Message);
                }
                Debug.WriteLine("1st add data task");
                try
                {
                    AddCustomers(customers);
                    ProgressUpdate.TransferMessage("...Customers added");
                }
                catch (Exception e)
                {

                    ProgressUpdate.TransferMessage("Error: Adding Customers failed" + e.Message);
                }
                try
                {
                    List<Flight> flights = AddFlights(countries, airLines, numberOfFlights);
                    ProgressUpdate.TransferMessage("...Flights added");
                    try
                    {
                        BuyTickets(ticketsPerCustomer, customers, flights);
                        ProgressUpdate.TransferMessage("...Tickets bought");
                    }
                    catch (Exception ex)
                    {

                        ProgressUpdate.TransferMessage("Error: Buying tickets failed code" + ex.Message);
                    }

                }
                catch (Exception e)
                {

                    ProgressUpdate.TransferMessage("Error: Flights creation failed" + e.Message);
                }

            });

            return t;
        }
        private static void Addcountries(List<Country> countries)//ok
        {
            countries.ForEach(c => Facade.AddCountry(Token, c));
        }
        private static void AddCustomers(List<Customer> customers)//ok
        {
            customers.ForEach(c => Facade.CreateNewCustomer(Token, c));
        }
        private static void AddAirlines(List<AirLine> airLines)
        {
            airLines.ForEach(a => Facade.CreateNewAirline(Token, a));//ok
        }


        private static List<Flight> AddFlights(List<Country> countries, List<AirLine> airLines, int numberOfFlights)
        {
            FlightFactory flightFactory = new FlightFactory();
            Random rnd = new Random();
            List<AirLine> airlinesTemp = new List<AirLine>();
            List<Flight> flights = new List<Flight>();
            try
            {
                airLines.ForEach(a => airlinesTemp.Add(Facade.GetAirlineByUser(Token, a.User_Name)));
            }
            catch (Exception e)
            {
                ErrorLogger.Logger(e);
                throw;
            }
            foreach (AirLine airline in airlinesTemp)
            {
                LoginToken<AirLine> airlineToken = new LoginToken<AirLine>
                {
                    User = airline
                };
                var airlineFacade = (LoggedInAirLineFacade)FlightCenter.GetInstance().GetFacade(airlineToken);
                for (int i = 0; i < numberOfFlights; i++)
                {
                    try
                    {
                        Flight flight = flightFactory.Generate(airline, countries[rnd.Next(0, countries.Count)], countries[rnd.Next(0, countries.Count)]);
                        airlineFacade.CreateFlight(airlineToken, flight);
                        flights.Add(flight);
                    }
                    catch (Exception e)
                    {

                        ErrorLogger.Logger(e);
                        i--;
                    }
                }
            }
            return _anonymousFacade.GetAllFlights().ToList();
        }
        private static void BuyTickets(int ticketsPerCustomer, List<Customer> customers, List<Flight> flights)
        {
            List<Customer> customersTemp = new List<Customer>();
            customers.ForEach(c => customersTemp.Add(Facade.GetCustomerByUser(Token, c.User_Name)));
            if (customers.Count == 0 || flights.Count == 0)
                return;
            List<Ticket> tickets = new List<Ticket>();
            Random rnd = new Random();
            foreach (Customer customer in customersTemp)
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
