using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using CourseManagement.Models;
using CoursesManagementDesktop.DAL;

namespace CourseManagementDesktop.DAL
{
    /// <summary>
    /// Class Defines a CourseDAL object for interacting with courses on the database
    /// </summary>
    [DataObject(true)]
    public class LocalCourseDAL
    {

        /// <summary>
        /// Gets the courses by teacher and semester.
        /// </summary>
        /// <param name="teacherIDCheck">The teacher identifier to check.</param>
        /// <param name="semesterID">The semester identifier.</param>
        /// <returns>
        /// A list of courses taught by teacherIDCheck
        /// during the semester semesterID
        /// </returns>
        /// <preconditions>
        /// Teacher name cannot be null
        /// AND
        /// Semester name cannot be null
        /// </preconditions>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Course> GetCoursesByTeacherAndSemester(string teacherIDCheck, string semesterID)
        {
            if (teacherIDCheck == null)
            {
                throw new Exception("Teacher name cannot be null");
            }

            if (semesterID == null)
            {
                throw new Exception("Semester name cannot be null");
            }

            SQLiteConnection dbConnection = LocalDbConnection.GetConnection();
            List<Course> coursesTaught = new List<Course>();
            using (dbConnection)
            {

                dbConnection.Open();

                var selectQuery =
                    "SELECT courses.* FROM teacher_teaches_courses, courses WHERE teacher_teaches_courses.teacher_uid = @teacherUID AND teacher_teaches_courses.courses_CRN = courses.CRN AND courses.semester_name = @semesterID";

                using (SQLiteCommand cmd = new SQLiteCommand(selectQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@teacherUID", teacherIDCheck);
                    cmd.Parameters.AddWithValue("@semesterID", semesterID);
                    using (SQLiteDataReader queryResultReader = cmd.ExecuteReader())
                    {

                        int CRNOrdinal = queryResultReader.GetOrdinal("CRN");
                        int departmentOrdinal = queryResultReader.GetOrdinal("dept_name");
                        int courseNameOrdinal = queryResultReader.GetOrdinal("course_name");
                        int courseDescriptionOrdinal = queryResultReader.GetOrdinal("course_desc");
                        int sectionNumberOrdinal = queryResultReader.GetOrdinal("section_num");
                        int maxSeatsOrdinal = queryResultReader.GetOrdinal("seats_max");
                        int locationOrdinal = queryResultReader.GetOrdinal("location");
                        int semesterNameOrdinal = queryResultReader.GetOrdinal("semester_name");
                        int courseTimeIDOrdinal = queryResultReader.GetOrdinal("course_time_id");

                        while (queryResultReader.Read())
                        {
                            int CRN = queryResultReader[CRNOrdinal] == DBNull.Value
                                ? default(int)
                                : queryResultReader.GetInt32(CRNOrdinal);
                            string departmentName = queryResultReader[departmentOrdinal] == DBNull.Value
                                ? default(string)
                                : queryResultReader.GetString(departmentOrdinal);
                            string courseName = queryResultReader[courseNameOrdinal] == DBNull.Value
                                ? default(string)
                                : queryResultReader.GetString(courseNameOrdinal);
                            string courseDescription = queryResultReader[courseDescriptionOrdinal] == DBNull.Value
                                ? default(string)
                                : queryResultReader.GetString(courseDescriptionOrdinal);
                            string sectionNumber = queryResultReader[sectionNumberOrdinal] == DBNull.Value
                                ? default(string)
                                : queryResultReader.GetString(sectionNumberOrdinal);
                            int maxSeats = queryResultReader[maxSeatsOrdinal] == DBNull.Value
                                ? default(int)
                                : queryResultReader.GetInt32(maxSeatsOrdinal);
                            string location = queryResultReader[locationOrdinal] == DBNull.Value
                                ? default(string)
                                : queryResultReader.GetString(locationOrdinal);
                            string semesterName = queryResultReader[semesterNameOrdinal] == DBNull.Value
                                ? default(string)
                                : queryResultReader.GetString(semesterNameOrdinal);
                            int courseTimeID = queryResultReader[courseTimeIDOrdinal] == DBNull.Value
                                ? default(int)
                                : queryResultReader.GetInt32(courseTimeIDOrdinal);

                            Course currentCourse = new Course(CRN, departmentName, courseName, courseDescription,
                                sectionNumber, maxSeats, location, semesterName, courseTimeID);
                            coursesTaught.Add(currentCourse);

                        }

                        return coursesTaught;
                    }
                }
            }
        }
    }
}