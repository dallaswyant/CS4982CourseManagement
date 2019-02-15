using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using CourseManagement.App_Code;
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
            CourseRubricDAL rubricGetter = new CourseRubricDAL();
            List<RubricItem> rubric = rubricGetter.GetCourseRubricByCRN(int.Parse(this.ddlCourse.SelectedValue));
            int sum = 0;
            foreach (var currRubricItem in rubric)
            {
                sum += currRubricItem.AssignmentWeight;

            }
            this.lblWarning.ForeColor = ColorTranslator.FromHtml("#0066FF");
            if (sum > 100)
            {
                this.lblWarning.Text = "Caution: rubric values add to " + sum + " which is over 100 percent";
            } else if (sum < 100)
            {
                this.lblWarning.Text = "Caution: rubric values add to " + sum + " which is less than 100 percent";
            }
            else
            {
                this.lblWarning.Text = "Rubric values sum to 100";
                this.lblWarning.ForeColor = ColorTranslator.FromHtml("#009900"); 
            }
            HttpContext.Current.Session["CRN"] = int.Parse(this.ddlCourse.SelectedValue);
        }

       
        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            HttpContext.Current.Session["CRN"] = int.Parse(this.ddlCourse.SelectedValue);
        }

        protected void gvwWeights_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            object[] itemParts = new object[4];
            e.Values.Values.CopyTo(itemParts,0);

            int CRN = int.Parse((string)itemParts[1]);
            string assignmentType = (string) itemParts[2];
            int weight = int.Parse((string) itemParts[3]);
            int index = int.Parse((string)itemParts[0]);

            RubricItem item = new RubricItem(CRN,assignmentType, weight,index);
            HttpContext.Current.Session["RubricItemToDelete"] = item;

        }

        #endregion

        protected void gvwWeights_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            Response.Redirect("RubricPage.aspx");
        }

        protected void dvwAddGradeItem_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            Response.Redirect("RubricPage.aspx");
        }

        protected void gvwWeights_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            Response.Redirect("RubricPage.aspx");
        }
    }
}