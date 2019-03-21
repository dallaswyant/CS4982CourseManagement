namespace CourseManagement.Models
{
    //TODO not in use discuss removal
    public class DegreeProgram
    {
        #region Properties
        /// <summary>
        /// gets the name
        /// </summary>
        public string name { get; }
        /// <summary>
        /// gets the required courses
        /// </summary>
        public CourseCollection RequiredCourses { get; }

        #endregion

        #region Constructors
        /// <summary>
        /// Constructor for degree program
        /// </summary>
        /// <param name="name">name of the program</param>
        /// <param name="requiredCourses"> courses required</param>
        public DegreeProgram(string name, CourseCollection requiredCourses)
        {
            this.name = name;
            this.RequiredCourses = requiredCourses;
        }

        #endregion
    }
}