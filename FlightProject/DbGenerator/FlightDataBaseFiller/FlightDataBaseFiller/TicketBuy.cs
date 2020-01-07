using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagerProject;

namespace FlightDataBaseFiller
{
    class TicketBuy
    {

        private LoginService GetLoginService;
        
        public int TicketsPerCustomer { get; set; }

        public List<Flight> TotalFlights { get; set; }

        public List<Customer> TotalCustomers { get; set; }
        //to figure out what to do in exception scenario

        public TicketBuy()
        {
            GetLoginService = new LoginService();
        }

        public void TicketAquisition()
        {
            foreach (Customer c in TotalCustomers)
            {
                int ticketsPerCustomer = this.TicketsPerCustomer;
                while (ticketsPerCustomer > 0)
                {
                    List<Flight> flights = this.TotalFlights;//to cancel?
                    LoginToken<Customer> token;

                    if(GetLoginService.TryCustomerLogin(c.User_Name,c.Password,out token))
                    { 
                        try
                        {
                            MakePurchase(flights, token);
                        }
                        catch (ExceptionTicketSoldOut e)
                        {

                            ErrorLogger.Logger(e);
                            ticketsPerCustomer++;
                        }
                        catch(ExceptionFlightNotFound e)
                        {
                            ErrorLogger.Logger(e);
                            ticketsPerCustomer++;
                        }
                        catch(ExceptionTicketNotFound e)
                        {
                            ErrorLogger.Logger(e);
                            ticketsPerCustomer++;
                        }
                        catch (Exception e)
                        {
                            ErrorLogger.Logger(e);
                            System.Windows.MessageBox.Show("Error has Ocurred","Error",System.Windows.MessageBoxButton.OK,System.Windows.MessageBoxImage.Error);
                            return;
                        }
                        finally
                        {
                            ticketsPerCustomer--;
                        }
                       
                       
                    }
                }
            }
        }

        private static void MakePurchase(List<Flight> flights, LoginToken<Customer> token)
        {
            Random rnd = new Random();
            var getCustomerFacade = (LoggedInCustomerFacade)FlightCenter.GetInstance().GetFacade(token);
            getCustomerFacade.PurchaseTicket(token, flights[rnd.Next(0, flights.Count)]);
        }
    }
}
