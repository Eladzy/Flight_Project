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
         public static Task AddData(List<Customer> customers, List<AirLine> airLines, List<Country> countries, List<Flight> flights, List<Ticket> tickets)
         {
            Task t = new Task(() => {
                //Addcountries();
                //AddAirlines();
                //AddFlights();
            });
            Task t1 = new Task(() => { });
            //todo Cancel customer auto-increment ID
            //Task StartAddToDb = new Task(() => {
            //    Debug.WriteLine("++++++++++++++++++++++add to db will be here!++++++++++++++++"); 
            //});
            //StartAddToDb.Start();
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
        private static void AddFlights(List<Flight>flights)
        {
            
        }
        private static void BuyTickets()
        {

        }
        
    }
}
