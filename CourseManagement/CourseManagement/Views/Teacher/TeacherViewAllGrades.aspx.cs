using System;
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
            GradedItemDAL checker = new GradedItemDAL();
            //need to get current assignment and check the box or uncheck appropriately
            int crn = Int32.Parse(this.ddlCourses.SelectedValue);
            Dictionary<string,string> items =  checker.GetUniqueGradedItemsByCRN(crn);
            var selected = items.First(o => o.Value.Equals(this.ddlAssignments.SelectedValue));

            Dictionary<string,string> list = (Dictionary<string,string>) this.odsAssignments.Select();
            string stuff = list.Values.ToList()[0];
      
            string selectedValue = selected.Value;

            bool visible = checker.getPublicStatusByCRNandGradeName(crn,
                selectedValue);
            this.cbxVisibility.Checked = visible;
            
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
            
            GradedItemDAL gradeStuff = new GradedItemDAL();
            gradeStuff.PublishGradeItemByNameAndCRNForAllStudents(Int32.Parse(this.ddlCourses.SelectedValue), this.ddlAssignments.SelectedValue, !this.cbxVisibility.Checked);
            
        }
    }
}