using System;
using System.Transactions;
using System.Web;
using System.Web.UI.WebControls;
using CourseManagement.Models;

namespace CourseManagement.Views
{
    public partial class Homepage : System.Web.UI.Page
    {
   

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBind();
            }

        }


    }
}