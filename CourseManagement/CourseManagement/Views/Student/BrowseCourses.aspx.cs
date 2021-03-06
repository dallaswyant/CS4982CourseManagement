﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CourseManagement.DAL;
using CourseManagement.Models;
using CourseManagement.Utilities;


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
                    this.ddlDepartments.Items.Add(dept.Name);
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
                    SemesterDAL semesterChecker = new SemesterDAL();
                    if (semesterChecker.CheckIfAddDropHasPassed(this.ddlSemester.SelectedValue))
                    {
                        CourseSignUpHelper helper = new CourseSignUpHelper();
                        if (helper.CheckIfStudentCanSignUpForCourseBasedOnPreReqs(crn, current.UserId))
                        {
                            if (helper.CheckIfCanSignUpForCourseBasedOnTimes(crn, current.UserId,
                                this.ddlSemester.SelectedValue))
                            {
                                courseAdder.addCourseByCRNAndStudentUID(crn, current.UserId);
                                Response.Redirect("BrowseCourses.aspx", false);
                            }
                            else
                            {
                                this.lblCourseToAdd.Text =
                                    "You can't sign up for this course, its time conflicts with another course you have already signed up for.";
                            }
                        }
                        else
                        {
                            this.lblCourseToAdd.Text =
                                "You can't sign up for this course due to pre-requisite requirements" + Environment.NewLine;
                            this.lblCourseToAdd.Text += helper.GetPreReqsAndFormatForDisplay(crn);
                        }
                    }
                    else
                    {
                        {
                            this.lblCourseToAdd.Text = "The add drop deadline for this semester has already passed.";
                        }
                    }

                }

                catch (Exception ex)
                {
                    if (ex.Message.Equals("This course is still in progress."))
                    {
                        this.lblCourseToAdd.Text = ex.Message;
                    }
                    else
                    {
                        this.lblCourseToAdd.Text = "You are already in this course";
                        HttpContext.Current.Session["chosenCRN"] = null;
                    }
                }
            }
            
        }

        protected void AvailableCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            int crn = (int)this.AvailableCoursesGrid.SelectedValue;
            HttpContext.Current.Session["chosenCRN"] = crn;
            CourseDAL courseGetter = new CourseDAL();
            Course courseToAdd = courseGetter.GetCourseByCRN(crn);
            TeacherDAL dal = new TeacherDAL();
            Models.Teacher instructor = dal.GetTeacherByCRN(crn);
            string instructorName = instructor != null ? instructor.Name : "TBA"; 
            this.lblCourseToAdd.Text = "Course to Add: " + courseToAdd.CRN + " " + courseToAdd.Name + " " +
                                           courseToAdd.SectionNumber + " Instructor: " + instructorName;
            CourseSignUpHelper degreeChecker = new CourseSignUpHelper();
            var current = HttpContext.Current.Session["User"] as User;
            if (!degreeChecker.CheckIfCourseContributesToMajor(crn, current.UserId))
            {
                this.lblCourseToAdd.Text +=
                    Environment.NewLine + "This course does not contribute to your degree program.";
            }
        }

        protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.browseCoursUpdatePanel.Update();
        }
    }
}