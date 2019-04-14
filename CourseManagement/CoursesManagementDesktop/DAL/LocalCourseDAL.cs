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
        /// Gets the courses by teacher id.
        /// </summary>
        /// <param name="teacherIDCheck">The teacher id to check.</param>
        /// <returns>
        /// A list of courses taught by teacherIDCheck
        /// </returns>
        /// <preconditions>
        /// Teacher name cannot be null
        /// </preconditions>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Course> GetCoursesByTeacherID(string teacherIDCheck)
        {
            if (teacherIDCheck == null)
            {
                throw new Exception("Teacher name cannot be null");
            }
            SQLiteConnection dbConnection = LocalDbConnection.GetConnection();
            List<Course> coursesTaught = new List<Course>();
            using (dbConnection)
            {

                dbConnection.Open();

                var selectQuery =
                    "select courses.* from courses, teacher_teaches_courses WHERE teacher_teaches_courses.courses_CRN = courses.CRN AND teacher_teaches_courses.teacher_uid = @teacherUID";

                using (SQLiteCommand cmd = new SQLiteCommand(selectQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@teacherUID", teacherIDCheck);
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
                            int CRN = queryResultReader[CRNOrdinal] == DBNull.Value ? default(int) : queryResultReader.GetInt32(CRNOrdinal);
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
                            int CRN = queryResultReader[CRNOrdinal] == DBNull.Value ? default(int) : queryResultReader.GetInt32(CRNOrdinal);
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


        /// <summary>
        /// Gets the course by CRN.
        /// </summary>
        /// <param name="CRNCheck">The CRN to check.</param>
        /// <returns>
        /// A course object with the selected CRNCheck 
        /// </returns>
        /// <preconditions>
        /// CRNCheck must be greater than or equal to 0
        /// </preconditions>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public Course GetCourseByCRN(int CRNCheck)
        {
            if (CRNCheck <= 0)
            {
                throw new Exception("CRNCheck must be greater than or equal to 0");
            }
            SQLiteConnection dbConnection = LocalDbConnection.GetConnection();
            using (dbConnection)
            {
                dbConnection.Open();
                var selectQuery = "SELECT * from courses WHERE courses.CRN = @CRNCheck";

                using (SQLiteCommand cmd = new SQLiteCommand(selectQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@CRNCheck", CRNCheck);
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
                            int CRN = queryResultReader[CRNOrdinal] == DBNull.Value ? default(int) : queryResultReader.GetInt32(CRNOrdinal);
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
                            return currentCourse;
                        }
                    }
                }

                dbConnection.Close();
            }

            return null;
        }


        /// <summary>
        /// Gets the courses by student id.
        /// </summary>
        /// <param name="studentUIDCheck">The student uid check.</param>
        /// <returns>
        /// A list of courses taken by the selected studentUIDCheck
        /// </returns>
        /// <preconditions>
        /// Student UID cannot be null
        /// </preconditions>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Course> GetCoursesByStudentID(string studentUIDCheck)
        {
            if (studentUIDCheck == null)
            {
                throw new Exception("Student UID cannot be null");
            }
            SQLiteConnection dbConnection = LocalDbConnection.GetConnection();
            List<Course> coursesTaken = new List<Course>();
            using (dbConnection)
            {
                dbConnection.Open();
                var selectQuery =
                    "SELECT courses.* FROM courses, students, student_has_courses WHERE students.uid = student_has_courses.student_uid AND student_has_courses.courses_CRN = courses.CRN AND students.uid = @studentUID";

                using (SQLiteCommand cmd = new SQLiteCommand(selectQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@studentUID", studentUIDCheck);
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
                            int CRN = queryResultReader[CRNOrdinal] == DBNull.Value ? default(int) : queryResultReader.GetInt32(CRNOrdinal);
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
                            coursesTaken.Add(currentCourse);

                        }
                    }
                    return coursesTaken;
                }
            }

        }

        /// <summary>
        /// Gets the courses by student identifier and semester.
        /// </summary>
        /// <param name="studentUIDCheck">The student uid check.</param>
        /// <param name="semesterName">Name of the semester.</param>
        /// <returns>
        /// A list of all the courses taken by the selected studentUIDCheck
        /// during the selected semesterName
        /// </returns>
        /// <preconditions>
        /// Teacher name cannot be null
        /// AND
        /// Semester name cannot be null
        /// </preconditions>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Course> GetCoursesByStudentIDAndSemester(string studentUIDCheck, string semesterName)
        {
            if (studentUIDCheck == null)
            {
                throw new Exception("Teacher name cannot be null");
            }
            if (semesterName == null)
            {
                throw new Exception("Semester name cannot be null");
            }
            SQLiteConnection dbConnection = LocalDbConnection.GetConnection();
            List<Course> coursesTaken = new List<Course>();
            using (dbConnection)
            {
                dbConnection.Open();
                var selectQuery =
                    "SELECT courses.* FROM courses, students, student_has_courses WHERE students.uid = student_has_courses.student_uid AND student_has_courses.courses_CRN = courses.CRN AND students.uid = @studentUID AND courses.semester_name = @semester_name";

                using (SQLiteCommand cmd = new SQLiteCommand(selectQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@studentUID", studentUIDCheck);
                    cmd.Parameters.AddWithValue("@semester_name", semesterName);
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
                            int CRN = queryResultReader[CRNOrdinal] == DBNull.Value ? default(int) : queryResultReader.GetInt32(CRNOrdinal);
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
                            semesterName = queryResultReader[semesterNameOrdinal] == DBNull.Value
                                ? default(string)
                                : queryResultReader.GetString(semesterNameOrdinal);
                            int courseTimeID = queryResultReader[courseTimeIDOrdinal] == DBNull.Value
                                ? default(int)
                                : queryResultReader.GetInt32(courseTimeIDOrdinal);

                            Course currentCourse = new Course(CRN, departmentName, courseName, courseDescription,
                                sectionNumber, maxSeats, location, semesterName, courseTimeID);
                            coursesTaken.Add(currentCourse);

                        }
                    }
                    return coursesTaken;
                }
            }
        }



        /// <summary>
        /// Gets the name of the courses by department.
        /// </summary>
        /// <param name="departmentCheck">The department name to check.</param>
        /// <returns>
        /// A list of all courses offered by the given departmentCheck
        /// </returns>
        /// <preconditions>
        /// Department name cannot be null
        /// </preconditions>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Course> GetCoursesByDepartmentName(string departmentCheck)
        {
            if (departmentCheck == null)
            {
                throw new Exception("Department name cannot be null");
            }
            SQLiteConnection dbConnection = LocalDbConnection.GetConnection();
            List<Course> deptCourses = new List<Course>();
            using (dbConnection)
            {
                dbConnection.Open();
                var selectQuery = String.Empty;
                if (departmentCheck.Equals("All Departments"))
                {
                    selectQuery = "SELECT courses.* FROM courses";
                }
                else
                {
                    selectQuery = "SELECT courses.* FROM courses WHERE courses.dept_name = @name";
                }


                using (SQLiteCommand cmd = new SQLiteCommand(selectQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@name", departmentCheck);
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
                            int CRN = queryResultReader[CRNOrdinal] == DBNull.Value ? default(int) : queryResultReader.GetInt32(CRNOrdinal);
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
                            deptCourses.Add(currentCourse);


                        }
                    }

                    return deptCourses;
                }
            }
        }

        /// <summary>
        /// Gets the courses by department name and semester.
        /// </summary>
        /// <param name="departmentCheck">The department to check.</param>
        /// <param name="semesterName">Name of the semester.</param>
        /// <returns>
        /// A list of all the courses offered by the selected
        /// departmentCheck during the selected semesterName
        /// </returns>
        /// <preconditions>
        /// Department name cannot be null
        /// AND
        /// Semester name cannot be null
        /// </preconditions>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Course> GetCoursesByDepartmentNameAndSemester(string departmentCheck, string semesterName)
        {
            if (departmentCheck == null)
            {
                throw new Exception("Department name cannot be null");
            }
            if (semesterName == null)
            {
                throw new Exception("Semester name cannot be null");
            }
            SQLiteConnection dbConnection = LocalDbConnection.GetConnection();
            List<Course> deptCourses = new List<Course>();
            using (dbConnection)
            {
                dbConnection.Open();
                var selectQuery = String.Empty;
                if (departmentCheck.Equals("All Departments"))
                {
                    selectQuery = "SELECT courses.* FROM courses WHERE semester_name = @semester_name";
                }
                else
                {
                    selectQuery = "SELECT courses.* FROM courses WHERE courses.dept_name = @name AND semester_name = @semester_name";
                }


                using (SQLiteCommand cmd = new SQLiteCommand(selectQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@name", departmentCheck);
                    cmd.Parameters.AddWithValue("@semester_name", semesterName);
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
                            int CRN = queryResultReader[CRNOrdinal] == DBNull.Value ? default(int) : queryResultReader.GetInt32(CRNOrdinal);
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
                            semesterName = queryResultReader[semesterNameOrdinal] == DBNull.Value
                                ? default(string)
                                : queryResultReader.GetString(semesterNameOrdinal);
                            int courseTimeID = queryResultReader[courseTimeIDOrdinal] == DBNull.Value
                                ? default(int)
                                : queryResultReader.GetInt32(courseTimeIDOrdinal);

                            Course currentCourse = new Course(CRN, departmentName, courseName, courseDescription,
                                sectionNumber, maxSeats, location, semesterName, courseTimeID);
                            deptCourses.Add(currentCourse);


                        }
                    }

                    return deptCourses;
                }
            }
        }

        /// <summary>
        /// Gets the prerequisite courses for given CRN.
        /// </summary>
        /// <param name="CRNCheck">The CRN to check.</param>
        /// <returns>
        /// A Dictionary representing the prerequisite courses
        /// for the selected courseName
        /// </returns>
        /// <preconditions>
        /// CRNCheck must be greater than or equal to 0
        /// </preconditions>
        public Dictionary<string, char> GetPrerequisiteCoursesForGivenCRN(int CRNCheck)
        {
            if (CRNCheck <= 0)
            {
                throw new Exception("CRNCheck must be greater than or equal to 0");
            }
            SQLiteConnection dbConnection = LocalDbConnection.GetConnection();
            Dictionary<string, char> preRequisiteCourses = new Dictionary<string, char>();
            using (dbConnection)
            {
                dbConnection.Open();
                var selectQuery =
                    "select prereq_courses.* from courses, prereq_courses WHERE courses.course_name = prereq_courses.desired_course_name AND courses.CRN = @CRNCheck";

                using (SQLiteCommand cmd = new SQLiteCommand(selectQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@CRNCheck", CRNCheck);
                    using (SQLiteDataReader queryResultReader = cmd.ExecuteReader())
                    {
                        int requiredCourseOrdinal = queryResultReader.GetOrdinal("required_course_name");
                        int requiredGradeOrdinal = queryResultReader.GetOrdinal("required_grade");

                        while (queryResultReader.Read())
                        {
                            string requiredCourseName = queryResultReader[requiredCourseOrdinal] == DBNull.Value
                                ? default(string)
                                : queryResultReader.GetString(requiredCourseOrdinal);
                            char requiredGrade = queryResultReader[requiredGradeOrdinal] == DBNull.Value
                                ? default(char)
                                : queryResultReader.GetChar(requiredGradeOrdinal);


                            preRequisiteCourses.Add(requiredCourseName, requiredGrade);
                        }

                        return preRequisiteCourses;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the name of the prerequisite courses for given course.
        /// </summary>
        /// <param name="courseName">Name of the course.</param>
        /// <returns>
        /// A Dictionary representing the prerequisite courses
        /// for the selected courseName
        /// </returns>
        /// <preconditions>
        /// Course name cannot be null
        /// </preconditions>
        public Dictionary<string, char> GetPrerequisiteCoursesForGivenCourseName(string courseName)
        {
            if (courseName == null)
            {
                throw new Exception("Course name cannot be null");
            }
            SQLiteConnection dbConnection = LocalDbConnection.GetConnection();
            Dictionary<string, char> preRequisiteCourses = new Dictionary<string, char>();
            using (dbConnection)
            {
                dbConnection.Open();
                var selectQuery =
                    "select prereq_courses.* from prereq_courses WHERE prereq_courses.desired_course_name = @desired_course";

                using (SQLiteCommand cmd = new SQLiteCommand(selectQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@desired_course", courseName);
                    using (SQLiteDataReader queryResultReader = cmd.ExecuteReader())
                    {
                        int requiredCourseOrdinal = queryResultReader.GetOrdinal("required_course_name");
                        int requiredGradeOrdinal = queryResultReader.GetOrdinal("required_grade");


                        while (queryResultReader.Read())
                        {
                            string requiredCourseName = queryResultReader[requiredCourseOrdinal] == DBNull.Value
                                ? default(string)
                                : queryResultReader.GetString(requiredCourseOrdinal);
                            char requiredGrade = queryResultReader[requiredGradeOrdinal] == DBNull.Value
                                ? default(char)
                                : queryResultReader.GetChar(requiredGradeOrdinal);


                            preRequisiteCourses.Add(requiredCourseName, requiredGrade);


                        }

                        return preRequisiteCourses;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the grades earned for completed course.
        /// </summary>
        /// <param name="courseName">Name of the course.</param>
        /// <param name="studentUID">The student uid.</param>
        /// <returns>
        /// A list of the grades earned for the selected
        /// courseName for the selected studentUID
        /// </returns>
        /// <preconditions>
        /// courseName cannot be null
        /// AND
        /// studentUID cannot be null
        /// </preconditions>
        public List<char> GetGradesEarnedForCompletedCourse(string courseName, string studentUID)
        {
            if (courseName == null)
            {
                throw new Exception("Course name cannot be null");
            }
            if (studentUID == null)
            {
                throw new Exception("Student UID cannot be null");
            }
            SQLiteConnection dbConnection = LocalDbConnection.GetConnection();
            List<char> grades = new List<char>();
            using (dbConnection)
            {
                dbConnection.Open();
                var selectQuery =
                    "SELECT student_has_courses.grade_earned FROM student_has_courses, courses WHERE student_has_courses.courses_CRN = courses.CRN AND student_has_courses.student_uid = @studentUID AND student_has_courses.courses_CRN = courses.CRN AND courses.course_name = @course_name";

                using (SQLiteCommand cmd = new SQLiteCommand(selectQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@studentUID", studentUID);
                    cmd.Parameters.AddWithValue("@course_name", courseName);
                    using (SQLiteDataReader queryResultReader = cmd.ExecuteReader())
                    {
                        int gradeEarnedOrdinal = queryResultReader.GetOrdinal("grade_earned");

                        while (queryResultReader.Read())
                        {
                            char earnedGrade = queryResultReader[gradeEarnedOrdinal] == DBNull.Value
                                ? default(char)
                                : queryResultReader.GetChar(gradeEarnedOrdinal);


                            grades.Add(earnedGrade);


                        }

                        return grades;
                    }
                }
            }
        }
    }
}