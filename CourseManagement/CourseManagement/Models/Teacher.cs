using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseManagement.App_Code
{
    public class Teacher
    {
        public string Location { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public bool IsEmailPublic { get; private set; }
        public string PhoneNumber { get; private set; }
        public Department PrimaryDepartment { get; private set; }
        public CourseCollection CoursesTaught { get; private set; }

        public Teacher(string location, string name, string email, bool isEmailPublic, string phoneNumber, Department primaryDepartment, CourseCollection coursesTaught)
        {
            Location = location;
            Name = name;
            Email = email;
            IsEmailPublic = isEmailPublic;
            PhoneNumber = phoneNumber;
            PrimaryDepartment = primaryDepartment;
            CoursesTaught = coursesTaught;
        }
    }
}