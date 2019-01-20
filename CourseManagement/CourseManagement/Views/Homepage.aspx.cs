using System;
using System.Web;
using System.Web.UI.WebControls;
using CourseManagement.App_Code;
using CourseManagement.DAL;

namespace CourseManagement.Views
{
    public partial class Homepage : System.Web.UI.Page
    {
        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #endregion

        protected void lgnUser_OnLoggingIn(object sender, LoginCancelEventArgs e)
        {
            UserDAL userDAL = new UserDAL();
            User user = userDAL.CheckLogin(lgnUser.UserName, lgnUser.Password);
            if (string.IsNullOrEmpty(user.UserId + user.Password + user.Role))
            {
                this.loginResults.Text = "Invalid Username or Password";
                HttpContext.Current.Session["user"] = null;
            }
            else
            {
                HttpContext.Current.Session["user"] = user;
            }

            
        }
    }
}