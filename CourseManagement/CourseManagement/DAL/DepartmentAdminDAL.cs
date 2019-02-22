using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using CourseManagement.App_Code;
using MySql.Data.MySqlClient;

namespace CourseManagement.DAL
{
    [DataObject(true)]
    public class DepartmentAdminDAL
    {
        public Department GetDepartmentByUserID(string userID)
        {
                MySqlConnection conn = DbConnection.GetConnection();

                using (conn)
                {
                    conn.Open();
                    var selectQuery =
                        "select departments.* FROM department_admins, departments WHERE departments.name = department_admins.department_name AND department_admins.admin_uid = @user_uid";
                    
                    using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
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

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Course> GetDepartmentCoursesByUserID(string userID)
        {
            Department currentDepartment = GetDepartmentByUserID(userID);
            CourseDAL courseDal = new CourseDAL();
            return courseDal.GetCoursesByDepartmentName(currentDepartment.Name);
        }


        //Add new course
        //INSERT INTO courses (dept_name, course_name, section_num, credit_hours, seats_max, location) VALUES (@dept_name, @course_name, @section_num, @credit_hours, @seats_max, @location)
        //THEN
        //INSERT INTO dept_offers_courses(dept_name, courses_CRN) VALUES (@department_name, @CRN)
        [DataObjectMethod(DataObjectMethodType.Insert)]
        public void InsertNewCourse(Course newCourse, int maxSeats, string departmentName)
        {
            MySqlConnection conn = DbConnection.GetConnection();

            using (conn)
            {
                conn.Open();
                    
                    
                    var selectQuery =
                        "INSERT INTO courses (dept_name, course_name, course_description, section_num, credit_hours, seats_max, location, semester_name) VALUES (@dept_name, @course_name, @section_num, @credit_hours, @seats_max, @location)";
                    using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@dept_name",departmentName);
                        cmd.Parameters.AddWithValue("@course_name", newCourse.Name);
                        cmd.Parameters.AddWithValue("@course_description", newCourse.Description);
                        cmd.Parameters.AddWithValue("@section_num", newCourse.SectionNumber);
                        cmd.Parameters.AddWithValue("@credit_hours", newCourse.CreditHours);
                        cmd.Parameters.AddWithValue("@seats_max", maxSeats);
                        cmd.Parameters.AddWithValue("@location", newCourse.Location);
                        cmd.Parameters.AddWithValue("@semester_name", newCourse.SemesterID);
                        cmd.ExecuteNonQuery();
                    }

                    var query =
                        "INSERT INTO dept_offers_courses(dept_name, courses_CRN) VALUES (@department_name, (SELECT courses.CRN FROM courses WHERE dept_name = @dept_name AND course_name = @course_name AND section_num = @section_num))";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@department_name", departmentName);
                        cmd.Parameters.AddWithValue("@course_name", newCourse.Name);
                        cmd.Parameters.AddWithValue("@section_num", newCourse.SectionNumber);
                        cmd.ExecuteNonQuery();
                    }
                
                conn.Close();
            }

        }

        //Delete course
        //DELETE FROM courses WHERE CRN = @CRN;
        //THEN
        //DELETE FROM dept_offers_courses WHERE dept_name = @dept_name AND courses_CRN = @CRN
        [DataObjectMethod(DataObjectMethodType.Delete)]
        public void DeleteCourseByDepartmentAndCRN(string departmentName, int CRN)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            using (conn)
            {
                conn.Open();
                var selectQuery = "DELETE FROM courses WHERE CRN = @CRN";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@CRN", CRN);
                    cmd.ExecuteNonQuery();
                }

                selectQuery = "DELETE FROM dept_offers_courses WHERE dept_name = @dept_name AND courses_CRN = @CRN";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@CRN", CRN);
                    cmd.Parameters.AddWithValue("@dept_name", departmentName);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }

        }
        //Edit course
        //UPDATE courses SET (course_name=@course_name, section_num=@section_num, credit_hours=@credit_hours, seats_max=@seats_max, location=@location, semester_id=@semester_id) WHERE CRN = @CRN;
        public void UpdateCourse(Course course)
        {
            MySqlConnection conn = DbConnection.GetConnection();

            using (conn)
            {
                conn.Open();
                    
                    var selectQuery =
                        "UPDATE courses SET (course_name=@course_name, section_num=@section_num, course_description = @course_description, credit_hours=@credit_hours, seats_max=@seats_max, location=@location, semester_name = @semester_name) WHERE CRN = @CRN";
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@course_name", course.Name);
                    cmd.Parameters.AddWithValue("@section_num", course.SectionNumber);
                    cmd.Parameters.AddWithValue("@course_description", course.Description);
                    cmd.Parameters.AddWithValue("@credit_hours", course.CreditHours);
                    cmd.Parameters.AddWithValue("@seats_max", course.MaxSeats);
                    cmd.Parameters.AddWithValue("@location", course.Location);
                    cmd.Parameters.AddWithValue("@CRN", course.CRN);
                    cmd.Parameters.AddWithValue("@semester_name", course.SemesterID);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        //Assign teacher to course
        //INSERT INTO teacher_teaches_courses (teacher_uid, courses_CRN) VALUES (@teacher_UID, @CRN)
        [DataObjectMethod(DataObjectMethodType.Insert)]
        public void AssignTeacherToCourse(Teacher teacher, int CRN)
        {
            MySqlConnection conn = DbConnection.GetConnection();

            using (conn)
            {
                conn.Open();


                var selectQuery =
                    "INSERT INTO teacher_teaches_courses (teacher_uid, courses_CRN) VALUES (@teacher_UID, @CRN)";
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@teacherUID", teacher.TeacherUID);
                    cmd.Parameters.AddWithValue("@CRN", CRN);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }

        }

        /// <summary>
        /// Gets a list of all teachers for admin's department.
        /// </summary>
        /// <returns>A list of all departments.</returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Teacher> GetAllTeachersByAdminDepartment(User user)
        {

            string department = GetAdminDepartment(user);

            MySqlConnection conn = DbConnection.GetConnection();

            using (conn)
            {
                conn.Open();
                var selectQuery =
                    "select * FROM dept_employs_teachers WHERE dept_name=@dept_name";
                List<Teacher> teachers = new List<Teacher>();
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
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


            MySqlConnection conn = DbConnection.GetConnection();
            string department = "";
            using (conn)
            {
                conn.Open();
                var selectQuery =
                    "select * FROM department_admins WHERE admin_uid=@admin_uid";
                List<Teacher> teachers = new List<Teacher>();
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
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