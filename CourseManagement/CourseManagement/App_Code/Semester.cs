using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseManagement.App_Code
{
    public class Semester
    {
        /// <summary>Gets the semester identifier.</summary>
        /// <value>The semester identifier.</value>
        public string SemesterID { get; }
        

        /// <summary>
        /// Gets the Drop dead line
        /// </summary>
        public DateTime AddDropDeadline { get; }
        /// <summary>Gets the withdraw deadline.</summary>
        /// <value>The withdraw deadline.</value>
        public DateTime WithdrawDeadline { get; }
        /// <summary>
        /// The start date
        /// </summary>
        public DateTime StartDate { get; }
        /// <summary>
        /// The end date
        /// </summary>
        public DateTime EndDate { get; }
        /// <summary>
        /// The semester constructor
        /// </summary>
        /// <param name="semesterID"></param>
        public Semester(string semesterID, DateTime addDropDeadline, DateTime withdrawDeadline, DateTime startDate, DateTime endDate)
        {
            this.SemesterID = semesterID;
            this.AddDropDeadline = addDropDeadline;
            this.StartDate = startDate;
            this.WithdrawDeadline = withdrawDeadline;
            this.EndDate = endDate;
        }
    }
}