using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseManagement.Models
{
    public class Transcript
    {

        public DegreeProgram DegreeProgram { get; }

        public List<StudentCourseReport> CourseReports { get; set; }
        public List<Course> Courses { get; }
        public Student Student { get; }
        public Transcript(DegreeProgram program, Student student, List<Course> coursesTaken)
        {
            this.DegreeProgram = program;
            this.Student = student;
            List<Course> requiredCourses = this.DegreeProgram.RequiredCourses.Courses;
            this.CourseReports = new List<StudentCourseReport>();
           
            requiredCourses.AddRange(coursesTaken.Where(courseTaken =>
                requiredCourses.All(coursesRequired => coursesRequired.CRN != courseTaken.CRN)));
            this.Courses = new List<Course>(requiredCourses);
        }

        public class StudentCourseReport : Course
        {
            public enum Status { Default, InProgress, Completed, AwaitingGrade }
            public DegreeProgram DegreeProgram { get; private set; }
            public String DegreeProgramName => this.DegreeProgram.Name;
            private char? grade;
            public bool IsRequired => this.DegreeProgram.RequiredCourses.Courses.Exists(course => course.CRN == this.CRN);
            public String CourseStatus { get; private set; }
            public Student Student { get; }
            public StudentCourseReport(Course course, DegreeProgram program, Student student, char? grade, CourseTime time) 
                : base(course.CRN, course.DepartmentName,course.Name,course.Description,course.SectionNumber,course.MaxSeats, 
                    course.Location,course.SemesterID,course.CourseTimeID)
            {

                this.DegreeProgram = program;
                this.Student = student;

                this.grade = grade;
                if (time.CourseStart < DateTime.Now && DateTime.Now < time.CourseEnd)
                {
                    this.setStatus(Status.InProgress);
                }
                else if (this.grade == '\0' || this.grade == null)
                {
                    if (DateTime.Now > time.CourseEnd)
                    {
                        this.setStatus(Status.AwaitingGrade);
                    }
                    else
                    {
                        this.setStatus(Status.Default);
                    }
                }
                else
                {
                    this.setStatus(Status.Completed);
                }


            }


            private void setStatus(Status status)
            {
                switch (status)
                {
                    case Status.InProgress:
                        this.CourseStatus =  "In Progress";
                        break;
                    case Status.Completed:
                            this.CourseStatus = this.grade.ToString();
                        break;
                    case Status.AwaitingGrade:
                            this.CourseStatus = "Awaiting Grade";
                        break;
                    case Status.Default:
                        this.CourseStatus = "";
                        break;

                }
            }
        }
    }
}