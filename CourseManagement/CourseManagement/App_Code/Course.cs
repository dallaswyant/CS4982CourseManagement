using System;
using System.Collections.Generic;

namespace CourseManagement.App_Code
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
        public string Name { get; }        
        
        /// <summary>
        /// gets the description
        /// </summary>
        public string Description { get;  }

        /// <summary>
        ///   Location of the Course
        /// </summary>
        public string Location { get; }

        /// <summary>
        /// Gets lecture Notes
        /// </summary>
        public List<string> LectureNotes { get; } //TODO discuss being file paths

        /// <summary>
        /// gets the credit hours
        /// </summary>
        public int CreditHours { get; }

        /// <summary>
        /// gets the crn
        /// </summary>
        public int CRN { get;}

        /// <summary>
        /// gets the section number
        /// </summary>
        public string SectionNumber { get; }

        /// <summary>
        /// Gets the Department
        /// </summary>
        public Department Department { get; }
 
        /// <summary>
        /// Gets the Max seats
        /// </summary>
        public int MaxSeats { get; }

        /// <summary>
        /// Gets the semester ID
        /// </summary>
        public string SemesterID { get; }
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
        /// <param name="department">Course department</param>
        /// <param name="maxSeats">the maximum seats</param>
        /// <param name="semesterID">The semester id</param>
        public Course(string name, string description, string location, int creditHours, int crn, string sectionNumber, Department department, int maxSeats, string semesterID)
        {
            this.Name = name;
            this.Description = description;
            this.Location = location;
            this.LectureNotes = new List<string>();
            this.CreditHours = creditHours;
            this.CRN = crn;
            this.Department = department;
            this.MaxSeats = maxSeats;
            this.SectionNumber = sectionNumber;
            this.SemesterID = semesterID;
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