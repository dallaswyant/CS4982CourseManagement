using System;
using System.Web;
using CourseManagement.App_Code;

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
            if (user != null)
            {
                this.lblUsername.Text = user.UserId;
                this.btnLogging.Text = "Logout";
                
            }
            else
            {
                this.btnLogging.Text = "Login";
                this.lblUsername.Text = "";
            }

        }

        #endregion
    }
}