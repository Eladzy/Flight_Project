using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagerProject
{
    public class Customer : IPoco, IUser
    {
        public long Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string User_Name { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Phone_Number { get; set; }
        public string Credit_Card_Number { get; set; }

        public Customer()
        {

        }
        private Customer(long id, string first_Name, string last_Name, string user_Name, string password, string address, string phone_Number, string credit_Card_Number)
        {
            Id = id;
            First_Name = first_Name;
            Last_Name = last_Name;
            User_Name = user_Name;
            Password = password;
            Address = address;
            Phone_Number = phone_Number;
            Credit_Card_Number = credit_Card_Number;
        }

        public Customer(string first_Name, string last_Name, string user_Name, string password, string address, string phone_Number, string credit_Card_Number)
        {
            First_Name = first_Name;
            Last_Name = last_Name;
            User_Name = user_Name;
            Password = password;
            Address = address;
            Phone_Number = phone_Number;
            Credit_Card_Number = credit_Card_Number;
        }

        public static bool operator ==(Customer customer1, Customer customer2)
        {
            if (Equals(customer1, null) && Equals(customer2, null))
                return true;
            if (Equals(customer1, null) || Equals(customer2, null))
                return false;
            if (Equals(customer1.Id, customer2.Id))
                return true;
            return false;
        }
        public static bool operator !=(Customer customer1, Customer customer2)
        {
            return !(customer1 == customer2);
        }
        public override bool Equals(object obj)
        {
            if (this == null & obj == null)
                return true;
            if (this == null || obj == null)
                return false;
            if (obj is Customer)
            {
                var obj1 = obj as Customer;
                return (Id == obj1.Id);
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            return (int)Id;
        }
    }
}
