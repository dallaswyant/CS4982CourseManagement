namespace CourseManagement.App_Code
{
    public class CourseInfo
    {
        #region Properties

        public string Name { get; set; }
        public string Description { get; }
        public Teacher Teacher { get; }
        public string Location { get; }
        public CourseCollection PreReqClasses { get; }
        public int CreditHours { get; }
        public int CRN { get; }
        public string SectionNumber { get; }

        #endregion

        #region Constructors

        public CourseInfo(string name, Teacher teacher, string location,
             int creditHours, int CRN, string sectionNumber)
        {
            this.Name = name;
            this.Teacher = teacher;
            this.Location = location;
            this.CreditHours = creditHours;
            this.CRN = CRN;
            this.SectionNumber = sectionNumber;
        }

        #endregion
    }
}