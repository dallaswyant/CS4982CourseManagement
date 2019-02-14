using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseManagement.App_Code
{
    public class Semester
    {
        public string SemesterID { get; }
        public Semester(string semesterID)
        {
            this.SemesterID = semesterID;
        }
    }
}