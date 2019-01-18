using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseManagement.App_Code
{
    public class User
    {
        public string UserID { get; private set; }
        public string password { get; private set; }
        public string role { get; private set; }

        public User(string userID, string password, string role)
        {
            UserID = userID;
            this.password = password;
            this.role = role;
        }

        public User()
        {
        }
    }
}