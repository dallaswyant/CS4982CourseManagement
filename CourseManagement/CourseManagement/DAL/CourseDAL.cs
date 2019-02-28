﻿using System;
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
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Course> GetCoursesByTeacherID(string teacherIDCheck)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            List<Course> coursesTaught = new List<Course>();
            using (conn)
            {

                conn.Open();

                var selectQuery =
                    "select courses.* from courses, teacher_teaches_courses WHERE teacher_teaches_courses.courses_CRN = courses.CRN AND teacher_teaches_courses.teacher_uid = @teacherUID";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@teacherUID", teacherIDCheck);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {

                        int CRNOrdinal = reader.GetOrdinal("CRN");
                        int departmentOrdinal = reader.GetOrdinal("dept_name");
                        int courseNameOrdinal = reader.GetOrdinal("course_name");
                        int courseDescriptionOrdinal = reader.GetOrdinal("course_desc");
                        int sectionNumberOrdinal = reader.GetOrdinal("section_num");
                        int creditHoursOrdinal = reader.GetOrdinal("credit_hours");
                        int maxSeatsOrdinal = reader.GetOrdinal("seats_max");
                        int locationOrdinal = reader.GetOrdinal("location");
                        int semesterNameOrdinal = reader.GetOrdinal("semester_name");

                        while (reader.Read())
                        {
                            int CRN = reader[CRNOrdinal] == DBNull.Value ? default(int) : reader.GetInt32(CRNOrdinal);
                            string departmentName = reader[departmentOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(departmentOrdinal);
                            string courseName = reader[courseNameOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(courseNameOrdinal);
                            string courseDescription = reader[courseDescriptionOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(courseDescriptionOrdinal);
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
                            string semesterName = reader[semesterNameOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(semesterNameOrdinal);

                            Course currentCourse = new Course(CRN, departmentName, courseName, courseDescription,
                                sectionNumber, creditHours, maxSeats, location, semesterName);
                            currentCourse.LectureNotes.Clear();
                            coursesTaught.Add(currentCourse);

                        }

                        return coursesTaught;
                    }
                }
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Course> GetCoursesByTeacherAndSemester(string teacherIDCheck, string semesterID)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            List<Course> coursesTaught = new List<Course>();
            using (conn)
            {

                conn.Open();

                var selectQuery =
                    "SELECT courses.* FROM teacher_teaches_courses, courses WHERE teacher_teaches_courses.teacher_uid = @teacherUID AND teacher_teaches_courses.courses_CRN = courses.CRN AND courses.semester_name = @semesterID";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@teacherUID", teacherIDCheck);
                    cmd.Parameters.AddWithValue("@semesterID", semesterID);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {

                        int CRNOrdinal = reader.GetOrdinal("CRN");
                        int departmentOrdinal = reader.GetOrdinal("dept_name");
                        int courseNameOrdinal = reader.GetOrdinal("course_name");
                        int courseDescriptionOrdinal = reader.GetOrdinal("course_desc");
                        int sectionNumberOrdinal = reader.GetOrdinal("section_num");
                        int creditHoursOrdinal = reader.GetOrdinal("credit_hours");
                        int maxSeatsOrdinal = reader.GetOrdinal("seats_max");
                        int locationOrdinal = reader.GetOrdinal("location");
                        int semesterNameOrdinal = reader.GetOrdinal("semester_name");

                        while (reader.Read())
                        {
                            int CRN = reader[CRNOrdinal] == DBNull.Value ? default(int) : reader.GetInt32(CRNOrdinal);
                            string departmentName = reader[departmentOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(departmentOrdinal);
                            string courseName = reader[courseNameOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(courseNameOrdinal);
                            string courseDescription = reader[courseDescriptionOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(courseDescriptionOrdinal);
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
                            string semesterName = reader[semesterNameOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(semesterNameOrdinal);

                            Course currentCourse = new Course(CRN, departmentName, courseName, courseDescription,
                                sectionNumber, creditHours, maxSeats, location, semesterName);
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
        public Course GetCoursesByCRN(int CRNCheck)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            using (conn)
            {
                GradeItemDAL gradedStuff = new GradeItemDAL();
                conn.Open();
                var selectQuery = "SELECT * from courses WHERE courses.CRN = @CRNCheck";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@CRNCheck", CRNCheck);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int CRNOrdinal = reader.GetOrdinal("CRN");
                        int departmentOrdinal = reader.GetOrdinal("dept_name");
                        int courseNameOrdinal = reader.GetOrdinal("course_name");
                        int courseDescriptionOrdinal = reader.GetOrdinal("course_desc");
                        int sectionNumberOrdinal = reader.GetOrdinal("section_num");
                        int creditHoursOrdinal = reader.GetOrdinal("credit_hours");
                        int maxSeatsOrdinal = reader.GetOrdinal("seats_max");
                        int locationOrdinal = reader.GetOrdinal("location");
                        int semesterNameOrdinal = reader.GetOrdinal("semester_name");

                        while (reader.Read())
                        {
                            int CRN = reader[CRNOrdinal] == DBNull.Value ? default(int) : reader.GetInt32(CRNOrdinal);
                            string departmentName = reader[departmentOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(departmentOrdinal);
                            string courseName = reader[courseNameOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(courseNameOrdinal);
                            string courseDescription = reader[courseDescriptionOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(courseDescriptionOrdinal);
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
                            string semesterName = reader[semesterNameOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(semesterNameOrdinal);

                            Course currentCourse = new Course(CRN, departmentName, courseName, courseDescription,
                                sectionNumber, creditHours, maxSeats, location, semesterName);
                            currentCourse.LectureNotes.Clear();
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
        public Course GetCourseByCRN(int CRNCheck)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            using (conn)
            {
                GradeItemDAL gradedStuff = new GradeItemDAL();
                conn.Open();
                var selectQuery = "SELECT * from courses WHERE courses.CRN = @CRNCheck";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@CRNCheck", CRNCheck);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int CRNOrdinal = reader.GetOrdinal("CRN");
                        int departmentOrdinal = reader.GetOrdinal("dept_name");
                        int courseNameOrdinal = reader.GetOrdinal("course_name");
                        int courseDescriptionOrdinal = reader.GetOrdinal("course_desc");
                        int sectionNumberOrdinal = reader.GetOrdinal("section_num");
                        int creditHoursOrdinal = reader.GetOrdinal("credit_hours");
                        int maxSeatsOrdinal = reader.GetOrdinal("seats_max");
                        int locationOrdinal = reader.GetOrdinal("location");
                        int semesterNameOrdinal = reader.GetOrdinal("semester_name");

                        while (reader.Read())
                        {
                            int CRN = reader[CRNOrdinal] == DBNull.Value ? default(int) : reader.GetInt32(CRNOrdinal);
                            string departmentName = reader[departmentOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(departmentOrdinal);
                            string courseName = reader[courseNameOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(courseNameOrdinal);
                            string courseDescription = reader[courseDescriptionOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(courseDescriptionOrdinal);
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
                            string semesterName = reader[semesterNameOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(semesterNameOrdinal);

                            Course currentCourse = new Course(CRN, departmentName, courseName, courseDescription,
                                sectionNumber, creditHours, maxSeats, location, semesterName);
                            currentCourse.LectureNotes.Clear();
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
                conn.Open();
                var selectQuery =
                    "SELECT courses.* FROM courses, students, student_has_courses WHERE students.uid = student_has_courses.student_uid AND student_has_courses.courses_CRN = courses.CRN AND students.uid = @studentUID";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@studentUID", studentUIDCheck);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int CRNOrdinal = reader.GetOrdinal("CRN");
                        int departmentOrdinal = reader.GetOrdinal("dept_name");
                        int courseNameOrdinal = reader.GetOrdinal("course_name");
                        int courseDescriptionOrdinal = reader.GetOrdinal("course_desc");
                        int sectionNumberOrdinal = reader.GetOrdinal("section_num");
                        int creditHoursOrdinal = reader.GetOrdinal("credit_hours");
                        int maxSeatsOrdinal = reader.GetOrdinal("seats_max");
                        int locationOrdinal = reader.GetOrdinal("location");
                        int semesterNameOrdinal = reader.GetOrdinal("semester_name");

                        while (reader.Read())
                        {
                            int CRN = reader[CRNOrdinal] == DBNull.Value ? default(int) : reader.GetInt32(CRNOrdinal);
                            string departmentName = reader[departmentOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(departmentOrdinal);
                            string courseName = reader[courseNameOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(courseNameOrdinal);
                            string courseDescription = reader[courseDescriptionOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(courseDescriptionOrdinal);
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
                            string semesterName = reader[semesterNameOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(semesterNameOrdinal);

                            Course currentCourse = new Course(CRN, departmentName, courseName, courseDescription,
                                sectionNumber, creditHours, maxSeats, location, semesterName);
                            currentCourse.LectureNotes.Clear();
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
        public List<Course> GetCoursesByDepartmentName(string departmentCheck)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            List<Course> deptCourses = new List<Course>();
            using (conn)
            {
                conn.Open();
                var selectQuery = String.Empty;
                if (departmentCheck.Equals("All Departments"))
                {
                    selectQuery = "SELECT courses.* FROM courses";
                }
                else
                {
                    selectQuery = "SELECT courses.* FROM courses WHERE courses.dept_name = @name";
                }


                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@name", departmentCheck);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int CRNOrdinal = reader.GetOrdinal("CRN");
                        int departmentOrdinal = reader.GetOrdinal("dept_name");
                        int courseNameOrdinal = reader.GetOrdinal("course_name");
                        int courseDescriptionOrdinal = reader.GetOrdinal("course_desc");
                        int sectionNumberOrdinal = reader.GetOrdinal("section_num");
                        int creditHoursOrdinal = reader.GetOrdinal("credit_hours");
                        int maxSeatsOrdinal = reader.GetOrdinal("seats_max");
                        int locationOrdinal = reader.GetOrdinal("location");
                        int semesterNameOrdinal = reader.GetOrdinal("semester_name");

                        while (reader.Read())
                        {
                            int CRN = reader[CRNOrdinal] == DBNull.Value ? default(int) : reader.GetInt32(CRNOrdinal);
                            string departmentName = reader[departmentOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(departmentOrdinal);
                            string courseName = reader[courseNameOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(courseNameOrdinal);
                            string courseDescription = reader[courseDescriptionOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(courseDescriptionOrdinal);
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
                            string semesterName = reader[semesterNameOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(semesterNameOrdinal);

                            Course currentCourse = new Course(CRN, departmentName, courseName, courseDescription,
                                sectionNumber, creditHours, maxSeats, location, semesterName);
                            currentCourse.LectureNotes.Clear();
                            deptCourses.Add(currentCourse);


                        }
                    }

                    return deptCourses;
                }
            }
        }

        public Dictionary<string, char> GetPrerequisiteCoursesForGivenCRN(int CRNCheck)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            Dictionary<string, char> preReqs = new Dictionary<string, char>();
            using (conn)
            {
                conn.Open();
                var selectQuery =
                    "select prereq_courses.* from courses, prereq_courses WHERE courses.course_name = prereq_courses.desired_course_name AND courses.CRN = @CRNCheck";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@CRNCheck", CRNCheck);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int requiredCourseOrdinal = reader.GetOrdinal("required_course_name");
                        int requiredGradeOrdinal = reader.GetOrdinal("required_grade");

                        while (reader.Read())
                        {
                            string requiredCourseName = reader[requiredCourseOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(requiredCourseOrdinal);
                            char requiredGrade = reader[requiredGradeOrdinal] == DBNull.Value
                                ? default(char)
                                : reader.GetChar(requiredGradeOrdinal);


                            preReqs.Add(requiredCourseName, requiredGrade);


                        }

                        return preReqs;
                    }
                }
            }
        }

        public Dictionary<string, char> GetPrerequisiteCoursesForGivenCourseName(string courseName)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            Dictionary<string, char> preReqs = new Dictionary<string, char>();
            using (conn)
            {
                conn.Open();
                var selectQuery =
                    "select prereq_courses.* from prereq_courses WHERE prereq_courses.desired_course_name = @desired_course";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@desired_course", courseName);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int requiredCourseOrdinal = reader.GetOrdinal("required_course_name");
                        int requiredGradeOrdinal = reader.GetOrdinal("required_grade");


                        while (reader.Read())
                        {
                            string requiredCourseName = reader[requiredCourseOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(requiredCourseOrdinal);
                            char requiredGrade = reader[requiredGradeOrdinal] == DBNull.Value
                                ? default(char)
                                : reader.GetChar(requiredGradeOrdinal);


                            preReqs.Add(requiredCourseName, requiredGrade);


                        }

                        return preReqs;
                    }
                }
            }
        }

        public List<char> GetGradesEarnedForCompletedCourse(string courseName, string studentUID)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            List<char> grades = new List<char>();
            using (conn)
            {
                conn.Open();
                var selectQuery =
                    "SELECT student_has_courses.grade_earned FROM student_has_courses, courses WHERE student_has_courses.courses_CRN = courses.CRN AND student_has_courses.student_uid = @studentUID AND student_has_courses.courses_CRN = courses.CRN AND courses.course_name = @course_name";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@studentUID", studentUID);
                    cmd.Parameters.AddWithValue("@course_name", courseName);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int gradeEarnedOrdinal = reader.GetOrdinal("grade_earned");

                        while (reader.Read())
                        {
                            char earnedGrade = reader[gradeEarnedOrdinal] == DBNull.Value
                                ? default(char)
                                : reader.GetChar(gradeEarnedOrdinal);


                            grades.Add(earnedGrade);


                        }

                        return grades;
                    }
                }
            }
        }
    }
}