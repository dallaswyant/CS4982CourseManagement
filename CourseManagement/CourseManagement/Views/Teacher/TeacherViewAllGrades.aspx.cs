using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using CourseManagement.App_Code;
using CourseManagement.DAL;

namespace CourseManagement.Views
{
    public partial class TeacherViewAllGrades : System.Web.UI.Page
    {
        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void gvwGrade_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

            int index = e.NewSelectedIndex;
            List<GradedItem> items = (List<GradedItem>) this.odsStudents.Select();

            HttpContext.Current.Session["CurrentGradedItem"] = items[index];
            HttpContext.Current.Session["SelectedStudent"] = items[index].Student;
            CourseDAL courseDal = new CourseDAL();
            int crn = int.Parse(this.ddlCourses.SelectedValue);
            Course currentCourse = courseDal.GetCourseByCRN(crn);
            HttpContext.Current.Session["CurrentCourse"] = currentCourse;
            HttpContext.Current.Response.Redirect("TeacherGradeGradeItemPage.aspx");
        }

      


        protected void ddlAssignments_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdatePanel1.Update();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            List<GradedItem> items = (List<GradedItem>)this.odsStudents.Select();
            GradedItem current = items[0];
            HttpContext.Current.Session["CurrentGradedItem"] = current;
            CourseDAL courseDal = new CourseDAL();
            int crn = int.Parse(this.ddlCourses.SelectedValue);
            Course currentCourse = courseDal.GetCourseByCRN(crn);
            HttpContext.Current.Session["CurrentCourse"] = currentCourse;
            HttpContext.Current.Response.Redirect("ManageCreateGradeItem.aspx");
        }  
        
        #endregion
    }
}