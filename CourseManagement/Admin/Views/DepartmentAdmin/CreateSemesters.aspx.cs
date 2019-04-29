using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Admin.DAL;
using Admin.Models;
using Admin.Utilities;

namespace Admin.Views.DepartmentAdmin
{
    public partial class AddSemesters : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBind();
            }

            this.semesterSeason.DataSource = new List<String>()
            {
                "FALL",
                "SPRING",
                "SUMMER"
            };

            this.semesterYear.DataSource = CreateSemesterUtility.GetYears();


        }

        protected void createSemester_onClick(object sender, EventArgs e)
        {
            var seasonID = this.semesterSeason.SelectedValue.Substring(0, 2);
            var yearID = this.semesterYear.SelectedValue.Substring(2);
            var semesterID = seasonID + yearID;
            var startDate = DateTime.Parse(this.startDate.Text);
            var endDate = DateTime.Parse(this.endDate.Text);
            var finalGradeDeadline = DateTime.Parse(this.finalGradeDeadline.Text);
            var addDropDeadline = DateTime.Parse(this.addDropDeadline.Text);
            Semester semester = new Semester(semesterID, addDropDeadline, finalGradeDeadline, startDate, endDate);
            AdminDAL dal = new AdminDAL();
            try
            {
              dal.CreateSemester(semester);
                this.error.Text = "This semester has already been created.";
                this.confirmation.Text = "Semester successfully created.";
            } catch (Exception)
            {
                this.error.Text = "This semester has already exists.";
                this.confirmation.Text = "";
            }
            


        }

    }
}