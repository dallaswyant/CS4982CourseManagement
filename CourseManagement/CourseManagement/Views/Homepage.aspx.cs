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

            if (currentUser == null)
            {
                this.TreeView1.Visible = false;
            }
            else if (currentUser.Role.Equals("teachers"))
            {
                this.TreeView1.Visible = true;
                this.SiteMapDataSource1.SiteMapProvider = "Teacher";
            }
            else if (currentUser.Role.Equals("students"))
            {
                this.TreeView1.Visible = true;
                this.SiteMapDataSource1.SiteMapProvider = "Student";
            }
            else
            {
                this.TreeView1.Visible = false;
            }

        }

        #endregion



            
        
    }
}