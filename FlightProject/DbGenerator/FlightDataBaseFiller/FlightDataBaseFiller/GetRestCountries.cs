using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web;
using System.Net;
using FlightManagerProject;

namespace FlightDataBaseFiller
{
    class GetRestCountries
    {
        readonly static string Url = "https://restcountries.eu/rest/v2"; 

        public List<Country> Countries
        {
            get
            {
                GetData().GetAwaiter();
                return Countries;
            }
          private  set { Countries = value; }
        }




        public async Task<List<Country>> GetData()
        {
            this.Countries.Clear();
            int counter = 0;
            List<Country> countries = new List<Country>();
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(Url)
            };
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            using (HttpResponseMessage response = client.GetAsync("").Result)
            {
                if (response.IsSuccessStatusCode)
                {
                    var dataObj = await response.Content.ReadAsAsync<Dictionary<string,string>[]>();

                    foreach (Dictionary<string,string> dict in dataObj)
                    {
                        Country c = new Country
                        {
                            Country_Name = dict["name"],
                            Id = counter
                        };
                        countries.Add(c);
                        counter++;
                    }
                   
                }
            }

            return countries;//might cause race condition?

        }

    }
}
