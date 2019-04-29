using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Admin.Models;
using Admin;
using MySql.Data.MySqlClient;

namespace Admin.DAL
{
    /// <summary>
    /// Defines a DepartmentAdminDAL object for communication with the DB
    /// </summary>
    [DataObject(true)]
    public class DepartmentAdminDAL
    {
        /// <summary>
        /// Gets the department by the user id.
        /// </summary>
        /// <param name="userID">The user id to check.</param>
        /// <returns>
        /// A department object for for the given userID
        /// </returns>
        /// <preconditions>
        /// User ID cannot be null
        /// </preconditions>
        public Department GetDepartmentByUser(string userID)
        {
            if (userID == null)
            {
                throw new Exception("User ID cannot be null");
            }
            MySqlConnection dbConnection = DbConnection.GetConnection();

            using (dbConnection)
            {
                dbConnection.Open();
                var selectQuery =
                    "select departments.* FROM department_admins, departments WHERE departments.name = department_admins.department_name AND department_admins.admin_uid = @user_uid";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@user_uid", userID);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int departmentNameOrdinal = reader.GetOrdinal("name");
                        int chairOrdinal = reader.GetOrdinal("chair_uid");

                        while (reader.Read())
                        {
                            var departmentName = reader[departmentNameOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(departmentNameOrdinal);
                            var chairUID = reader[chairOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(chairOrdinal);
                            TeacherDAL teacherGetter = new TeacherDAL();
                            Teacher chair = teacherGetter.GetTeacherByTeacherID(chairUID);
                            Department dept = new Department(chair, departmentName);
                            return dept;
                        }

                    }
                }

            }

            return null;
        }

        /// <summary>
        /// Gets the department courses by user identifier.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// A list of courses for the department
        /// that the userID is a part of
        /// </returns>
        /// <preconditions>
        /// User ID cannot be null
        /// </preconditions>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Course> GetCoursesByDepartment(string department)
        {
            if (department == null)
            {
                throw new Exception("User ID cannot be null");
            }
            CourseDAL courseDal = new CourseDAL();
            return courseDal.GetCoursesByDepartmentName(department);
        }


        /// <summary>
        /// Inserts the new course.
        /// </summary>
        /// <param name="newCourse">The new course.</param>
        /// <preconditions>
        /// The new course cannot be null
        /// </preconditions>
        /// <postcondition>
        /// A new course has been inserted in the DB
        /// </postcondition>
        [DataObjectMethod(DataObjectMethodType.Insert)]
        public void InsertNewCourse(Course newCourse)
        {
            if (newCourse == null)
            {
                throw new Exception("The new course cannot be null");
            }
            MySqlConnection dbConnection = DbConnection.GetConnection();

            using (dbConnection)
            {
                dbConnection.Open();


                var insertQuery =
                    "INSERT INTO courses (dept_name, course_name, course_desc, section_num, seats_max, location, semester_name, course_time_id) VALUES (@dept_name, @course_name, @course_description, @section_num, @seats_max, @location, @semester_name, @course_time_id)";
                using (MySqlCommand cmd = new MySqlCommand(insertQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@dept_name", newCourse.DepartmentName);
                    cmd.Parameters.AddWithValue("@course_name", newCourse.Name);
                    cmd.Parameters.AddWithValue("@course_description", newCourse.Description);
                    cmd.Parameters.AddWithValue("@section_num", newCourse.SectionNumber);
                    cmd.Parameters.AddWithValue("@seats_max", newCourse.MaxSeats);
                    cmd.Parameters.AddWithValue("@location", newCourse.Location);
                    cmd.Parameters.AddWithValue("@semester_name", newCourse.SemesterID);
                    cmd.Parameters.AddWithValue("@course_time_id", newCourse.CourseTimeID);
                    cmd.ExecuteNonQuery();
                }

                dbConnection.Close();
            }

        }



        /// <summary>
        /// Deletes the course by department and CRN.
        /// </summary>
        /// <param name="course">The course.</param>
        /// <preconditions>
        /// The course cannot be null
        /// </preconditions>
        /// <postcondition>
        /// The given course has now been deleted from the DB
        /// </postcondition>
        [DataObjectMethod(DataObjectMethodType.Delete)]
        public void DeleteCourseByDepartmentAndCRN(Course course)
        {
            if (course == null)
            {
                throw new Exception("The course cannot be null");
            }
            MySqlConnection dbConnection = DbConnection.GetConnection();
            using (dbConnection)
            {
                dbConnection.Open();
                var selectQuery = "DELETE FROM courses WHERE CRN = @CRN";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@CRN", course.CRN);
                    cmd.ExecuteNonQuery();
                }

                selectQuery = "DELETE FROM dept_offers_courses WHERE dept_name = @dept_name AND courses_CRN = @CRN";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@CRN", course.CRN);
                    cmd.Parameters.AddWithValue("@dept_name", course.DepartmentName);
                    cmd.ExecuteNonQuery();
                }
                dbConnection.Close();
            }

        }

        public void UpdateNeedsFixingCourse(Course course)
        {

        }



        /// <summary>
        /// Updates the course.
        /// </summary>
        /// <param name="course">
        /// The course.
        /// </param>
        /// <preconditions>
        /// Course cannot be null
        /// </preconditions>
        /// <postcondition>
        /// The course has now been updated on the DB
        /// </postcondition>
        public void UpdateCourse(Course course)
        {
            if (course == null)
            {
                throw new Exception("Course cannot be null");
            }
            MySqlConnection dbConnection = DbConnection.GetConnection();

            using (dbConnection)
            {
                dbConnection.Open();

                var selectQuery =
                    "UPDATE courses SET course_name=@course_name, section_num=@section_num, course_desc = @course_description, seats_max=@seats_max, location=@location, semester_name = @semester_name, dept_name=@department, course_time_id = @course_time_id WHERE CRN = @CRN";
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@course_name", course.Name);
                    cmd.Parameters.AddWithValue("@section_num", course.SectionNumber);
                    cmd.Parameters.AddWithValue("@course_description", course.Description);
                    cmd.Parameters.AddWithValue("@seats_max", course.MaxSeats);
                    cmd.Parameters.AddWithValue("@location", course.Location);
                    cmd.Parameters.AddWithValue("@CRN", course.CRN);
                    cmd.Parameters.AddWithValue("@semester_name", course.SemesterID);
                    cmd.Parameters.AddWithValue("@department", course.DepartmentName);
                    cmd.Parameters.AddWithValue("@course_time_id", course.CourseTimeID);
                    cmd.ExecuteNonQuery();
                }

                dbConnection.Close();
            }
        }

        /// <summary>
        /// Assigns the teacher to course.
        /// </summary>
        /// <param name="teacher">The teacher.</param>
        /// <param name="CRNCheck">The CRN.</param>
        [DataObjectMethod(DataObjectMethodType.Insert)]
        public void AssignTeacherToCourse(Teacher teacher, int CRNCheck)
        {
            if (teacher == null)
            {
                throw new Exception("Teacher cannot be null");
            }

            if (CRNCheck <= 0)
            {
                throw new Exception("CRN must be greater than or equal to 0");
            }

            MySqlConnection dbConnection = DbConnection.GetConnection();

            using (dbConnection)
            {
                dbConnection.Open();


                var selectQuery =
                    "INSERT INTO teacher_teaches_courses (teacher_uid, courses_CRN) VALUES (@teacher_UID, @CRN)";
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@teacher_UID", teacher.TeacherUID);
                    cmd.Parameters.AddWithValue("@CRN", CRNCheck);
                    cmd.ExecuteNonQuery();
                }

                dbConnection.Close();
            }

        }





    }
}