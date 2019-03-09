using System;
using System.Collections.Generic;
using CourseManagement.Models;
using MySql.Data.MySqlClient;

namespace CourseManagement.DAL
{
    /// <summary>
    /// Defines a teacherDAL object for interacting with teachers on the database
    /// </summary>
    public class TeacherDAL
    {
        #region Methods

        /// <summary>
        /// Gets the teacher by teacher id.
        /// </summary>
        /// <param name="teacherUIDCheck">The teacher uid to check.</param>
        /// <returns>A teacher with the given teacher UID</returns>
        public Teacher GetTeacherByTeacherID(string teacherUIDCheck)
        {
            MySqlConnection dbConnection = DbConnection.GetConnection();

            using (dbConnection)
            {
                dbConnection.Open();
                var selectQuery =
                    "SELECT teachers.*, dept_employs_teachers.dept_name FROM teachers, dept_employs_teachers WHERE teachers.uid = dept_employs_teachers.teacher_uid AND teachers.uid = @teacherUID";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@teacherUID", teacherUIDCheck);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int teacherUIDOrdinal = reader.GetOrdinal("uid");
                        int officeLocationOrdinal = reader.GetOrdinal("office_location");
                        int emailOrdinal = reader.GetOrdinal("email");
                        int publicEmailOrdinal = reader.GetOrdinal("public_email");
                        int phoneOrdinal = reader.GetOrdinal("phone_number");

                        while (reader.Read())
                        {
                            var officeLocation = reader[officeLocationOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(officeLocationOrdinal);
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
                            Teacher currTeacher = new Teacher(officeLocation, email, publicEmail, phone, currTeacherCourses,teacherUID);

                            return currTeacher;
                            
                        }
                    }
                }

                dbConnection.Close();
            }

            return null;
        }


                /// <summary>
        /// Gets the teacher by teacher id.
        /// </summary>
        /// <param name="teacherUIDCheck">The teacher uid to check.</param>
        /// <returns>A teacher with the given teacher UID</returns>
        public Teacher GetTeacherByCRN(int crn)
        {
            MySqlConnection dbConnection = DbConnection.GetConnection();

            using (dbConnection)
            {
                dbConnection.Open();
             
                var selectQuery =
                    "SELECT teachers.* FROM teachers, teacher_teaches_courses WHERE teacher_teaches_courses.courses_CRN = @CRN";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@CRN", crn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int teacherUIDOrdinal = reader.GetOrdinal("uid");
                        int officeLocationOrdinal = reader.GetOrdinal("office_location");
                        int emailOrdinal = reader.GetOrdinal("email");
                        int publicEmailOrdinal = reader.GetOrdinal("public_email");
                        int phoneOrdinal = reader.GetOrdinal("phone_number");

                        while (reader.Read())
                        {
                            var officeLocation = reader[officeLocationOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(officeLocationOrdinal);
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
                            Teacher currTeacher = new Teacher(officeLocation, email, publicEmail, phone, currTeacherCourses,teacherUID);

                            return currTeacher;
                            
                        }
                    }
                }

                dbConnection.Close();
            }

            return null;
        }


        public void UpdateFinalGradeByCRNAndStudentID(int CRNCheck, string studentID, Char grade)
        {
            
            MySqlConnection dbConnection = DbConnection.GetConnection();

            using (dbConnection)
            {
                dbConnection.Open();

                var updateQuery =
                    "UPDATE student_has_courses SET student_has_courses.grade_earned = @grade WHERE student_has_courses.student_uid = @studentID AND student_has_courses.courses_CRN = @CRNCheck";

                using (MySqlCommand cmd = new MySqlCommand(updateQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@CRNCheck", CRNCheck);
                    cmd.Parameters.AddWithValue("@studentID", studentID);
                    cmd.Parameters.AddWithValue("@grade", grade);
                    cmd.ExecuteNonQuery();
                }
                

                dbConnection.Close();
            }

        }
        #endregion
    }
}