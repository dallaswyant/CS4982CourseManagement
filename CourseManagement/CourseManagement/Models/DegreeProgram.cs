using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseManagement.App_Code
{
    public class DegreeProgram
    {

        public string name { get; private set; }
        public CourseCollection RequiredCourses { get; private set; }

        public DegreeProgram(string name, CourseCollection requiredCourses)
        {
            this.name = name;
            RequiredCourses = requiredCourses;
        }
    }
}