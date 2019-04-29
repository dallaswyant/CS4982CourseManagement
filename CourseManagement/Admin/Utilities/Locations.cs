using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Utilities
{
    public static class Locations
    {
        public static List<String> GetCountries()
        {
            var countries = new List<String>();
            countries.Add("USA");
            return countries;
        }

        public static List<String> GetStates()
        {
            var states = new List<String>(50);
            states.Add("Alabama");
            states.Add("Alaska");
            states.Add("Arizona");
            states.Add("Arkansas");
            states.Add("California");
            states.Add("Colorado");
            states.Add("Connecticut");
            states.Add("Delaware");
            states.Add("District Of Columbia");
            states.Add("Florida");
            states.Add("Georgia");
            states.Add("Hawaii");
            states.Add("Idaho");
            states.Add("Illinois");
            states.Add("Indiana");
            states.Add("Iowa");
            states.Add("Kansas");
            states.Add("Kentucky");
            states.Add("Louisiana");
            states.Add("Maine");
            states.Add("Maryland");
            states.Add("Massachusetts");
            states.Add("Michigan");
            states.Add("Minnesota");
            states.Add("Mississippi");
            states.Add("Missouri");
            states.Add("Montana");
            states.Add("Nebraska");
            states.Add("Nevada");
            states.Add("New Hampshire");
            states.Add("New Jersey");
            states.Add("New Mexico");
            states.Add("New York");
            states.Add("North Carolina");
            states.Add("North Dakota");
            states.Add("Ohio");
            states.Add("Oklahoma");
            states.Add("Oregon");
            states.Add("Pennsylvania");
            states.Add("Rhode Island");
            states.Add("South Carolina");
            states.Add("South Dakota");
            states.Add("Tennessee");
            states.Add("Texas");
            states.Add("Utah");
            states.Add("Vermont");
            states.Add("Virginia");
            states.Add("Washington");
            states.Add("West Virginia");
            states.Add("Wisconsin");
            states.Add("Wyoming");
            return states;
        }

    }
}