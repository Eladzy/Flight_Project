using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagerProject;

namespace FlightDataBaseFiller
{
    class CustomerFactory
    {
        //private long ID;
        private string FirstName;
        private string LastName;
        private string UserName;
        private string CustomerPhone;
        private string CustomerPassword;
        private string CustomerAddress;
        private string CustomerCreditCard;


        public void Init()
        {
          //  this.ID = long.Parse(GeneralDataGenerator.NumericGenerator(18));
            this.FirstName = CustomerDictionaries.GetPrivateName();
            this.LastName = CustomerDictionaries.GetLastName();
            this.UserName = FirstName + LastName + GeneralDataGenerator.Generator(6);
            this.CustomerPhone =GeneralDataGenerator.NumericGenerator(10);
            this.CustomerPassword = GeneralDataGenerator.Generator(8);
            this.CustomerAddress = GeneralDataGenerator.Generator(12);
            this.CustomerCreditCard = GeneralDataGenerator.NumericGenerator(16);
        }


        public  Customer CreateNew()
        {
            Init();
            Customer c = new Customer
            {
               // Id = this.ID,
                First_Name = this.FirstName,
                Last_Name = this.LastName,
                User_Name = this.UserName,
                Phone_Number= this.CustomerPhone,
                Password= this.CustomerPassword,
                Address=this.CustomerAddress,
                Credit_Card_Number=this.CustomerCreditCard       
            };
            return c;
        }
    }
}
