using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CourseManagement.App_Code;
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
                    this.ddlDepartments.Items.Add(dept.DeptName);
                }
                this.DataBind();
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e) { 
            this.browseCoursUpdatePanel.Update();
        }

        protected void btnAddCourse_Click(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["chosenCRN"] != null)
            {
                var current = HttpContext.Current.Session["User"] as User;
                StudentDAL courseAdder = new StudentDAL();
                int crn = (int)HttpContext.Current.Session["chosenCRN"];
                try
                {
                    courseAdder.addCourseByCRNAndStudentUID(crn, current.UserId);
                    Response.Redirect("BrowseCourses.aspx");
                }
                catch (Exception ex)
                {
                    this.lblCourseToAdd.Text = "You are already in this course";
                    HttpContext.Current.Session["chosenCRN"] = null;
                }
            }
            
        }

        protected void AvailableCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            int thing = (int)this.AvailableCoursesGrid.SelectedValue;
            HttpContext.Current.Session["chosenCRN"] = thing;
            CourseDAL courseGetter = new CourseDAL();
            Course courseToAdd = courseGetter.GetCourseByCRN(thing);
            //TODO get this teacher somehow
            //this.lblCourseToAdd.Text = "Course to Add: " + courseToAdd.CRN + " " + courseToAdd.Name + " " +
            //                               courseToAdd.SectionNumber + " " + courseToAdd.Teacher;
        }
    }
}