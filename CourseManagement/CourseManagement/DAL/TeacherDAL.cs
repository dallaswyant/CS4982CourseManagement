using System;
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
                    "select teachers.*, dept_employs_teachers.dept_name FROM teachers, dept_employs_teachers WHERE teachers.teacher_id = dept_employs_teachers.teacher_id";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int officeLocationOrdinal = reader.GetOrdinal("office_location");
                        int nameOrdinal = reader.GetOrdinal("name");
                        int emailOrdinal = reader.GetOrdinal("email");
                        int publicEmailOrdinal = reader.GetOrdinal("public_email");
                        int phoneOrdinal = reader.GetOrdinal("phone_number");
                        int teacherIDOrdinal = reader.GetOrdinal("teacher_id");

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
                            var teacherID = reader[teacherIDOrdinal] == DBNull.Value
                                ? default(int)
                                : reader.GetInt32(teacherIDOrdinal);
                            CourseDAL courseGetter = new CourseDAL();
                            CourseCollection currTeacherCourses = courseGetter.GetCourseByTeacherID(teacherID);
                            Teacher currTeacher = new Teacher(officeLocation, name, email, publicEmail, phone, currTeacherCourses);

                            return currTeacher;

                        }
                    }
                }

                conn.Close();
            }

            return null;
        }

        public Teacher GetTeacherByTeacherID(int teacherIDCheck)
        {
            MySqlConnection conn = DbConnection.GetConnection();

            using (conn)
            {
                conn.Open();
                var selectQuery =
                    "select teachers.*, dept_employs_teachers.dept_name FROM teachers, dept_employs_teachers WHERE teachers.teacher_id = dept_employs_teachers.teacher_id";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int officeLocationOrdinal = reader.GetOrdinal("office_location");
                        int nameOrdinal = reader.GetOrdinal("name");
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
                            CourseDAL courseGetter = new CourseDAL();
                            CourseCollection currTeacherCourses = courseGetter.GetCourseByTeacherID(teacherIDCheck);
                            Teacher currTeacher = new Teacher(officeLocation, name, email, publicEmail, phone, currTeacherCourses);

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