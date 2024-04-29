using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Assignment_1.Models;

namespace Assignment_1.Controllers
{
    public class CountryController : ApiController
    {
        private static List<Country> countrylist = new List<Country>
        {
            new Country { Id = 1, CountryName = "INDIA", Capital = "NEW DELHI" },
            new Country { Id = 2, CountryName = "USA", Capital = "WASHINGTON DC" },
            new Country { Id = 3, CountryName = "BANGLADESH", Capital = "DHAKA" },
            new Country { Id = 4, CountryName = "AFGHANISTAN", Capital = "KABUL" }
        };

        //To read/view country

        [HttpGet]
        public IEnumerable<Country> Get()
        {
            return countrylist;
        }

        //To add new country
        [HttpPost]
        public List<Country> Post([FromBody] Country cont)
        {
            countrylist.Add(cont);
            return countrylist;
        }

        //to update the country
        [HttpPut]
        public void Put(int contid, [FromUri] Country country)
        {
            countrylist[contid - 1] = country;
        }

        //To delete the country
        [HttpDelete]
        public void Delete(int contid)
        {
            countrylist.RemoveAt(contid - 1);
        }

    }
}
