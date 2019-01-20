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
        public string Identifier { get; }
        public int SectionNumber { get; }

        #endregion

        #region Constructors

        public CourseInfo(string name, string description, Teacher teacher, string location,
            CourseCollection preReqClasses, int creditHours, string identifier, int sectionNumber)
        {
            this.Name = name;
            this.Description = description;
            this.Teacher = teacher;
            this.Location = location;
            this.PreReqClasses = preReqClasses;
            this.CreditHours = creditHours;
            this.Identifier = identifier;
            this.SectionNumber = sectionNumber;
        }

        #endregion
    }
}