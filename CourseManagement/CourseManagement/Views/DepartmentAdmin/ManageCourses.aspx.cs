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
            Models.Teacher teacher = tdal.GetTeacherByTeacherID(teacherID);
            dal.AssignTeacherToCourse(teacher, crn);
        }

        protected void btnViewTeacherCourses_Click(object sender, EventArgs e)
        {
            CourseDAL dal = new CourseDAL();
            this.gvwTeacherCourses.DataSource = dal.GetCoursesByTeacherID(this.ddlTeachers.SelectedValue);

        }

        protected void gvwDepartmentCourses_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            var rowIndex = e.RowIndex;
            
            var updateRow = this.gvwDepartmentCourses.Rows[rowIndex];
            var crn = int.Parse(((Label)updateRow.FindControl("crn")).Text);
            var name = ((TextBox)updateRow.FindControl("name")).Text;
            var description = ((TextBox)updateRow.FindControl("description")).Text;
            var location = ((TextBox)updateRow.FindControl("location")).Text;
            var credit = int.Parse(((TextBox)updateRow.FindControl("creditHours")).Text);
            var section = ((TextBox)updateRow.FindControl("section")).Text;
            var department = ((TextBox)updateRow.FindControl("department")).Text;
            var seats = int.Parse(((TextBox)updateRow.FindControl("seats")).Text);
            var semesterID = ((Label)updateRow.FindControl("semesterID")).Text;
            //TODO add course_time_id with a ddl
            //TODO make semesterID a ddl
            //TODO make crn a label
            //TODO make department a label
            //Course updateCourse = new Course(crn,department,name,description,section,credit,seats,location,semesterID);
            DepartmentAdminDAL dal = new DepartmentAdminDAL(); 
            //dal.UpdateCourse(updateCourse);



        }
    }
}