using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseManagement.App_Code;

namespace CourseManagement.Models
{
    public class GradeItem
    {
        public string Name { get; private set; }
        public Dictionary<Student,double> StudentGrades { get; private set; }
        public Dictionary<Student,string> StudentFeedBack { get; private set; }

        public GradeItem(string name, Dictionary<Student, double> studentGrades, Dictionary<Student, string> studentFeedBack)
        {
            Name = name;
            StudentGrades = studentGrades;
            StudentFeedBack = studentFeedBack;
        }

        public void addGrade(Student student,double grade)
        {
            //TODO error handling
            this.StudentGrades.Add(student,grade);
        }

        public void addFeedback(Student student,string feedback)
        {
            //TODO error handling
            this.StudentFeedBack.Add(student,feedback);
        }
    }
}
