using System;
using System.Web;
using System.Web.UI.WebControls;
using CourseManagement.App_Code;
using CourseManagement.DAL;

namespace CourseManagement
{
    public partial class CourseManagement : System.Web.UI.MasterPage
    {
        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBind();

            }

            User user = (User) HttpContext.Current.Session["User"];
            if (user == null)
            {
                this.btnLogging.Text = "Login";
                this.lblPasswordTXT.Visible = true;
                this.lblUsernameTXT.Visible = true;
                this.tbxPassword.Visible = true;
                this.tbxUsername.Visible = true;
                
                this.lblUsername.Text = "";
            }
            else
            {
                this.btnLogging.Text = "Logout";
                this.lblPasswordTXT.Visible = false;
                this.lblUsernameTXT.Visible = false;
                this.tbxPassword.Visible = false;
                this.tbxUsername.Visible = false;
                this.lblUsername.Text = user.UserId;
            }

        }

        protected void lgnUser_OnLoggingIn(object sender, LoginCancelEventArgs e)
        {
 

            #endregion
        }

        protected void logging_OnClick(object sender, EventArgs e)
        {
            UserDAL userDAL = new UserDAL();


            if (this.btnLogging.Text.Equals("Logout"))
            {
                this.btnLogging.Text = "Login";
                HttpContext.Current.Session["User"] = null;

                this.lblPasswordTXT.Visible = true;
                this.tbxPassword.Text = "";
                this.lblUsernameTXT.Visible = true;
                this.tbxUsername.Text = "";
                this.tbxPassword.Visible = true;
                this.tbxUsername.Visible = true;
                
                this.lblUsername.Text = "";
            }
            else
            {
                User user = userDAL.CheckLogin(this.tbxUsername.Text, this.tbxPassword.Text);
                if (string.IsNullOrEmpty(user.UserId + user.Password + user.Role))
                {
                    HttpContext.Current.Session["User"] = null;
                }
                else
                {
                    HttpContext.Current.Session["User"] = user;
                }
                this.btnLogging.Text = "Logout";
                this.lblPasswordTXT.Visible = false;
                this.lblUsernameTXT.Visible = false;
                this.tbxPassword.Visible = false;
                this.tbxUsername.Visible = false;
                this.lblUsername.Text = user.UserId;
            }
        }
    }
}