using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseManagement.Models;

namespace CourseManagement.DAL
{
    public class TranscriptDAL
    {

        public List<Transcript.StudentCourseReport> GetDegreeReport(string studentIDCheck)
        {
            DegreeProgramDAL programDAL = new DegreeProgramDAL();
            String degreeProgram = programDAL.GetDegreeProgramByStudentID(studentIDCheck);
            CourseCollection requiredCourses = programDAL.GetCoursesByDegreeProgram(degreeProgram);
            DegreeProgram program = new DegreeProgram(degreeProgram, requiredCourses);
            StudentDAL studentDal = new StudentDAL();
            Student student = studentDal.GetStudentByStudentID(studentIDCheck);
            CourseDAL courseDal = new CourseDAL();
            List<Course> coursesTaken = courseDal.GetCoursesByStudentID(studentIDCheck);
            Transcript transcript = new Transcript(program, student, coursesTaken);
            CourseTimeDAL courseTimeDal = new CourseTimeDAL();

            foreach (var course in transcript.Courses)
            {
                char? grade = studentDal.GetGradeByCourseAndStudentID(course.CRN, studentIDCheck);
                CourseTime time = courseTimeDal.GetCourseTimeByCRN(course.CRN);
                Transcript.StudentCourseReport courseReport = new Transcript.StudentCourseReport(course,program,student,grade,time);
                transcript.CourseReports.Add(courseReport);
            }
            return transcript.CourseReports;
        }
    }
}