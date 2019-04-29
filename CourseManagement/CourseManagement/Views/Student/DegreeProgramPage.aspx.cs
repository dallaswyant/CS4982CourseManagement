using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CourseManagement.DAL;

namespace CourseManagement.Views.Student
{
    public partial class DegreeProgramPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBind();
            }

            this.DropDownList1.DataSource = (new DegreeProgramDAL()).GetAllDegreePrograms();
            List<String> coursesRequired =
                (new DegreeProgramDAL()).GetApplicableCoursesByDegreeProgram(this.DropDownList1.SelectedValue);
            this.GridView2.DataSource =
                coursesRequired.Select(i => new { Data = i });


        }
    }
}