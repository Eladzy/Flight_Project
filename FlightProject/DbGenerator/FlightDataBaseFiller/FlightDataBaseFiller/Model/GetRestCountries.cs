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
        readonly static string Url = "https://restcountries.eu/rest/v2/all?fields=name;";

        private List<Country> _countries;
        public List<Country> Countries
        {
            get
            {
                //  Countries.Clear();             
                return _countries;
            }
          private  set { _countries = value; }
        }

        public GetRestCountries()
        {
            GetData();
        }


        private async void GetData()
        {
           
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

            this.Countries=countries;//might cause race condition?

        }

    }
}
