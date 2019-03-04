using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseManagement.DAL;
using CourseManagement.Models;

namespace CoursesManagementDesktop.Model
{
   public class LoginHandler
    {
        /// <summary>
        /// This is a public property for the user that is currently logged in
        /// </summary>
        public User CurrentUser { get; private set; }  
        
        /// <summary>
        /// Checks and handles if the credintials are a valid match
        /// </summary>
        /// <param name="username">the username</param>
        /// <param name="password">the password</param>
        /// <precondition>
        /// username != null And username!= empty string
        /// And password!= null And password != empty string
        /// </precondition>
        /// <returns>false is valid credits true other wise</returns>
        public bool IsInvalidCredentials(string username, string password)
        {
            

            UserDAL userDAL = new UserDAL();
            this.CurrentUser = userDAL.CheckLogin(username, password);
            return(this.CurrentUser == null || string.IsNullOrWhiteSpace(this.CurrentUser.UserId + this.CurrentUser.Password + this.CurrentUser.Role));
        }
    }
}
