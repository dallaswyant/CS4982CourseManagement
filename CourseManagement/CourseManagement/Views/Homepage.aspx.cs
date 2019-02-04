using System;
using System.Transactions;
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
            if (!IsPostBack)
            {
                DataBind();
            }

            User currentUser = HttpContext.Current.Session["User"] as User;

            this.handleSiteNavigationDisplay(currentUser);

        }

        private void handleSiteNavigationDisplay(User currentUser)
        {
            if (currentUser == null)
            {
                this.handleWhenUserNotSignedIn();
            }
            else if (currentUser.Role.Equals("teachers"))
            {
                this.handleWhenTeacherLogin();
            }
            else if (currentUser.Role.Equals("students"))
            {
                this.handleWhenStudentLoggedIn();
            }
            else
            {
                this.TreeView1.Visible = false;
            }
        }

        private void handleWhenUserNotSignedIn()
        {
            this.TreeView1.Visible = false;
        }

        private void handleWhenTeacherLogin()
        {
            this.TreeView1.Visible = true;
            this.SiteMapDataSource1.SiteMapProvider = "Teacher";
        }

        private void handleWhenStudentLoggedIn()
        {
            this.TreeView1.Visible = true;
            this.SiteMapDataSource1.SiteMapProvider = "Student";
        }

        #endregion



            
        
    }
}