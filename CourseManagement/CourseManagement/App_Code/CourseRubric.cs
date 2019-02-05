using System;
using System.Collections.Generic;
using System.Web;

namespace CourseManagement.App_Code
{
    /// <summary>
    /// Course rubric class
    /// </summary>
    public class CourseRubric
    {
        /// <summary>
        /// gets the rubric ID
        /// </summary>
        public int RubricID { get; }
        /// <summary>
        /// gets the grade type and weights
        /// </summary>
        public Dictionary<string,int> GradeTypeWithWeights { get; private set; }
        /// <summary>
        /// constuctor of the course rubric
        /// </summary>
        /// <param name="gradeTypeWithWeights">grade and weight combo</param>
        /// <param name="rubricID">rubric Id</param>
        public CourseRubric(Dictionary<string, int> gradeTypeWithWeights, int rubricID)
        {
            RubricID = rubricID;
            GradeTypeWithWeights = gradeTypeWithWeights;
        }

    }
}