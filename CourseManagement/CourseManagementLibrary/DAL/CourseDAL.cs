using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CourseManagementLibrary.Model;
using MySql.Data.MySqlClient;

namespace CourseManagementLibrary.DAL
{
    /// <summary>
    /// Class Defines a CourseDAL object for interacting with courses on the database
    /// </summary>
    public class CourseDAL
    {
        /// <summary>
        /// Gets a list of course info bulletins by teacher id.
        /// </summary>
        /// <param name="teacherIDCheck">The teacher id to check.</param>
        /// <returns>A list of course info for courses taught by the teacher.</returns>
        public List<CourseInfo> GetCourseBulletinByTeacherID(string teacherIDCheck)
        {
            List<CourseInfo> courseBulletin = new List<CourseInfo>();
            List<Course> teacherCourses = this.GetCoursesByTeacherID(teacherIDCheck);
            foreach (var courses in teacherCourses)
            {
                courseBulletin.Add(courses.CourseInfo);
            }
            return courseBulletin;
        }

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
                GradedItemDAL gradedStuff = new GradedItemDAL();
                
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

                            List<GradedItem> listOfGrades = gradedStuff.GetGradedItemsByCRN(CRN);

                            CourseInfo currCourseInfo =
                                new CourseInfo(courseName, location, creditHours, CRN, sectionNumber);
                            Course currentCourse = new Course(listOfGrades, currCourseInfo, maxSeats);
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
        public CourseInfo GetCoursesByCRN(int CRN)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            using (conn)
            {
                GradedItemDAL gradedStuff = new GradedItemDAL();
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

                            List<GradedItem> listOfGrades = gradedStuff.GetGradedItemsByCRN(CRN);
                            CourseInfo currCourseInfo = new CourseInfo(courseName, location, creditHours, CRN, sectionNumber);
                            Course currentCourse = new Course(listOfGrades, currCourseInfo, maxSeats);
                            return currCourseInfo;

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
        public Course GetCourseByCRN(int CRN)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            using (conn)
            {
                GradedItemDAL gradedStuff = new GradedItemDAL();
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

                            List<GradedItem> listOfGrades = gradedStuff.GetGradedItemsByCRN(CRN);
                            
                            CourseInfo currCourseInfo = new CourseInfo(courseName, location, creditHours, CRN, sectionNumber);
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
        public List<Course> GetCoursesByStudentID(string studentUIDCheck)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            List<Course> coursesTaken = new List<Course>();
            using (conn)
            {
                GradedItemDAL gradedStuff = new GradedItemDAL();
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

                            List<GradedItem> listOfGrades = gradedStuff.GetGradedItemsByCRN(CRN);
                            CourseInfo currCourseInfo = new CourseInfo(courseName, location, creditHours, CRN, sectionNumber);
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
        public List<Course> GetCoursesByDepartmentName(string departmentName)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            List<Course> deptCourses = new List<Course>();
            using (conn)
            {
                GradedItemDAL gradedStuff = new GradedItemDAL();
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

                            List<GradedItem> listOfGrades = gradedStuff.GetGradedItemsByCRN(CRN);

                            CourseInfo currCourseInfo = new CourseInfo(courseName, location, creditHours, CRN, sectionNumber);
                            Course currentCourse = new Course(listOfGrades, currCourseInfo, maxSeats);
                            deptCourses.Add(currentCourse);

                        }
                    }

                    return deptCourses;
                }
            }
        }

        /// <summary>
        /// Gets a list of course info bulletin by student id.
        /// </summary>
        /// <param name="studentUID">The student uid.</param>
        /// <returns>A list of course info for all courses taken by the selected student</returns>
        public List<CourseInfo> GetCourseBulletinByStudentID(string studentUID)
        {
            List<CourseInfo> courseBulletin = new List<CourseInfo>();
            List<Course> studentsCourses = this.GetCoursesByStudentID(studentUID);
            foreach (var courses in studentsCourses)
            {
                courseBulletin.Add(courses.CourseInfo);
            }
            return courseBulletin;
        }

        /// <summary>
        /// Gets a list of course info bulletins by department name.
        /// </summary>
        /// <param name="departmentName">Name of the department</param>
        /// <returns>A list of course info offered by the selected department</returns>
        public List<CourseInfo> GetCourseBulletinByDepartmentName(string departmentName)
        {
            List<CourseInfo> courseBulletin = new List<CourseInfo>();
            List<Course> departmentCourses = this.GetCoursesByDepartmentName(departmentName);
            foreach (var courses in departmentCourses)
            {
                courseBulletin.Add(courses.CourseInfo);
            }
            return courseBulletin;
        }
    }
}