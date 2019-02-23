using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseManagement.App_Code;
using CourseManagement.DAL;

namespace CoursesManagementDesktop.Model
{
   public class LoginHandler
    {
        public User user { get; private set; }   
        public bool IsInvalidCredentials(string username, string password)
        {
            UserDAL userDAL = new UserDAL();
            this.user = userDAL.CheckLogin(username, password);
            return(this.user == null || string.IsNullOrWhiteSpace(user.UserId + user.Password + user.Role));
        }
    }
}
