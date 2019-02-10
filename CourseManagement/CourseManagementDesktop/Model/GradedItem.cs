using System.Collections.Generic;

namespace CourseManagement.App_Code
{
    public class GradedItem
    {
        #region Properties
        /// <summary>
        /// gets or sets the name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// gets the student
        /// </summary>
        public Student Student { get;  }
        /// <summary>
        /// gets or sets the grade
        /// </summary>
        public double Grade { get; set; }
        /// <summary>
        /// gets or sets the feedback
        /// </summary>
        public string Feedback { get; set; }
        /// <summary>
        /// gets the possible points
        /// </summary>
        public double PossiblePoints { get;  }
        /// <summary>
        /// gets the gradetype
        /// </summary>
        public string GradeType { get;  }
        /// <summary>
        /// gets or sets the Grade ID
        /// </summary>
        public int GradeId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is public.
        /// </summary>
        public bool IsPublic { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is graded.
        /// </summary>
        public bool IsGraded { get; set; }

        #endregion

        #region Constructors
        /// <summary>
        /// Constructor for gradeItem
        /// </summary>
        /// <param name="name">the name of the grade item</param>
        /// <param name="student">the student assigne to the grade item</param>
        /// <param name="grade">the grade</param>
        /// <param name="feedBack">the feedback</param>
        /// <param name="possiblePoints">the possible points</param>
        /// <param name="gradeType">the grade type</param>
        /// <param name="gradeId">the grade id</param>
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
        /// <summary>
        /// default constructor
        /// </summary>
        public GradedItem()
        {
        }


        #endregion

        #region Methods





        #endregion
    }
}