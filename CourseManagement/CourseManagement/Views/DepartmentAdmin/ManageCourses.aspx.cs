using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CourseManagement.DAL;
using CourseManagement.Models;

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

        protected void btnAddTeachers_Click(object sender, EventArgs e)
        {
            DepartmentAdminDAL dal = new DepartmentAdminDAL();
            var crnRow = this.dvwTeacherCourse.Rows[1];
            var crnCell = crnRow.Cells[1];
            var crn = int.Parse(crnCell.Text);
            string teacherID = this.ddlTeachers.SelectedValue;
            TeacherDAL tdal = new TeacherDAL();
            Teacher teacher = tdal.GetTeacherByTeacherID(teacherID);
            dal.AssignTeacherToCourse(teacher, crn);
        }

        protected void btnViewTeacherCourses_Click(object sender, EventArgs e)
        {
            CourseDAL dal = new CourseDAL();
            this.gvwTeacherCourses.DataSource = dal.GetCoursesByTeacherID(this.ddlTeachers.SelectedValue);

        }
    }
}