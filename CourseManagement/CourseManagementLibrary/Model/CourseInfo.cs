namespace CourseManagementLibrary.Model
{
    public class CourseInfo
    {
        #region Properties
        /// <summary>
        /// gets or sets the Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// gets the description
        /// </summary>
        public string Description { get; }
        /// <summary>
        /// gets the teacher
        /// </summary>
        public Teacher Teacher { get; }
        /// <summary>
        /// gets the location
        /// </summary>
        public string Location { get; }
        /// <summary>
        /// gets the pre req classes
        /// </summary>
        public CourseCollection PreReqClasses { get; }
        /// <summary>
        /// gets the credit hours
        /// </summary>
        public int CreditHours { get; }
        /// <summary>
        /// gets the crn
        /// </summary>
        public int CRN { get; }
        /// <summary>
        /// gets the section number
        /// </summary>
        public string SectionNumber { get; }

        #endregion

        #region Constructors
        /// <summary>
        /// constructor for course info
        /// </summary>
        /// <param name="name">name of the course</param>
        /// <param name="location">location of the course</param>
        /// <param name="creditHours"> credit hours for the cours</param>
        /// <param name="CRN">crn of the course</param>
        /// <param name="sectionNumber">section number of the course</param>
        public CourseInfo(string name, string location,
             int creditHours, int CRN, string sectionNumber)
        {
            this.Name = name;
          //  this.Teacher = teacher;
            this.Location = location;
            this.CreditHours = creditHours;
            this.CRN = CRN;
            this.SectionNumber = sectionNumber;
        }

        #endregion
    }
}