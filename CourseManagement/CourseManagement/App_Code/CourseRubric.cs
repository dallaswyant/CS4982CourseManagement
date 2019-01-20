using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseManagement.App_Code
{
    public class CourseRubric
    {
        public Dictionary<string,int> GradeTypeWithWeights { get;private set; }

        public CourseRubric(Dictionary<string, int> gradeTypeWithWeights)
        {
            GradeTypeWithWeights = gradeTypeWithWeights;
        }


    }
}