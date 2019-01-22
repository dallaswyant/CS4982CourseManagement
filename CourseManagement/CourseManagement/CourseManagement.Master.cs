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
                this.rfvUsername.Enabled = true;
                this.rfvPassword.Enabled = true;
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
                this.rfvUsername.Enabled = false;
                this.rfvPassword.Enabled = false;
                if (user.Role.Equals("teachers"))
                {
                    TeacherDAL teacherDAL = new TeacherDAL();
                    Teacher teacher = teacherDAL.GetTeacherByTeacherID(user.UserId);
                    this.lblUsername.Text = "Welcome, " + teacher.Name + " (" + user.Role + ") ";
                } else if (user.Role.Equals("students"))
                {
                    StudentDAL studentDAL = new StudentDAL();
                    Student student = studentDAL.GetStudentByStudentID(user.UserId);
                    this.lblUsername.Text = "Welcome, " + student.name + " (" + user.Role + ") ";
                }
                
            }

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
            HttpContext.Current.Response.Redirect("Homepage.aspx");
        }
    }
}