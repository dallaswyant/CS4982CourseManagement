using System.Collections.Generic;

namespace CourseManagement.App_Code
{
    public class GradedItem
    {
        #region Properties

        public string Name { get; }
        public Student Student { get; private set; }
        public double Grade { get; }
        public string Feedback { get; set; }
        public int PossiblePoints { get; }
        public string GradeType { get; }
        public int GradeId { get; private set; }

        #endregion

        #region Constructors

        public GradedItem(string name, Student student,double grade,
            string feedBack, int possiblePoints, string gradeType, int gradeId)
        {
            this.Name = name;
            this.Student = student;
            this.Grade = grade;
            this.Feedback = feedBack;
            this.PossiblePoints = possiblePoints;
            this.GradeType = gradeType;
            this.GradeId = gradeId;
        }

        #endregion

        #region Methods

        

        

        #endregion
    }
}