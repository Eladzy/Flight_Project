using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagerProject;

namespace FlightDataBaseFiller
{
    class AirlineFactory//todo after countries
    {      
        private long AlId;
        private long AlCountryCode;
        private string AlAirLineName;
        private string AlUserName;
        private string AlPassword;

        private  void Init(Country country)
        {

            this.AlId = long.Parse(GeneralDataGenerator.NumericGenerator(18));   

            this.AlCountryCode = country.Id;

            this.AlAirLineName = AirlineDictionary.GetPrefix() + country.Country_Name + GeneralDataGenerator.NumericGenerator(4);

            this.AlUserName = AlAirLineName + GeneralDataGenerator.Generator(3);

            this.AlPassword = GeneralDataGenerator.Generator(8);
          
        }
        public  AirLine Generate(Country country)
        {
            Init(country);

            AirLine a = new AirLine
            {
                Id = this.AlId,
                AirLine_Name = this.AlAirLineName,
                User_Name = this.AlUserName,
                CountryCode = this.AlCountryCode,
                Password = this.AlPassword

            };
            return a;
        }
    }
}

