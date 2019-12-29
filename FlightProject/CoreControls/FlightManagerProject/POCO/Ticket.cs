using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagerProject
{

    public class Ticket : IPoco
    {
        public long Id { get; set; }
        public long Flight_Id { get; set; }
        public long Customer_Id { get; set; }
        public Ticket()
        {

        }
        public Ticket(long id, long flight_Id, long customer_Id)
        {
            Id = id;
            Flight_Id = flight_Id;
            Customer_Id = customer_Id;
        }
        public Ticket( long flight_Id, long customer_Id)
        {
            Flight_Id = flight_Id;
            Customer_Id = customer_Id;
        }
        public static bool operator ==(Ticket ticket1, Ticket ticket2)
        {
            if (Equals(ticket1, null) && Equals(ticket2, null))
                return true;
            if (Equals(ticket1, null) || Equals(ticket2, null))
                return false;
            if (Equals(ticket1.Id, ticket2.Id))
                return true;
            return false;
        }
        public static bool operator !=(Ticket ticket1, Ticket ticket2)
        {
            return !(ticket1 == ticket2);
        }
        public override bool Equals(object obj)
        {
            if (this == null & obj == null)
                return true;
            if (this == null || obj == null)
                return false;
            if (obj is Ticket)
            {
                var obj1 = obj as Ticket;
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