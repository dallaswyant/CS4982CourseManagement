using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseManagement.Utilities
{
    public static class GradeSuggester
    {
        public static char GetSuggestedGrade(double grade)
        {
            if (grade < 60)
            {
                return 'F';
            } else if (grade < 70)
            {
                return 'D';
            }else if (grade < 80)
            {
                return 'C';
            } else if (grade < 90)
            {
                return 'B';
            }
            else
            {
                return 'A';
            }
        }
    }
}