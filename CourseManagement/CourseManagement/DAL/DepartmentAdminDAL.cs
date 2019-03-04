using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using CourseManagement.Models;
using CourseManagements;
using MySql.Data.MySqlClient;

namespace CourseManagement.DAL
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
        public Department GetDepartmentByUserID(string userID)
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
        public List<Course> GetDepartmentCoursesByUserID(string userID)
        {
            if (userID == null)
            {
                throw new Exception("User ID cannot be null");
            }
            Department currentDepartment = this.GetDepartmentByUserID(userID);
            CourseDAL courseDal = new CourseDAL();
            return courseDal.GetCoursesByDepartmentName(currentDepartment.Name);
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
        public void InsertNewCourse( Course newCourse)
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
                        "INSERT INTO courses (dept_name, course_name, course_description, section_num, credit_hours, seats_max, location, semester_name) VALUES (@dept_name, @course_name, @section_num, @credit_hours, @seats_max, @location)";
                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, dbConnection))
                    {
                        cmd.Parameters.AddWithValue("@dept_name",newCourse.DepartmentName);
                        cmd.Parameters.AddWithValue("@course_name", newCourse.Name);
                        cmd.Parameters.AddWithValue("@course_description", newCourse.Description);
                        cmd.Parameters.AddWithValue("@section_num", newCourse.SectionNumber);
                        cmd.Parameters.AddWithValue("@credit_hours", newCourse.CreditHours);
                        cmd.Parameters.AddWithValue("@seats_max", newCourse.MaxSeats);
                        cmd.Parameters.AddWithValue("@location", newCourse.Location);
                        cmd.Parameters.AddWithValue("@semester_name", newCourse.SemesterID);
                        cmd.ExecuteNonQuery();
                    }

                    insertQuery =
                        "INSERT INTO dept_offers_courses(dept_name, courses_CRN) VALUES (@department_name, (SELECT courses.CRN FROM courses WHERE dept_name = @dept_name AND course_name = @course_name AND section_num = @section_num))";
                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, dbConnection))
                    {
                        cmd.Parameters.AddWithValue("@department_name", newCourse.DepartmentName);
                        cmd.Parameters.AddWithValue("@course_name", newCourse.Name);
                        cmd.Parameters.AddWithValue("@section_num", newCourse.SectionNumber);
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
                        "UPDATE courses SET course_name=@course_name, section_num=@section_num, course_desc = @course_description, credit_hours=@credit_hours, seats_max=@seats_max, location=@location, semester_name = @semester_name, dept_name=@department WHERE CRN = @CRN";
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@course_name", course.Name);
                    cmd.Parameters.AddWithValue("@section_num", course.SectionNumber);
                    cmd.Parameters.AddWithValue("@course_description", course.Description);
                    cmd.Parameters.AddWithValue("@credit_hours", course.CreditHours);
                    cmd.Parameters.AddWithValue("@seats_max", course.MaxSeats);
                    cmd.Parameters.AddWithValue("@location", course.Location);
                    cmd.Parameters.AddWithValue("@CRN", course.CRN);
                    cmd.Parameters.AddWithValue("@semester_name", course.SemesterID);
                    cmd.Parameters.AddWithValue("@department", course.DepartmentName);
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


        /// <summary>
        /// Gets all teachers by admin department.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// A list of all the teachers in the admin's department
        /// </returns>
        /// <preconditions>
        /// User cannot be null
        /// </preconditions>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Teacher> GetAllTeachersByAdminDepartment(User user)
        {
            if (user == null)
            {
                throw new Exception("User cannot be null");
            }
            string department = GetAdminDepartment(user);

            MySqlConnection dbConnection = DbConnection.GetConnection();

            using (dbConnection)
            {
                dbConnection.Open();
                var selectQuery =
                    "select * FROM dept_employs_teachers WHERE dept_name=@dept_name";
                List<Teacher> teachers = new List<Teacher>();
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@dept_name", department);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int teacherIDOrdinal = reader.GetOrdinal("teacher_uid");

                        while (reader.Read())
                        {
                            var teacherID = reader[teacherIDOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(teacherIDOrdinal);
                            TeacherDAL teacherGetter = new TeacherDAL();
                            Teacher teacher = teacherGetter.GetTeacherByTeacherID(teacherID);
     
                            teachers.Add(teacher);
                        }
                        return teachers;
                    }
                }
            }
        }

 
        private string GetAdminDepartment(User user)
        {


            MySqlConnection dbConnection = DbConnection.GetConnection();
            string department = "";
            using (dbConnection)
            {
                dbConnection.Open();
                var selectQuery =
                    "select * FROM department_admins WHERE admin_uid=@admin_uid";
                List<Teacher> teachers = new List<Teacher>();
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@admin_uid", user.UserId);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int departmentOrdinal = reader.GetOrdinal("department_name");

                        while (reader.Read())
                        {
                            department = reader[departmentOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(departmentOrdinal);
                        }

                        return department;
                    }
                }
            }
        }
    }
}