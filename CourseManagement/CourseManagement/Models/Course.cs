using System;
using System.Collections.Generic;

namespace CourseManagement.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Course
    {
        #region Properties

        /// <summary>
        /// gets or sets the Name
        /// </summary>
        public string Name { get; set; }        
        
        /// <summary>
        /// gets the description
        /// </summary>
        public string Description { get;  set; }  

        /// <summary>
        ///   Location of the Course
        /// </summary>
        public string Location { get; set; }  

        /// <summary>
        /// Gets lecture Notes
        /// </summary>
        public List<string> LectureNotes { get; set; }   //TODO discuss being file paths

        /// <summary>
        /// gets the credit hours
        /// </summary>
        public int CreditHours { get; set; }  

        /// <summary>
        /// gets the crn
        /// </summary>
        public int CRN { get; set; }  

        /// <summary>
        /// gets the section number
        /// </summary>
        public string SectionNumber { get; set; }  

        /// <summary>
        /// Gets the Department
        /// </summary>
        public string DepartmentName { get; set; }  
 
        /// <summary>
        /// Gets the Max seats
        /// </summary>
        public int MaxSeats { get; set; }  

        /// <summary>
        /// Gets the semester ID
        /// </summary>
        public string SemesterID { get; set; }

        public int CourseTimeID { get; set; }
        #endregion

        #region Constructors

        /// <summary>Constructor for course</summary>
        /// <param name="name">Course name</param>
        /// <param name="description">Course description</param>
        /// <param name="location">Course location</param>
        /// <param name="lectureNotes">Course lecture notes</param>
        /// <param name="creditHours">Credit hours</param>
        /// <param name="crn">Course crn</param>
        /// <param name="sectionNumber">Section number</param>
        /// <param name="departmentName">Course department</param>
        /// <param name="maxSeats">the maximum seats</param>
        /// <param name="semesterID">The semester id</param>
        public Course(int crn, string departmentName, string name, string description, string sectionNumber, int creditHours, int maxSeats, string location, string semesterID, int courseTimeId)
        {
            this.CRN = crn;
            this.DepartmentName = departmentName;
            this.Name = name;
            this.Description = description;
            this.SectionNumber = sectionNumber;
            this.CreditHours = creditHours;
            this.MaxSeats = maxSeats;
            this.Location = location;
            this.SemesterID = semesterID;
            this.CourseTimeID = courseTimeId;
            this.LectureNotes = new List<string>();
        }

        public Course()
        {

        }
        #endregion


        /// <summary>
        /// Adds the note.
        /// </summary>
        /// <param name="note">The note.</param>
        public void AddNote(string note)
        {
            this.LectureNotes.Add(note);
        }
    }
}