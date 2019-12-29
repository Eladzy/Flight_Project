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
        private string FirstName { get; set; }
        private string LastName { get; set; }
        private string UserName { get; set; }
        private string CustomerPhone { get; set; }
        private string CustomerPassword { get; set; }
        private string CustomerAddress { get; set; }
        private string CustomerCreditCard { get; set; }


        public void Init()
        {
            this.FirstName = CustomerDictionaries.GetPrivateName();
            this.LastName = CustomerDictionaries.GetLastName();
            this.UserName = FirstName + LastName + GeneralDataGenerator.Generator(6);
            this.CustomerPhone = GeneralDataGenerator.NumericGenerator(10);
            this.CustomerPassword = GeneralDataGenerator.Generator(8);
            this.CustomerAddress = GeneralDataGenerator.Generator(12);
            this.CustomerCreditCard = GeneralDataGenerator.NumericGenerator(16);
        }


        public  Customer CreateNew()
        {
            Init();
            Customer c = new Customer
            {
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
