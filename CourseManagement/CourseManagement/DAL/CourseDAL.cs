using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using CourseManagement.App_Code;
using MySql.Data.MySqlClient;

namespace CourseManagement.DAL
{
    /// <summary>
    /// Class Defines a CourseDAL object for interacting with courses on the database
    /// </summary>
    [DataObject(true)]
    public class CourseDAL
    {

        /// <summary>
        /// Gets a list of courses by teacher id.
        /// </summary>
        /// <param name="teacherIDCheck">The teacher id to check.</param>
        /// <returns>A list of courses taught by the teacher</returns>
        public List<Course> GetCoursesByTeacherID(string teacherIDCheck)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            List<Course> coursesTaught = new List<Course>();
            using (conn)
            {

                conn.Open();
                GradeItemDAL gradedStuff = new GradeItemDAL();
                
                var selectQuery = "select courses.* from courses, teacher_teaches_courses WHERE teacher_teaches_courses.courses_CRN = courses.CRN AND teacher_teaches_courses.teacher_uid = @teacherUID";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@teacherUID", teacherIDCheck);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {

                        int CRNOrdinal = reader.GetOrdinal("CRN");
                        int courseNameOrdinal = reader.GetOrdinal("course_name");
                        int sectionNumberOrdinal = reader.GetOrdinal("section_num");
                        int creditHoursOrdinal = reader.GetOrdinal("credit_hours");
                        int maxSeatsOrdinal = reader.GetOrdinal("seats_max");
                        int locationOrdinal = reader.GetOrdinal("location");

                        while (reader.Read())
                        {
                            int CRN = reader[CRNOrdinal] == DBNull.Value ? default(int) : reader.GetInt32(CRNOrdinal);
                            string courseName = reader[courseNameOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(courseNameOrdinal);
                            string sectionNumber = reader[sectionNumberOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(sectionNumberOrdinal);
                            int creditHours = reader[creditHoursOrdinal] == DBNull.Value
                                ? default(int)
                                : reader.GetInt32(creditHoursOrdinal);
                            int maxSeats = reader[maxSeatsOrdinal] == DBNull.Value
                                ? default(int)
                                : reader.GetInt32(maxSeatsOrdinal);
                            string location = reader[locationOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(locationOrdinal);

                            List<GradeItem> listOfGrades = gradedStuff.GetGradedItemsByCRN(CRN);
                            Course currentCourse = new Course(listOfGrades, currCourseInfo, maxSeats);
                            currentCourse.LectureNotes.Clear();
                            coursesTaught.Add(currentCourse);

                        }

                        return coursesTaught;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the information about a course by CRN.
        /// </summary>
        /// <param name="CRN">The CRN.</param>
        /// <returns>Course information about the course with the selected CRN</returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public Course GetCoursesByCRN(int CRN)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            using (conn)
            {
                GradeItemDAL gradedStuff = new GradeItemDAL();
                conn.Open();
                var selectQuery = "SELECT * from courses WHERE courses.CRN = @CRNCheck";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@CRNCheck", CRN);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int courseNameOrdinal = reader.GetOrdinal("course_name");
                        int sectionNumberOrdinal = reader.GetOrdinal("section_num");
                        int creditHoursOrdinal = reader.GetOrdinal("credit_hours");
                        int maxSeatsOrdinal = reader.GetOrdinal("seats_max");
                        int locationOrdinal = reader.GetOrdinal("location");

                        while (reader.Read())
                        {
                            string courseName = reader[courseNameOrdinal] == DBNull.Value ? default(string) : reader.GetString(courseNameOrdinal);
                            string sectionNumber = reader[sectionNumberOrdinal] == DBNull.Value ? default(string) : reader.GetString(sectionNumberOrdinal);
                            int creditHours = reader[creditHoursOrdinal] == DBNull.Value ? default(int) : reader.GetInt32(creditHoursOrdinal);
                            int maxSeats = reader[maxSeatsOrdinal] == DBNull.Value ? default(int) : reader.GetInt32(maxSeatsOrdinal);
                            string location = reader[locationOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(locationOrdinal);

                            List<GradeItem> listOfGrades = gradedStuff.GetGradedItemsByCRN(CRN);
                            Course currentCourse = new Course(listOfGrades, currCourseInfo, maxSeats);
                            return currentCourse;

                        }
                    }
                }
                conn.Close();
            }

            return null;
        }

        /// <summary>
        /// Gets the course by its CRN.
        /// </summary>
        /// <param name="CRN">The CRN.</param>
        /// <returns>The course with the selected CRN</returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public Course GetCourseByCRN(int CRN)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            using (conn)
            {
                GradeItemDAL gradedStuff = new GradeItemDAL();
                conn.Open();
                var selectQuery = "SELECT * from courses WHERE courses.CRN = @CRNCheck";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@CRNCheck", CRN);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int courseNameOrdinal = reader.GetOrdinal("course_name");
                        int sectionNumberOrdinal = reader.GetOrdinal("section_num");
                        int creditHoursOrdinal = reader.GetOrdinal("credit_hours");
                        int maxSeatsOrdinal = reader.GetOrdinal("seats_max");
                        int locationOrdinal = reader.GetOrdinal("location");

                        while (reader.Read())
                        {
                            string courseName = reader[courseNameOrdinal] == DBNull.Value ? default(string) : reader.GetString(courseNameOrdinal);
                            string sectionNumber = reader[sectionNumberOrdinal] == DBNull.Value ? default(string) : reader.GetString(sectionNumberOrdinal);
                            int creditHours = reader[creditHoursOrdinal] == DBNull.Value ? default(int) : reader.GetInt32(creditHoursOrdinal);
                            int maxSeats = reader[maxSeatsOrdinal] == DBNull.Value ? default(int) : reader.GetInt32(maxSeatsOrdinal);
                            string location = reader[locationOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(locationOrdinal);

                            List<GradeItem> listOfGrades = gradedStuff.GetGradedItemsByCRN(CRN);
                            
                            Course currentCourse = new Course(listOfGrades, currCourseInfo, maxSeats);
                            return currentCourse;

                        }
                    }
                }
                conn.Close();
            }

            return null;
        }

        /// <summary>
        /// Gets a list of courses by student id.
        /// </summary>
        /// <param name="studentUIDCheck">The student uid to check.</param>
        /// <returns>A list of courses taken by the selected student</returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Course> GetCoursesByStudentID(string studentUIDCheck)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            List<Course> coursesTaken = new List<Course>();
            using (conn)
            {
                GradeItemDAL gradedStuff = new GradeItemDAL();
                conn.Open();
                var selectQuery = "SELECT courses.* FROM courses, students, student_has_courses WHERE students.uid = student_has_courses.student_uid AND student_has_courses.courses_CRN = courses.CRN AND students.uid = @studentUID";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@studentUID", studentUIDCheck);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int CRNOrdinal = reader.GetOrdinal("CRN");
                        int courseNameOrdinal = reader.GetOrdinal("course_name");
                        int sectionNumberOrdinal = reader.GetOrdinal("section_num");
                        int creditHoursOrdinal = reader.GetOrdinal("credit_hours");
                        int maxSeatsOrdinal = reader.GetOrdinal("seats_max");
                        int locationOrdinal = reader.GetOrdinal("location");

                        while (reader.Read())
                        {
                            int CRN = reader[CRNOrdinal] == DBNull.Value ? default(int) : reader.GetInt32(CRNOrdinal);
                            string courseName = reader[courseNameOrdinal] == DBNull.Value ? default(string) : reader.GetString(courseNameOrdinal);
                            string sectionNumber = reader[sectionNumberOrdinal] == DBNull.Value ? default(string) : reader.GetString(sectionNumberOrdinal);
                            int creditHours = reader[creditHoursOrdinal] == DBNull.Value ? default(int) : reader.GetInt32(creditHoursOrdinal);
                            int maxSeats = reader[maxSeatsOrdinal] == DBNull.Value ? default(int) : reader.GetInt32(maxSeatsOrdinal);
                            string location = reader[locationOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(locationOrdinal);

                            List<GradeItem> listOfGrades = gradedStuff.GetGradedItemsByCRN(CRN);
                            Course currentCourse = new Course(listOfGrades, currCourseInfo, maxSeats);
                            coursesTaken.Add(currentCourse);

                        }
                    }

                    return coursesTaken;
                }
                conn.Close();
            }

            return null;
        }

        /// <summary>
        /// Gets a list of courses by department name.
        /// </summary>
        /// <param name="departmentName">Name of the department to check</param>
        /// <returns>A list of courses taught by the selected department</returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Course> GetCoursesByDepartmentName(string departmentName)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            List<Course> deptCourses = new List<Course>();
            using (conn)
            {
                GradeItemDAL gradedStuff = new GradeItemDAL();
                conn.Open();
                var selectQuery = String.Empty;
                if (departmentName.Equals("All Departments"))
                {
                    selectQuery = "SELECT courses.* FROM courses, dept_offers_courses WHERE courses.CRN = dept_offers_courses.courses_CRN";
                }
                else
                {
                    selectQuery = "SELECT courses.* FROM courses, dept_offers_courses WHERE courses.CRN = dept_offers_courses.courses_CRN AND dept_offers_courses.dept_name = @deptName";
                }
               

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@deptName", departmentName);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int CRNOrdinal = reader.GetOrdinal("CRN");
                        int courseNameOrdinal = reader.GetOrdinal("course_name");
                        int sectionNumberOrdinal = reader.GetOrdinal("section_num");
                        int creditHoursOrdinal = reader.GetOrdinal("credit_hours");
                        int maxSeatsOrdinal = reader.GetOrdinal("seats_max");
                        int locationOrdinal = reader.GetOrdinal("location");

                        while (reader.Read())
                        {
                            int CRN = reader[CRNOrdinal] == DBNull.Value ? default(int) : reader.GetInt32(CRNOrdinal);
                            string courseName = reader[courseNameOrdinal] == DBNull.Value ? default(string) : reader.GetString(courseNameOrdinal);
                            string sectionNumber = reader[sectionNumberOrdinal] == DBNull.Value ? default(string) : reader.GetString(sectionNumberOrdinal);
                            int creditHours = reader[creditHoursOrdinal] == DBNull.Value ? default(int) : reader.GetInt32(creditHoursOrdinal);
                            int maxSeats = reader[maxSeatsOrdinal] == DBNull.Value ? default(int) : reader.GetInt32(maxSeatsOrdinal);
                            string location = reader[locationOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(locationOrdinal);

                            List<GradeItem> listOfGrades = gradedStuff.GetGradedItemsByCRN(CRN);

                            Course currentCourse = new Course(listOfGrades, currCourseInfo, maxSeats);
                            deptCourses.Add(currentCourse);

                        }
                    }

                    return deptCourses;
                }
            }
        }
    }
}