using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseManagement.App_Code;

namespace CourseManagement.Models
{
    public class CourseInfo
    {
        public string Name { get; set; }
        public string description { get; private set; }
        public Teacher teacher { get; private set; }
        public string Location { get; private set; }
        public CourseCollection preReqClasses { get; private set; }
        public int  creditHours { get; private set; }
        public string identifier { get; private set; }
        public int sectionNumber { get; private set; }

        public CourseInfo(string name, string description, Teacher teacher, string location, CourseCollection preReqClasses, int creditHours, string identifier, int sectionNumber)
        {
            Name = name;
            this.description = description;
            this.teacher = teacher;
            Location = location;
            this.preReqClasses = preReqClasses;
            this.creditHours = creditHours;
            this.identifier = identifier;
            this.sectionNumber = sectionNumber;
        }
    }
}
