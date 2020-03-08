using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FlightManagerProject;

namespace FlightDataBaseFiller
{
   public class CustomerDictionaries
   {
       
       static string[] privateNames = new string[]
        {
         "John","Jenna","Ariel","Maya","David","Daniel","Rachel","Ross","Monica"
            ,"Ronald","Harry","Gerard","Jacob","Michelle","Mike","Thomas","Chloe","Heather","Nathan","Nathaly","Arnold","Henry","Emma","Haward","Erik","Christine"
        };
       static  string[] lastNames = new string[] 
        {
            "Jefferson","Gallagher","Andersson","Miles","Grant","Goldberg","Lawrence","Barnes","Prescott","Smith","Adams","Brown","Stone","Watson","Stein"
        };
       static Random rnd = new Random();

        public static string GetPrivateName()
        {
            return privateNames[rnd.Next(0, privateNames.Length)];
        }
        public static string GetLastName()
        {
            return lastNames[rnd.Next(0, lastNames.Length)];
        }

        public static string GenerateUserName(string firstName, string lastName)
        {
            return privateNames[rnd.Next(0, privateNames.Length)]+rnd.Next(10,1000);
        }

    }
}
