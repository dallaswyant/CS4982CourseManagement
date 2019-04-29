using System;
using System.Web;
using System.Web.UI.WebControls;
using Admin.DAL;
using Admin.Models;
using CourseManagementAdministration.Models;

namespace CourseManagementAdministration.Models
{
    public partial class CourseManagement : System.Web.UI.MasterPage
    {

        #region Method

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBind();
            }
            
            User currentUser = HttpContext.Current.Session["User"] as User;
            this.manageLogging(currentUser);

            this.handleSiteNavigationDisplay(currentUser);
            HttpContext.Current.Session["previousPage"] = this.ContentPlaceHolder1.Page.GetType().BaseType.Name;
        }

        private void handleSiteNavigationDisplay(User currentUser)
        {
            if (currentUser == null)
            {
               
            
            }else if (currentUser.Role.Equals("admin"))
            {
                this.handleWhenAdminLoggedIn();
            }
        }


    
        private void handleWhenAdminLoggedIn()
        {

            this.SiteMapDataSource1.SiteMapProvider = "Admin";
        }


        private void manageLogging(User currentUser)
        {
            if (currentUser == null)
            {
                this.managePropertiesWhenNotLoggedIn();
            } 
            else
            {
                this.managePropertiesWhenLoggedIn();
                this.handleLogin(currentUser);
            }
        }

        private void handleLogin(User currentUser)
        {
            if (currentUser.Role.Equals("admin"))
            {
                handleAdminLogin(currentUser);
                HttpContext.Current.Session["User"] = currentUser;
            }
        }



        private void handleAdminLogin(User currentUser)
        {
            this.lblUsername.Text = "Welcome, " + currentUser.UserId + " (" + currentUser.Role + ") ";
            this.smdsSite.SiteMapProvider = "Admin";
            this.menuMain.Visible = true;
        }

        private void managePropertiesWhenLoggedIn()
        {
            this.tbxUsername.TabIndex = -1;
            this.tbxPassword.TabIndex = -1;
            this.btnLogin.TabIndex = -1;
            this.btnLogout.TabIndex = 1;
            this.btnLogin.Visible = false;
            this.btnLogout.Visible = true;
            this.lblPasswordTXT.Visible = false;
            this.lblUsernameTXT.Visible = false;
            this.tbxPassword.Visible = false;
            this.tbxUsername.Visible = false;
            this.rfvUsername.Enabled = false;
            this.rfvPassword.Enabled = false;
        }

        private void managePropertiesWhenNotLoggedIn()
        {
            this.menuMain.Visible = false;
            this.btnLogin.Visible = true;
            this.btnLogout.Visible = false;
            this.lblPasswordTXT.Visible = true;
            this.lblUsernameTXT.Visible = true;
            this.tbxPassword.Visible = true;
            this.tbxUsername.Visible = true;
            this.rfvUsername.Enabled = true;
            this.rfvPassword.Enabled = true;
            this.lblUsername.Text = "";
            this.tbxUsername.TabIndex = 1;
            this.tbxPassword.TabIndex = 2;
            this.btnLogin.TabIndex = 3;
            this.btnLogout.TabIndex = -1;
        }

        private void handleLogin()
        {
            UserDAL userDAL = new UserDAL();
                User user = userDAL.CheckLogin(this.tbxUsername.Text, this.tbxPassword.Text);
                if (user == null || string.IsNullOrWhiteSpace(user.UserId + user.Password + user.Role))
                {
                    this.handleInvalidLogin("Invalid username or password.");
                }
                else if (!user.Role.Equals("admin"))
                {
                    this.handleInvalidLogin("Please login as an Administrator.");
                }
                else
                {
                    HttpContext.Current.Session["User"] = user;
                    User currentUser = HttpContext.Current.Session["User"] as User;
                    this.handleValidLogin(user);
                }
            


        }

        private void handleValidLogin(User user)
        {
            

            HttpContext.Current.Session["UserID"] = user?.UserId;
            this.lblPasswordTXT.Visible = false;
            this.lblUsernameTXT.Visible = false;
            this.tbxPassword.Visible = false;
            this.tbxUsername.Visible = false;
            this.lblLogin.Text = "";
            this.lblUsername.Text = user.UserId;
            if(user.Role.Equals("admin"))
            {
                HttpContext.Current.Response.Redirect("DepartmentAdmin/ManageCourses.aspx");
            }
        }

        private void handleInvalidLogin(String prompt)
        {
            HttpContext.Current.Session["User"] = null;
            HttpContext.Current.Session["UserID"] = null;
            this.lblLogin.Text = prompt;
        }

        private void handleLogout()
        {
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
            handleLogin();
        }

        protected void logout_OnClick(object sender, EventArgs e)
        {
            handleLogout();
            HttpContext.Current.Response.Redirect("~/Views/Homepage.aspx");
        }

        #endregion
    }
}