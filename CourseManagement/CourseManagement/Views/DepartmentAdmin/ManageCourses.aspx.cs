using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CourseManagement.Views.DepartmentAdmin
{
    public partial class ManageCourses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void gvwDepartmentCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dvwTeacherCourse.PageIndex = gvwDepartmentCourses.SelectedIndex;
            dvwTeacherCourse.DataBind();
        }
    }
}