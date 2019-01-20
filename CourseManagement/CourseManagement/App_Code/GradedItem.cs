using System.Collections.Generic;

namespace CourseManagement.App_Code
{
    public class GradedItem
    {
        #region Properties

        public string Name { get; }
        public Dictionary<Student, double> StudentGrades { get; }
        public Dictionary<Student, string> StudentFeedBack { get; }
        public int PossiblePoints { get; }
        public string GradeType { get; }

        #endregion

        #region Constructors

        public GradedItem(string name, Dictionary<Student, double> studentGrades,
            Dictionary<Student, string> studentFeedBack, int possiblePoints, string gradeType)
        {
            this.Name = name;
            this.StudentGrades = studentGrades;
            this.StudentFeedBack = studentFeedBack;
            this.PossiblePoints = possiblePoints;
            this.GradeType = gradeType;
        }

        #endregion

        #region Methods

        public void addGrade(Student student, double grade)
        {
            //TODO error handling
            this.StudentGrades.Add(student, grade);
        }

        public void addFeedback(Student student, string feedback)
        {
            //TODO error handling
            this.StudentFeedBack.Add(student, feedback);
        }

        #endregion
    }
}