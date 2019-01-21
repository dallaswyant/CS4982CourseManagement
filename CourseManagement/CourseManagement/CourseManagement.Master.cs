using System;
using System.Web;
using System.Web.UI.WebControls;
using CourseManagement.App_Code;
using CourseManagement.DAL;

namespace CourseManagement
{
    public partial class CourseManagement : System.Web.UI.MasterPage
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBind();

            }

            User user = (User) HttpContext.Current.Session["User"];
            if (user == null)
            {
                this.btnLogin.Visible = true;
                this.btnLogout.Visible = false;
                this.lblPasswordTXT.Visible = true;
                this.lblUsernameTXT.Visible = true;
                this.tbxPassword.Visible = true;
                this.tbxUsername.Visible = true;
                
                this.lblUsername.Text = "";
            }
            else
            {
                this.btnLogin.Visible = false;
                this.btnLogout.Visible = true;
                this.lblPasswordTXT.Visible = false;
                this.lblUsernameTXT.Visible = false;
                this.tbxPassword.Visible = false;
                this.tbxUsername.Visible = false;
                this.lblUsername.Text = user.UserId;
            }

        }

        private void handleLogin(UserDAL userDAL)
        {
            User user = userDAL.CheckLogin(this.tbxUsername.Text, this.tbxPassword.Text);
            if (string.IsNullOrEmpty(user.UserId + user.Password + user.Role))
            {
                HttpContext.Current.Session["User"] = null;
                HttpContext.Current.Session["UserID"] = null;
            }
            else
            {
                HttpContext.Current.Session["User"] = user;
                HttpContext.Current.Session["UserID"] = user.UserId;
            }

            this.btnLogin.Text = "Logout";
            this.lblPasswordTXT.Visible = false;
            this.lblUsernameTXT.Visible = false;
            this.tbxPassword.Visible = false;
            this.tbxUsername.Visible = false;
            this.lblUsername.Text = user.UserId;
        }

        private void handleLogout()
        {
            this.btnLogin.Text = "Login";
            HttpContext.Current.Session["User"] = null;

            this.lblPasswordTXT.Visible = true;
            this.tbxPassword.Text = "";
            this.lblUsernameTXT.Visible = true;
            this.tbxUsername.Text = "";
            this.tbxPassword.Visible = true;
            this.tbxUsername.Visible = true;

            this.lblUsername.Text = "";
        }

        protected void login_OnClick(object sender, EventArgs e)
        {
            UserDAL userDAL = new UserDAL();
            handleLogin(userDAL);
            HttpContext.Current.Response.Redirect("Homepage.aspx");

        }

        protected void logout_OnClick(object sender, EventArgs e)
        {
            handleLogout();
            HttpContext.Current.Response.Redirect("Homepage.aspx");
        }
    }
}