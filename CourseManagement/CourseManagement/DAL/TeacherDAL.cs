using System;
using System.Collections.Generic;
using CourseManagement.App_Code;
using MySql.Data.MySqlClient;

namespace CourseManagement.DAL
{
    public class TeacherDAL
    {
        #region Methods

        public Teacher GetAllTeachers()
        {
            MySqlConnection conn = DbConnection.GetConnection();

            using (conn)
            {
                conn.Open();
                var selectQuery =
                    "select * FROM teachers";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int teacherUIDOrdinal = reader.GetOrdinal("uid");
                        int officeLocationOrdinal = reader.GetOrdinal("office_location");
                        int nameOrdinal = reader.GetOrdinal("Name");
                        int emailOrdinal = reader.GetOrdinal("email");
                        int publicEmailOrdinal = reader.GetOrdinal("public_email");
                        int phoneOrdinal = reader.GetOrdinal("phone_number");

                        while (reader.Read())
                        {
                            var officeLocation = reader[officeLocationOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(officeLocationOrdinal);
                            var name = reader[nameOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(nameOrdinal);
                            var email = reader[emailOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(emailOrdinal);
                            var publicEmail = reader[publicEmailOrdinal] != DBNull.Value && reader.GetBoolean(publicEmailOrdinal);
                            var phone = reader[phoneOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(phoneOrdinal);
                            var teacherUID = reader[teacherUIDOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(teacherUIDOrdinal);
                            CourseDAL courseGetter = new CourseDAL();
                            List<Course> currTeacherCourses = courseGetter.GetCoursesByTeacherID(teacherUID);
                            Teacher currTeacher = new Teacher(officeLocation, name, email, publicEmail, phone, currTeacherCourses, teacherUID);

                            return currTeacher;

                        }
                    }
                }

                conn.Close();
            }

            return null;
        }

        public Teacher GetTeacherByTeacherID(string teacherIDCheck)
        {
            MySqlConnection conn = DbConnection.GetConnection();

            using (conn)
            {
                conn.Open();
                var selectQuery =
                    "select teachers.*, dept_employs_teachers.dept_name FROM teachers, dept_employs_teachers WHERE teachers.uid = dept_employs_teachers.teacher_uid AND teachers.uid = @teacherUID";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@teacherUID", teacherIDCheck);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int teacherUIDOrdinal = reader.GetOrdinal("uid");
                        int officeLocationOrdinal = reader.GetOrdinal("office_location");
                        int nameOrdinal = reader.GetOrdinal("Name");
                        int emailOrdinal = reader.GetOrdinal("email");
                        int publicEmailOrdinal = reader.GetOrdinal("public_email");
                        int phoneOrdinal = reader.GetOrdinal("phone_number");

                        while (reader.Read())
                        {
                            var officeLocation = reader[officeLocationOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(officeLocationOrdinal);
                            var name = reader[nameOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(nameOrdinal);
                            var email = reader[emailOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(emailOrdinal);
                            var publicEmail = reader[publicEmailOrdinal] != DBNull.Value && reader.GetBoolean(publicEmailOrdinal);
                            var phone = reader[phoneOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(phoneOrdinal);
                            var teacherUID = reader[teacherUIDOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(teacherUIDOrdinal);
                            CourseDAL courseGetter = new CourseDAL();
                            List<Course> currTeacherCourses = courseGetter.GetCoursesByTeacherID(teacherUID);
                            Teacher currTeacher = new Teacher(officeLocation, name, email, publicEmail, phone, currTeacherCourses,teacherUID);

                            return currTeacher;
                            
                        }
                    }
                }

                conn.Close();
            }

            return null;
        }

        #endregion
    }
}