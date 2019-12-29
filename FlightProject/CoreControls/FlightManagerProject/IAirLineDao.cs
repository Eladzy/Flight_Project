using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagerProject
{
   public interface IAirLineDao : IBasic<AirLine>
    {
        AirLine GetAirLineByUserName(string username);
        IList<AirLine> GetAllAirLinesByCountry(long countryId);
    }
}
