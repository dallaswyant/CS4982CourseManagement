using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CourseManagement.DAL;

namespace CourseManagement.Views.Student
{
    public partial class BrowseCourses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DepartmentDAL deptGetter = new DepartmentDAL();
                foreach (var dept in deptGetter.GetAllDepartments())
                {
                    this.DropDownList1.Items.Add(dept.DeptName);
                }
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e) { 
        


            this.browseCoursUpdatePanel.Update();
        }
    }
}