﻿using System;
using System.Collections.Generic;
using System.Linq;
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
            if (!IsPostBack)
            {
                DataBind();
            }
            CourseDAL courseGetter = new CourseDAL();
            User currentUser = HttpContext.Current.Session["User"] as User;
            var courses = courseGetter.GetCoursesByTeacherAndSemester(currentUser.UserId, this.ddlSemesters.SelectedValue);
            if (HttpContext.Current.Session["SelectedCourseIndex"] != null &
                HttpContext.Current.Session["SelectedSemester"] != null)
            {
                this.ddlCourses.Items.Clear();
                foreach (var course in courses)
                {
                    this.ddlCourses.Items.Add(new ListItem(course.Name, course.CRN.ToString()));
                }

                var oldCourseIndex = (int) HttpContext.Current.Session["SelectedCourseIndex"];
                var oldSemester = (string) HttpContext.Current.Session["SelectedSemester"];
                if (oldSemester == this.ddlSemesters.SelectedValue)
                {
                    this.ddlCourses.SelectedIndex = oldCourseIndex;
                }

        }
            else
            {
                this.ddlCourses.Items.Clear();
                foreach (var course in courses)
                {
                    this.ddlCourses.Items.Add(new ListItem(course.Name, course.CRN.ToString()));
                }

                this.lblError.Text = String.Empty;
            }
            GradeItemDAL checker = new GradeItemDAL();
            int crn = Int32.Parse(this.ddlCourses.SelectedValue);
            Dictionary<string, string> items = checker.GetUniqueGradedItemsByCRN(crn);
            try
            {
                var selected = items.First(o => o.Value.Equals(this.ddlAssignments.SelectedValue));
                Dictionary<string, string> list = (Dictionary<string, string>)this.odsAssignments.Select();
                string stuff = list.Values.ToList()[0];

                string selectedValue = selected.Value;

                bool visible = checker.getPublicStatusByCRNandGradeName(crn,
                    selectedValue);
                this.cbxVisibility.Checked = visible;
            }
            catch (Exception ex)
            {
                this.lblError.Text = "There are no assignments for this course. Whoops!";
            }
            //<asp:ObjectDataSource ID="odsCourses" runat="server" OldValuesParameterFormatString="original_{0}"  TypeName="CourseManagement.DAL.CourseDAL" SelectMethod="GetCoursesByTeacherAndSemester">
            //< SelectParameters >
            //    < asp:SessionParameter Name = "teacherIDCheck" SessionField = "UserID" Type = "String" />
     
            //    < asp:ControlParameter ControlID = "ddlSemesters" Name = "semesterID" PropertyName = "SelectedValue" Type = "String" />
            
            //    </ SelectParameters >
            
            //    </ asp:ObjectDataSource >



        }

        protected void gvwGrade_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

            int index = e.NewSelectedIndex;
            List<GradeItem> items = (List<GradeItem>) this.odsStudents.Select();

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
            List<GradeItem> items = (List<GradeItem>)this.odsStudents.Select();
            GradeItem current = items[0];
            HttpContext.Current.Session["CurrentGradedItem"] = current;
            HttpContext.Current.Session["editing"] = true;
            CourseDAL courseDal = new CourseDAL();
            int crn = int.Parse(this.ddlCourses.SelectedValue);
            Course currentCourse = courseDal.GetCourseByCRN(crn);
            HttpContext.Current.Session["CurrentCourse"] = currentCourse;
            HttpContext.Current.Response.Redirect("ManageCreateGradeItem.aspx");
        }

        #endregion

        protected void cbxVisibility_CheckedChanged(object sender, EventArgs e)
        {
            GradeItemDAL gradeStuff = new GradeItemDAL();
            gradeStuff.PublishGradeItemByNameAndCRNForAllStudents(Int32.Parse(this.ddlCourses.SelectedValue), this.ddlAssignments.SelectedValue, !this.cbxVisibility.Checked);
        }

        protected void ddlCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            HttpContext.Current.Session["SelectedCourseIndex"] = this.ddlCourses.SelectedIndex;
            HttpContext.Current.Session["SelectedSemester"] = this.ddlSemesters.SelectedValue;
            this.UpdatePanel1.Update();
        }

        protected void ddlSemesters_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdatePanel1.Update();
            
        }
    }
}