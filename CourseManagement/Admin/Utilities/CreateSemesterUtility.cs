using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Utilities
{
    public class CreateSemesterUtility
    {
        private static List<int> Years => generateYears();

        private static List<int> generateYears()
        {
            List<int> years = new List<int>();
            for (int year = DateTime.Now.Year; year < 3001; year++)
            {
                years.Add(year);
            }

            return years;
        }


        public static List<int> GetYears()
        {
            return Years;
        }
}
}