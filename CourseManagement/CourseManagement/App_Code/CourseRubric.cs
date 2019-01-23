using System;
using System.Collections.Generic;
using System.Web;

namespace CourseManagement.App_Code
{
    public class CourseRubric
    {
        public int RubricID { get; }
        public Dictionary<string,int> GradeTypeWithWeights { get; private set; }

        public CourseRubric(Dictionary<string, int> gradeTypeWithWeights, int rubricID)
        {
            RubricID = rubricID;
            GradeTypeWithWeights = gradeTypeWithWeights;
        }

    }
}