using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace FlightManagerProject
{
    public class FlightCenter
    {
        private static FlightCenter _instance;
        private static readonly object key = new object();
        private static System.Timers.Timer timer;
        /// <summary>
        /// private ctor starts the time count
        /// </summary>
        private FlightCenter()
        {
            timer = new System.Timers.Timer
            {
                Interval = ConfigurationUtils.CLEAN_DB_INTERVAL
            };
            timer.Elapsed += CheckTime;
        }
        /// <summary>
        /// returns to correct facade
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public  IAnonymousFacade GetFacade(ILoginTokenBase token)
        {
            AnonymousFacade anonymousFacade = new AnonymousFacade();
            if (token == null)
            {
                return anonymousFacade;
            }

            if (token.GetUser() is Administrator)
           {
                LoggedInAdminFacade adminFacade = new LoggedInAdminFacade();
                return adminFacade;
           }
            else if(token.GetUser() is Customer)
            {
                LoggedInCustomerFacade customerFacade = new LoggedInCustomerFacade();
                return customerFacade;
            }
           else if(token.GetUser() is AirLine)
           {
                LoggedInAirLineFacade airLineFacade = new LoggedInAirLineFacade();
                return airLineFacade;
           }
            return anonymousFacade;

        }
        /// <summary>
        /// Make sure that only 1 instance is being used at a time
        /// </summary>
        /// <returns></returns>
        public static FlightCenter GetInstance()
        {
           if(_instance==null)
           {
                lock (key)
                {
                    if (_instance == null)
                    {
                        _instance = new FlightCenter();
                    }
                }
           }
           return _instance;
        }

        /// <summary>
        /// applies the daily clean up tool at the correct time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void CheckTime(object sender,EventArgs args)
        {
            DailyCLeanUpTool.GetInstance().MoveToHistory();             
        }
    }
}
