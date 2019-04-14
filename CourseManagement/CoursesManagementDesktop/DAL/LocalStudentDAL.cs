using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Reflection.Emit;
using CourseManagement.Models;
using CoursesManagementDesktop.DAL;

namespace CourseManagementDesktop.DAL
{
    public class LocalStudentDAL
    {
        #region Methods


        /// <summary>
        /// Gets the student by student id.
        /// </summary>
        /// <param name="studentUIDCheck">The student uid to check.</param>
        /// <returns>A student with the selected studentUID</returns>
        public Student GetStudentByStudentID(string studentUIDCheck)
        {
            SQLiteConnection dbConnection = LocalDbConnection.GetConnection();
            using (dbConnection)
            {
                dbConnection.Open();
                var selectQuery = "SELECT * from students WHERE students.uid = @studentUID";

                using (SQLiteCommand cmd = new SQLiteCommand(selectQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@studentUID", studentUIDCheck);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        int studentUIDOrdinal = reader.GetOrdinal("uid");
                        int emailOrdinal = reader.GetOrdinal("email");


                        while (reader.Read())
                        {
                            var studentUID = reader[studentUIDOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(studentUIDOrdinal);
                            var email = reader[emailOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(emailOrdinal);


                            var newStudent = new Student(studentUID, email);
                            return newStudent;
                        }
                    }
                }

                dbConnection.Close();
            }

            return null;
        }

        /// <summary>
        /// Gets a list of students in the course with the given CRN.
        /// </summary>
        /// <param name="CRNCheck">The CRN to check.</param>
        /// <returns>A list of students in the selected course</returns>
        public List<Student> GetStudentsByCRN(int CRNCheck)
        {
            SQLiteConnection dbConnection = LocalDbConnection.GetConnection();
            List<Student> studentsInCurrentClasses = new List<Student>();
            using (dbConnection)
            {
                dbConnection.Open();
                var selectQuery =
                    "select students.* from students, student_has_courses WHERE students.uid=student_has_courses.student_uid AND student_has_courses.courses_CRN=@CRNCheck";

                using (SQLiteCommand cmd = new SQLiteCommand(selectQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@CRNCheck", CRNCheck);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        int studentUIDOrdinal = reader.GetOrdinal("uid");
                        int emailOrdinal = reader.GetOrdinal("email");


                        while (reader.Read())
                        {
                            var studentUID = reader[studentUIDOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(studentUIDOrdinal);
                            var email = reader[emailOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(emailOrdinal);

                            var newStudent = new Student(studentUID, email);
                            studentsInCurrentClasses.Add(newStudent);
                        }

                        return studentsInCurrentClasses;
                    }
                }
            }
        }
        
        #endregion

    }
}