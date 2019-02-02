using System.Collections.Generic;

namespace CourseManagement.App_Code
{
    public class GradedItem
    {
        #region Properties

        public string Name { get; set; }
        public Student Student { get;  }
        public double Grade { get; set; }
        public string Feedback { get; set; }
        public double PossiblePoints { get;  }
        public string GradeType { get;  }
        public int GradeId { get; set; }

        #endregion

        #region Constructors

        public GradedItem(string name, Student student,double grade,
            string feedBack, double possiblePoints, string gradeType, int gradeId)
        {
            this.Name = name;
            this.Student = student;
            this.Grade = grade;
            this.Feedback = feedBack;
            this.PossiblePoints = possiblePoints;
            this.GradeType = gradeType;
            this.GradeId = gradeId;
        }

        public GradedItem()
        {
        }


        #endregion

        #region Methods





        #endregion
    }
}