using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace CourseManagement.Models
{
    public class CourseTime
    {
        public int CourseTimeID { get; set; }

        public String TimeSlot => this.CourseDays + " (" + this.CourseStart.ToString("hh:mm tt", new CultureInfo("en-US")) + "-" + CourseEnd.ToString("hh:mm tt", new CultureInfo("en-US")) + ")";

        public DateTime CourseStart { get; set; }

        public DateTime CourseEnd { get; set; }

        public string CourseDays { get; set; }

        public CourseTime(int courseTimeId, DateTime courseStart, DateTime courseEnd, string courseDays)
        {
            this.CourseTimeID = courseTimeId;
            this.CourseStart = courseStart;
            this.CourseEnd = courseEnd;
            this.CourseDays = courseDays;
        } 
    }
}