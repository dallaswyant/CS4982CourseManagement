using System;
using System.Collections.Generic;
using CourseManagementLibrary.Model;
using MySql.Data.MySqlClient;

namespace CourseManagementLibrary.DAL
{
    /// <summary>
    /// This class defines a gradedItemDAL for interacting with gradedItems on the database
    /// </summary>
    public class GradedItemDAL
    {
        #region Methods



        /// <summary>
        /// Gets a list of graded items by CRN.
        /// </summary>
        /// <param name="CRNCheck">The CRN for the selected course</param>
        /// <returns>A list of graded items for the selected course</returns>
        public List<GradedItem> GetGradedItemsByCRN(int CRNCheck)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            var coursesTaught = new CourseCollection();
            var grades = new List<GradedItem>();
            using (conn)
            {
                conn.Open();
                var selectQuery =
                    "SELECT grade_items.* from grade_items, grade_belongs_to_courses WHERE grade_items.grade_item_id = grade_belongs_to_courses.grade_item_id AND grade_belongs_to_courses.courses_CRN = @CRN";
                var studentGetter = new StudentDAL();
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@CRN", CRNCheck);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int studentIdOrdinal = reader.GetOrdinal("student_uid");
                        int totalPointsOrdinal = reader.GetOrdinal("grade_total_points");
                        int gradeEarnedOrdinal = reader.GetOrdinal("grade_earned_points");
                        int gradeTypeOrdinal = reader.GetOrdinal("grade_type");
                        int gradeNameOrdinal = reader.GetOrdinal("grade_name");
                        int gradeFeedbackOrdinal = reader.GetOrdinal("grade_feedback");
                        int gradeItemIdOrdinal = reader.GetOrdinal("grade_item_id");
                        int isPublicOrdinal = reader.GetOrdinal("is_public");
                        int timeGradedOrdinal = reader.GetOrdinal("time_graded");

                        while (reader.Read())
                        {
                            var studentUID = reader[studentIdOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(studentIdOrdinal);
                            var totalPoints = reader[totalPointsOrdinal] == DBNull.Value
                                ? default(int)
                                : reader.GetInt32(totalPointsOrdinal);
                            var gradeEarned = reader[gradeEarnedOrdinal] == DBNull.Value
                                ? default(int)
                                : reader.GetInt32(gradeEarnedOrdinal);
                            var gradeType = reader[gradeTypeOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(gradeTypeOrdinal);
                            var gradeName = reader[gradeNameOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(gradeNameOrdinal);
                            var gradeFeedback = reader[gradeFeedbackOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(gradeFeedbackOrdinal);
                            var gradeItemId = reader[gradeItemIdOrdinal] == DBNull.Value
                                ? default(int)
                                : reader.GetInt32(gradeItemIdOrdinal);
                            var isPublic = reader[isPublicOrdinal] != DBNull.Value && reader.GetBoolean(isPublicOrdinal);
                            DateTime? timeGraded = reader[gradeFeedbackOrdinal] == DBNull.Value ? default(DateTime) : reader.GetDateTime(timeGradedOrdinal);
                            if (timeGraded == DateTime.MinValue)
                            {
                                timeGraded = null;
                            }
                            var currStudent = studentGetter.GetStudentByStudentID(studentUID);
                            
                            var currGradedItem = new GradedItem(gradeName, currStudent, gradeEarned, gradeFeedback, totalPoints,
                                gradeType,gradeItemId, isPublic, timeGraded);
                            grades.Add(currGradedItem);
                        }

                        return grades;
                    }
                }
            }
        }

        /// <summary>
        /// Gets unique graded items by CRN.
        /// </summary>
        /// <param name="CRNCheck">The CRN check.</param>
        /// <returns>A dictionary of unique graded items for the selected course</returns>
        public Dictionary<string, string> GetUniqueGradedItemsByCRN(int CRNCheck)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            var grades = new Dictionary<string, string>();
            using (conn)
            {
                conn.Open();
                var selectQuery =
                    "SELECT DISTINCT grade_items.grade_name From grade_items, grade_belongs_to_courses WHERE grade_belongs_to_courses.courses_CRN = @CRN";
                var studentGetter = new StudentDAL();
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@CRN", CRNCheck);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int gradeNameOrdinal = reader.GetOrdinal("grade_name");

                        while (reader.Read())
                        {
                           
                            var gradeName = reader[gradeNameOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(gradeNameOrdinal);
                            
                            grades.Add(gradeName, gradeName);
                        }

                        return grades;
                    }
                }
            }
        }

        /// <summary>
        /// Gets a list of graded items by student id.
        /// </summary>
        /// <param name="studentUID">The student uid.</param>
        /// <param name="CRNCheck">The CRN check.</param>
        /// <returns>A list of graded items that belong to the selected student</returns>
        public List<GradedItem> GetGradedItemsByStudentId(string studentUID, int CRNCheck)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            var coursesTaught = new CourseCollection();
            var grades = new List<GradedItem>();
            using (conn)
            {
                conn.Open();
                var selectQuery =
                    "SELECT grade_items.* FROM grade_items, grade_belongs_to_courses WHERE grade_items.grade_item_id = grade_belongs_to_courses.grade_item_id AND grade_belongs_to_courses.courses_CRN = @CRN AND student_uid = @studentId";
                var studentGetter = new StudentDAL();
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@CRN", CRNCheck);
                    cmd.Parameters.AddWithValue("@studentId", studentUID);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int studentUIDOrdinal = reader.GetOrdinal("student_uid");
                        int totalPointsOrdinal = reader.GetOrdinal("grade_total_points");
                        int gradeEarnedOrdinal = reader.GetOrdinal("grade_earned_points");
                        int gradeTypeOrdinal = reader.GetOrdinal("grade_type");
                        int gradeNameOrdinal = reader.GetOrdinal("grade_name");
                        int gradeFeedbackOrdinal = reader.GetOrdinal("grade_feedback");
                        int gradeItemIdOrdinal = reader.GetOrdinal("grade_item_id");
                        int isPublicOrdinal = reader.GetOrdinal("is_public");
                        int timeGradedOrdinal = reader.GetOrdinal("time_graded");

                        while (reader.Read())
                        {
                            var studentUid = reader[studentUIDOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(studentUIDOrdinal);
                            var totalPoints = reader[totalPointsOrdinal] == DBNull.Value
                                ? default(int)
                                : reader.GetInt32(totalPointsOrdinal);
                            var gradeEarned = reader[gradeEarnedOrdinal] == DBNull.Value
                                ? default(int)
                                : reader.GetInt32(gradeEarnedOrdinal);
                            var gradeType = reader[gradeTypeOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(gradeTypeOrdinal);
                            var gradeName = reader[gradeNameOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(gradeNameOrdinal);
                            var gradeFeedback = reader[gradeFeedbackOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(gradeFeedbackOrdinal);
                            var gradeItemId = reader[gradeItemIdOrdinal] == DBNull.Value
                                ? default(int)
                                : reader.GetInt32(gradeItemIdOrdinal);
                            var isPublic = reader[isPublicOrdinal] != DBNull.Value && reader.GetBoolean(isPublicOrdinal);
                            DateTime? timeGraded = reader[gradeFeedbackOrdinal] == DBNull.Value ? default(DateTime) : reader.GetDateTime(timeGradedOrdinal);
                            if (timeGraded == DateTime.MinValue)
                            {
                                timeGraded = null;
                            }

                            var currStudent = studentGetter.GetStudentByStudentID(studentUid);
                            
                            var currGradedItem = new GradedItem(gradeName, currStudent, gradeEarned, gradeFeedback, totalPoints,
                                gradeType,gradeItemId, isPublic, timeGraded);
                            grades.Add(currGradedItem);
                        }

                        return grades;
                    }
                }
            }
        }

        /// <summary>
        /// Grades the graded item by CRN and student uid.
        /// </summary>
        /// <param name="newItem">The new item.</param>
        /// <param name="CRN">The CRN.</param>
        /// <param name="studentUID">The student uid.</param>
        public void gradeGradedItemByCRNAndStudentUID(GradedItem newItem, int CRN, string studentUID)
        {
            StudentDAL studentGetter = new StudentDAL();
            Student currStudent = studentGetter.GetStudentByStudentID(studentUID);

            MySqlConnection conn = DbConnection.GetConnection();

            using (conn)
            {
                conn.Open();
                    GradedItem grade = new GradedItem(newItem.Name, currStudent, newItem.Grade, newItem.Feedback, newItem.PossiblePoints, newItem.GradeType, 0, newItem.IsPublic, newItem.TimeGraded);
                    var selectQuery =
                        "UPDATE grade_items SET grade_earned_points=@grade_points, grade_feedback=@grade_feedback, time_graded=@time_graded WHERE student_uid = @studentUID AND grade_name = @grade_name";
                    using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@studentUID", studentUID);
                        cmd.Parameters.AddWithValue("@grade_points", grade.Grade);
                        cmd.Parameters.AddWithValue("@grade_name", grade.Name);
                        cmd.Parameters.AddWithValue("@grade_feedback", grade.Feedback);
                        cmd.Parameters.AddWithValue("@time_graded", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();
            
        }


        /// <summary>
        /// Deletes the graded item by CRN for all students.
        /// </summary>
        /// <param name="gradedItem">The graded item.</param>
        /// <param name="CRN">The CRN.</param>
        public void deleteGradedItemByCRNForAllStudents(GradedItem gradedItem, int CRN)
        {
            StudentDAL studentGetter = new StudentDAL();
            List<Student> students = studentGetter.GetStudentsByCRN(CRN);


            MySqlConnection conn = DbConnection.GetConnection();

            using (conn)
            {
                conn.Open();
                foreach (var t in students)
                {
                    var selectQuery =
                        "DELETE grade_items FROM grade_items INNER JOIN grade_belongs_to_courses ON grade_items.grade_item_id = grade_belongs_to_courses.grade_item_id WHERE grade_items.student_uid = @studentUID AND grade_belongs_to_courses.courses_CRN = @CRNCheck AND grade_items.grade_name = @grade_name";

                    using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@studentUID", t.StudentUID);
                        cmd.Parameters.AddWithValue("@grade_name", gradedItem.Name);
                        cmd.Parameters.AddWithValue("@CRNCheck", CRN);
                        cmd.ExecuteNonQuery();
                    }

                    var query =
                        "DELETE grade_belongs_to_courses FROM grade_belongs_to_courses WHERE grade_belongs_to_courses.grade_item_id = (SELECT grade_items.grade_item_id FROM grade_items WHERE grade_items.student_uid = @studentUID AND grade_items.grade_name = @gradeName) AND grade_belongs_to_courses.courses_CRN = @CRN";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@studentUID", t.StudentUID);
                        cmd.Parameters.AddWithValue("@gradeName", gradedItem.Name);
                        cmd.Parameters.AddWithValue("@CRN", CRN);
                        cmd.ExecuteNonQuery();
                    }
                }

                conn.Close();
            }
        }

        /// <summary>
        /// Inserts the new graded item by CRN for all students.
        /// </summary>
        /// <param name="newItem">The new item.</param>
        /// <param name="CRN">The CRN.</param>
        public void InsertNewGradedItemByCRNForAllStudents(GradedItem newItem, int CRN)
        {
            StudentDAL studentGetter = new StudentDAL();
            List<Student> students = studentGetter.GetStudentsByCRN(CRN);
            
                
                MySqlConnection conn = DbConnection.GetConnection();

                using (conn)
                {
                    conn.Open();
                    foreach (var t in students)
                    {
                        GradedItem grade = new GradedItem(newItem.Name, t, 0.0, null, newItem.PossiblePoints, newItem.GradeType, 0, newItem.IsPublic, newItem.TimeGraded);
                        var selectQuery =
                            "INSERT INTO grade_items(student_uid, grade_total_points, grade_earned_points, grade_type, grade_name, grade_feedback, is_public) VALUES (@studentUID,@grade_total,@grade_points,@grade_type,@grade_name,@grade_feedback,@is_public)";
                        using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@studentUID", grade.Student.StudentUID);
                            cmd.Parameters.AddWithValue("@grade_total", grade.PossiblePoints);
                            cmd.Parameters.AddWithValue("@grade_points", grade.Grade);
                            cmd.Parameters.AddWithValue("@grade_type", grade.GradeType);
                            cmd.Parameters.AddWithValue("@grade_name", grade.Name);
                            cmd.Parameters.AddWithValue("@grade_feedback", grade.Feedback);
                            cmd.Parameters.AddWithValue("@is_public", grade.IsPublic);
                            cmd.ExecuteNonQuery();
                        }

                        var query =
                            "INSERT INTO grade_belongs_to_courses (grade_item_id, courses_CRN) VALUES ((SELECT grade_items.grade_item_id FROM grade_items WHERE grade_items.student_uid = @studentUID AND grade_items.grade_name = @grade_name),@CRN)";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@studentUID", grade.Student.StudentUID);
                            cmd.Parameters.AddWithValue("@grade_name", grade.Name);
                            cmd.Parameters.AddWithValue("@CRN", CRN);
                            cmd.ExecuteNonQuery();
                        }
                }
                    conn.Close();
                }
            
        }

        /// <summary>
        /// Updates the grade item by CRN and old name for all students.
        /// </summary>
        /// <param name="newItem">The new item.</param>
        /// <param name="CRN">The CRN.</param>
        /// <param name="oldgradename">The oldgradename.</param>
        public void UpdateGradeItemByCRNAndOldNameForAllStudents(GradedItem newItem,int CRN, string oldgradename)
        {
            StudentDAL studentGetter = new StudentDAL();
            List<Student> students = studentGetter.GetStudentsByCRN(CRN);


            MySqlConnection conn = DbConnection.GetConnection();

            using (conn)
            {
                conn.Open();
                foreach (var t in students)
                {
                    GradedItem grade = new GradedItem(newItem.Name, t, 0.0, null, newItem.PossiblePoints, newItem.GradeType, 0, newItem.IsPublic, newItem.TimeGraded);
                    var selectQuery =
                        "UPDATE grade_items SET grade_total_points=@grade_total, grade_type=@grade_type, grade_name=@grade_newname, is_public=@is_public WHERE student_uid = @studentUID AND grade_name = @grade_oldname";
                    using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@studentUID", grade.Student.StudentUID);
                        cmd.Parameters.AddWithValue("@grade_total", grade.PossiblePoints);
                        cmd.Parameters.AddWithValue("@grade_type", grade.GradeType);
                        cmd.Parameters.AddWithValue("@is_public", grade.IsPublic);
                        cmd.Parameters.AddWithValue("@grade_newname", grade.Name);
                        cmd.Parameters.AddWithValue("@grade_oldname", oldgradename);
                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
        }

        /// <summary>
        /// Gets the graded items by CRN and grade name for all students.
        /// </summary>
        /// <param name="CRNCheck">The CRN check.</param>
        /// <param name="gradeName">Name of the grade.</param>
        /// <returns>A list of graded Items by crn and name of the grade</returns>
        public List<GradedItem> GetGradedItemsByCRNAndGradeNameForAllStudents(int CRNCheck, string gradeName)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            var grades = new List<GradedItem>();
            using (conn)
            {
                conn.Open();
                var selectQuery =
                    "SELECT * FROM grade_items,grade_belongs_to_courses WHERE grade_items.grade_item_id = grade_belongs_to_courses.grade_item_id AND grade_belongs_to_courses.courses_CRN = @CRNCheck AND grade_name = @grade_name";
                var studentGetter = new StudentDAL();
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@CRNCheck", CRNCheck);
                    cmd.Parameters.AddWithValue("@grade_name", gradeName);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int studentIdOrdinal = reader.GetOrdinal("student_uid");
                        int totalPointsOrdinal = reader.GetOrdinal("grade_total_points");
                        int gradeEarnedOrdinal = reader.GetOrdinal("grade_earned_points");
                        int gradeTypeOrdinal = reader.GetOrdinal("grade_type");
                        int gradeFeedbackOrdinal = reader.GetOrdinal("grade_feedback");
                        int gradeItemIdOrdinal = reader.GetOrdinal("grade_item_id");
                        int isPublicOrdinal = reader.GetOrdinal("is_public");
                        int timeGradedOrdinal = reader.GetOrdinal("time_graded");

                        while (reader.Read())
                        {
                            var studentUID = reader[studentIdOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(studentIdOrdinal);
                            var totalPoints = reader[totalPointsOrdinal] == DBNull.Value
                                ? default(int)
                                : reader.GetInt32(totalPointsOrdinal);
                            var gradeEarned = reader[gradeEarnedOrdinal] == DBNull.Value
                                ? default(int)
                                : reader.GetInt32(gradeEarnedOrdinal);
                            var gradeType = reader[gradeTypeOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(gradeTypeOrdinal);
                            var gradeFeedback = reader[gradeFeedbackOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(gradeFeedbackOrdinal);
                            var gradeItemId = reader[gradeItemIdOrdinal] == DBNull.Value
                                ? default(int)
                                : reader.GetInt32(gradeItemIdOrdinal);
                            var isPublic = reader[isPublicOrdinal] != DBNull.Value && reader.GetBoolean(isPublicOrdinal);
                            DateTime? timeGraded = reader[gradeFeedbackOrdinal] == DBNull.Value ? default(DateTime) : reader.GetDateTime(timeGradedOrdinal);
                            if (timeGraded == DateTime.MinValue)
                            {
                                timeGraded = null;
                            }
                            var currStudent = studentGetter.GetStudentByStudentID(studentUID);

                            var currGradedItem = new GradedItem(gradeName, currStudent, gradeEarned, gradeFeedback, totalPoints,
                                gradeType, gradeItemId, isPublic, timeGraded);
                            grades.Add(currGradedItem);
                        }

                        return grades;
                    }
                }
            }
        }

        #endregion


    }
}