using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using CourseManagement.DAL;

namespace CourseManagement.Views
{
    public partial class RubricPage : System.Web.UI.Page
    {
        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBind();
            }
            HttpContext.Current.Session["CRN"] = int.Parse(this.ddlCourse.SelectedValue);
        }

        #endregion

        protected void GridView1_RowUpdated(object sender, System.Web.UI.WebControls.GridViewUpdatedEventArgs e)
        {
            //this.Validate("gridview");
        }

        protected void gvwWeights_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
       //     HttpContext.Current.Session["TypeWeightIndex"] = e.RowIndex;

            //   int typeWeightIndex = e.RowIndex;
            //    e.NewValues.Values as 

            //       int crn = int.Parse(this.ddlCourse.SelectedValue);


            // CourseRubricDAL dal = new CourseRubricDAL();
            //  Dictionary<String,int> rubric = dal.GetCourseRubricByCRN(crn);
            //  dal.UpdateCourseRubric(rubric, crn, typeWeightIndex);


        }

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            HttpContext.Current.Session["CRN"] = int.Parse(this.ddlCourse.SelectedValue);
        }

    }
}