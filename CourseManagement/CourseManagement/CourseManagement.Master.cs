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
            
            User currentUser = HttpContext.Current.Session["User"] as User;

            if (currentUser == null)
            {
                managePropertiesWhenNotLoggedIn();
            }
            else
            {
                managePropertiesWhenLoggedIn();
                handleLogin(currentUser);
            }

        }

        private void handleLogin(User currentUser)
        {
            if (currentUser.Role.Equals("teachers"))
            {
                handleTeacherLogin(currentUser);
            }
            else if (currentUser.Role.Equals("students"))
            {
                handleStudentLogin(currentUser);
            }
        }

        private void handleStudentLogin(User currentUser)
        {
            StudentDAL studentDAL = new StudentDAL();
            Student student = studentDAL.GetStudentByStudentID(currentUser.UserId);
            this.lblUsername.Text = "Welcome, " + student.name + " (" + currentUser.Role + ") ";
            this.smdsSite.SiteMapProvider = "Student";
            this.menuMain.Visible = true;
        }

        private void handleTeacherLogin(User currentUser)
        {
            TeacherDAL teacherDAL = new TeacherDAL();
            Teacher teacher = teacherDAL.GetTeacherByTeacherID(currentUser.UserId);
            this.lblUsername.Text = "Welcome, " + teacher.Name + " (" + currentUser.Role + ") ";
            this.smdsSite.SiteMapProvider = "Teacher";
            this.menuMain.Visible = true;
        }

        private void managePropertiesWhenLoggedIn()
        {
            this.tbxUsername.TabIndex = 100;
            this.tbxPassword.TabIndex = 101;
            this.btnLogin.TabIndex = 102;
            this.btnLogout.TabIndex = 10;
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
            this.btnLogout.TabIndex = 100;
        }

        private void handleLogin()
        {
            UserDAL userDAL = new UserDAL();
            User user = userDAL.CheckLogin(this.tbxUsername.Text, this.tbxPassword.Text);
            if (user == null || string.IsNullOrWhiteSpace(user.UserId + user.Password + user.Role))
            {
                HttpContext.Current.Session["User"] = null;
                HttpContext.Current.Session["UserID"] = null;
                this.lblLogin.Text = "Invalid username or password.";

            }
            else
            {
                HttpContext.Current.Session["User"] = user;
                HttpContext.Current.Session["UserID"] = user?.UserId; 
                this.lblPasswordTXT.Visible = false;
                this.lblUsernameTXT.Visible = false;
                this.tbxPassword.Visible = false;
                this.tbxUsername.Visible = false;
                this.lblLogin.Text = "";
                this.lblUsername.Text = user.UserId;
            }


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
            HttpContext.Current.Response.Redirect("Homepage.aspx");

        }

        protected void logout_OnClick(object sender, EventArgs e)
        {
            handleLogout();
            HttpContext.Current.Response.Redirect("../Homepage.aspx");
        }
    }
}