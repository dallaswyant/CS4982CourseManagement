namespace CourseManagement.App_Code
{
    public class DegreeProgram
    {
        #region Properties

        public string name { get; }
        public CourseCollection RequiredCourses { get; }

        #endregion

        #region Constructors

        public DegreeProgram(string name, CourseCollection requiredCourses)
        {
            this.name = name;
            this.RequiredCourses = requiredCourses;
        }

        #endregion
    }
}