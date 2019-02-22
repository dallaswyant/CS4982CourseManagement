using System;
using System.Collections.Generic;
using CourseManagement.App_Code;
using MySql.Data.MySqlClient;

namespace CourseManagement.DAL
{
    /// <summary>
    /// This class defines a gradedItemDAL for interacting with gradedItems on the database
    /// </summary>
    public class GradeItemDAL
    {
        #region Methods



        /// <summary>
        /// Gets a list of graded items by CRN.
        /// </summary>
        /// <param name="CRNCheck">The CRN for the selected course</param>
        /// <returns>A list of graded items for the selected course</returns>
        public List<GradeItem> GetGradedItemsByCRN(int CRNCheck)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            var coursesTaught = new CourseCollection();
            var grades = new List<GradeItem>();
            using (conn)
            {
                conn.Open();
                var selectQuery =
                    "SELECT grade_defs.* from grade_defs WHERE grade_defs.course_CRN = @CRNCheck";
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
                        int gradeDescriptionOrdinal = reader.GetOrdinal("grade_description");
                        int gradeItemIdOrdinal = reader.GetOrdinal("grade_def_id");
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
                            var gradeDescription = reader[gradeDescriptionOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(gradeDescriptionOrdinal);
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
                            //fix this one
                            var currGradedItem = new GradeItem(gradeName, currStudent, gradeEarned, gradeFeedback, totalPoints,
                                gradeType,gradeDescription,gradeItemId, isPublic, timeGraded);
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
                    "SELECT grade_defs.* from grade_defs WHERE grade_defs.course_CRN = @CRNCheck";
                var studentGetter = new StudentDAL();
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@CRNCheck", CRNCheck);
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
        public List<GradeItem> GetGradedItemsByStudentId(string studentUIDCheck, int CRNCheck)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            var coursesTaught = new CourseCollection();
            var grades = new List<GradeItem>();
            using (conn)
            {
                conn.Open();
                var selectQuery =
                    "SELECT grade_defs.*, student_grade_items.* from grade_defs, student_grade_items WHERE student_grade_items.student_uid = @studentUIDCheck AND grade_defs.grade_def_id = student_grade_items.grade_def_id AND grade_defs.course_CRN = @CRNCheck";
                var studentGetter = new StudentDAL();
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@CRNCheck", CRNCheck);
                    cmd.Parameters.AddWithValue("@studentUIDCheck", studentUIDCheck);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int studentUIDOrdinal = reader.GetOrdinal("student_uid");
                        int totalPointsOrdinal = reader.GetOrdinal("grade_total_points");
                        int gradeEarnedOrdinal = reader.GetOrdinal("grade_earned_points");
                        int gradeTypeOrdinal = reader.GetOrdinal("grade_type");
                        int gradeNameOrdinal = reader.GetOrdinal("grade_name");
                        int gradeFeedbackOrdinal = reader.GetOrdinal("grade_feedback");
                        int gradeItemIdOrdinal = reader.GetOrdinal("grade_def_id");
                        int isPublicOrdinal = reader.GetOrdinal("is_public");
                        int timeGradedOrdinal = reader.GetOrdinal("time_graded");
                        int gradeDescriptionOrdinal = reader.GetOrdinal("grade_description");

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
                            var gradeDescription = reader[gradeDescriptionOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(gradeDescriptionOrdinal);
                            var isPublic = reader[isPublicOrdinal] != DBNull.Value && reader.GetBoolean(isPublicOrdinal);
                            DateTime? timeGraded = reader[gradeFeedbackOrdinal] == DBNull.Value ? default(DateTime) : reader.GetDateTime(timeGradedOrdinal);
                            if (timeGraded == DateTime.MinValue)
                            {
                                timeGraded = null;
                            }

                            var currStudent = studentGetter.GetStudentByStudentID(studentUid);
                            
                            var currGradedItem = new GradeItem(gradeName, currStudent, gradeEarned, gradeFeedback, totalPoints,
                                gradeType,gradeDescription,gradeItemId, isPublic, timeGraded);
                            grades.Add(currGradedItem);
                        }

                        return grades;
                    }
                }
            }
        }

        public List<GradeItem> GetPublicGradedItemsByStudentId(string studentUIDCheck, int CRNCheck)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            var coursesTaught = new CourseCollection();
            var grades = new List<GradeItem>();
            using (conn)
            {
                conn.Open();
                var selectQuery =
                    "SELECT grade_defs.*, student_grade_items.* from grade_defs, student_grade_items WHERE student_grade_items.student_uid = @studentUIDCheck AND grade_defs.grade_def_id = student_grade_items.grade_def_id AND grade_defs.course_CRN = @CRNCheck AND grade_defs.is_public = 1";
                var studentGetter = new StudentDAL();
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@CRNCheck", CRNCheck);
                    cmd.Parameters.AddWithValue("@studentUIDCheck", studentUIDCheck);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int studentUIDOrdinal = reader.GetOrdinal("student_uid");
                        int totalPointsOrdinal = reader.GetOrdinal("grade_total_points");
                        int gradeEarnedOrdinal = reader.GetOrdinal("grade_earned_points");
                        int gradeTypeOrdinal = reader.GetOrdinal("grade_type");
                        int gradeNameOrdinal = reader.GetOrdinal("grade_name");
                        int gradeFeedbackOrdinal = reader.GetOrdinal("grade_feedback");
                        int gradeItemIdOrdinal = reader.GetOrdinal("grade_def_id");
                        int isPublicOrdinal = reader.GetOrdinal("is_public");
                        int timeGradedOrdinal = reader.GetOrdinal("time_graded");
                        int gradeDescriptionOrdinal = reader.GetOrdinal("grade_description");

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
                            var gradeDescription = reader[gradeDescriptionOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(gradeDescriptionOrdinal);
                            var isPublic = reader[isPublicOrdinal] != DBNull.Value && reader.GetBoolean(isPublicOrdinal);
                            DateTime? timeGraded = reader[gradeFeedbackOrdinal] == DBNull.Value ? default(DateTime) : reader.GetDateTime(timeGradedOrdinal);
                            if (timeGraded == DateTime.MinValue)
                            {
                                timeGraded = null;
                            }

                            var currStudent = studentGetter.GetStudentByStudentID(studentUid);

                            var currGradedItem = new GradeItem(gradeName, currStudent, gradeEarned, gradeFeedback, totalPoints,
                                gradeType, gradeDescription, gradeItemId, isPublic, timeGraded);

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
        public void gradeGradedItemByCRNAndStudentUID(GradeItem newItem, int CRN, string studentUID)
        {
            StudentDAL studentGetter = new StudentDAL();
            Student currStudent = studentGetter.GetStudentByStudentID(studentUID);

            MySqlConnection conn = DbConnection.GetConnection();

            using (conn)
            {
                conn.Open();
                    GradeItem grade = new GradeItem(newItem.Name, currStudent, newItem.Grade, newItem.Feedback, newItem.PossiblePoints, newItem.GradeType, newItem.Description, 0, newItem.IsPublic, newItem.TimeGraded);
                    var selectQuery =
                        "UPDATE student_grade_items SET grade_earned_points=@grade_earned_points, grade_feedback=@grade_feedback, time_graded=@time_graded WHERE student_uid = @studentUID AND student_grade_items.grade_def_id = (SELECT grade_defs.grade_def_id FROM grade_defs WHERE grade_defs.grade_name = @grade_name AND grade_defs.course_CRN = @CRNCheck)";
                    using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@studentUID", studentUID);
                        cmd.Parameters.AddWithValue("@grade_earned_points", grade.Grade);
                        cmd.Parameters.AddWithValue("@grade_name", grade.Name);
                        cmd.Parameters.AddWithValue("@grade_feedback", grade.Feedback);
                        cmd.Parameters.AddWithValue("@time_graded", DateTime.Now);
                        cmd.Parameters.AddWithValue("@CRNCheck", CRN);
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
        public void deleteGradedItemByCRNForAllStudents(GradeItem gradedItem, int CRN)
        {
            MySqlConnection conn = DbConnection.GetConnection();

            using (conn)
            {
                conn.Open();
                    var selectQuery =
                        "DELETE grade_defs FROM grade_defs WHERE grade_defs.course_CRN = @CRNCheck AND grade_defs.grade_name = @grade_name";

                    using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@grade_name", gradedItem.Name);
                        cmd.Parameters.AddWithValue("@CRNCheck", CRN);
                        cmd.ExecuteNonQuery();
                    }

                conn.Close();
            }
        }

        /// <summary>
        /// Inserts the new graded item by CRN for all students.
        /// </summary>
        /// <param name="newItem">The new item.</param>
        /// <param name="CRN">The CRN.</param>
        public void InsertNewGradedItemByCRNForAllStudents(GradeItem newItem, int CRN)
        {
            StudentDAL studentGetter = new StudentDAL();
            List<Student> students = studentGetter.GetStudentsByCRN(CRN);
            
                
                MySqlConnection conn = DbConnection.GetConnection();

                using (conn)
                {
                    conn.Open();
                    //GradeItem grade = new GradeItem(newItem.Name, t, 0.0, null, newItem.PossiblePoints, newItem.GradeType, 0, newItem.IsPublic, newItem.TimeGraded);
                    var selectQuery =
                        "INSERT INTO grade_defs(grade_total_points, grade_description, grade_type, grade_name, is_public, course_CRN) VALUES (@grade_total_points,@grade_description,@grade_type,@grade_name,@is_public,@course_CRN)";
                    using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@grade_total_points", newItem.PossiblePoints);
                        cmd.Parameters.AddWithValue("@grade_description", newItem.Description);
                        cmd.Parameters.AddWithValue("@grade_type", newItem.GradeType);
                        cmd.Parameters.AddWithValue("@grade_name", newItem.Name);
                        cmd.Parameters.AddWithValue("@grade_feedback", newItem.Feedback);
                        cmd.Parameters.AddWithValue("@is_public", newItem.IsPublic);
                        cmd.Parameters.AddWithValue("@course_CRN", CRN);
                        cmd.ExecuteNonQuery();
                    }
                foreach (var t in students)
                    {

                    GradeItem grade = new GradeItem(newItem.Name, t, 0.0, null, newItem.PossiblePoints, newItem.GradeType, null, 0, newItem.IsPublic, newItem.TimeGraded);
                    var query =
                            "INSERT INTO student_grade_items (grade_def_id, student_uid, grade_earned_points,grade_feedback,time_graded) VALUES ((SELECT grade_defs.grade_def_id FROM grade_defs WHERE grade_defs.grade_name = @grade_name AND grade_defs.course_CRN = @CRNCheck),@studentUID,@grade_earned_points,@grade_feedback,@time_graded)";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@studentUID", grade.Student.StudentUID);
                            cmd.Parameters.AddWithValue("@grade_name", grade.Name);
                            cmd.Parameters.AddWithValue("@CRNCheck", CRN);
                            cmd.Parameters.AddWithValue("@grade_earned_points", 0);
                            cmd.Parameters.AddWithValue("@grade_feedback",null);
                            cmd.Parameters.AddWithValue("@time_graded", null);
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
        public void UpdateGradeItemByCRNAndOldNameForAllStudents(GradeItem newItem,int CRN, string oldgradename)
        {
            

            MySqlConnection conn = DbConnection.GetConnection();

            using (conn)
            {
                conn.Open();
                
                    var selectQuery =
                        "UPDATE grade_defs SET grade_total_points=@grade_total, grade_type=@grade_type, grade_description = @grade_description grade_name=@grade_newname, is_public=@is_public WHERE grade_name = @grade_oldname AND course_CRN = @CRNCheck";
                    using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@grade_total", newItem.PossiblePoints);
                        cmd.Parameters.AddWithValue("@grade_type", newItem.GradeType);
                        cmd.Parameters.AddWithValue("@is_public", newItem.IsPublic);
                        cmd.Parameters.AddWithValue("@grade_newname", newItem.Name);
                        cmd.Parameters.AddWithValue("@grade_description", newItem.Description);
                        cmd.Parameters.AddWithValue("@grade_oldname", oldgradename);
                        cmd.ExecuteNonQuery();
                    }
                
                conn.Close();
            }
        }
        public void PublishGradeItemByNameAndCRNForAllStudents(int CRN, string name, bool isPublic)
        {
            MySqlConnection conn = DbConnection.GetConnection();

            using (conn)
            {
                conn.Open();

                    var selectQuery =
                        "UPDATE grade_defs SET is_public=@is_public WHERE grade_name = @grade_name AND course_CRN = @CRNCheck";
                    using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@CRNCheck", CRN);
                        cmd.Parameters.AddWithValue("@is_public", isPublic);
                        cmd.Parameters.AddWithValue("@grade_name", name);
                        cmd.ExecuteNonQuery();
                    }
                
             }
        }

        public bool getPublicStatusByCRNandGradeName(int CRN, string name)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            var grades = new Dictionary<string, string>();
            using (conn)
            {
                conn.Open();
                var selectQuery =
                    "SELECT grade_defs.is_public From grade_defs WHERE grade_defs.course_CRN = @CRNCheck AND grade_defs.grade_name = @grade_name";
                var studentGetter = new StudentDAL();
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@CRNCheck", CRN);
                    cmd.Parameters.AddWithValue("@grade_name", name);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int publicOrdinal = reader.GetOrdinal("is_public");

                        while (reader.Read())
                        {

                           
                            var isPublic = reader[publicOrdinal] != DBNull.Value && reader.GetBoolean(publicOrdinal);

                            return isPublic;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the graded items by CRN and grade name for all students.
        /// </summary>
        /// <param name="CRNCheck">The CRN check.</param>
        /// <param name="gradeName">Name of the grade.</param>
        /// <returns>A list of graded Items by crn and name of the grade</returns>
        public List<GradeItem> GetGradedItemsByCRNAndGradeNameForAllStudents(int CRNCheck, string gradeName)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            var grades = new List<GradeItem>();
            using (conn)
            {
                conn.Open();
                var selectQuery =
                    "SELECT * FROM grade_defs,student_grade_items WHERE grade_defs.course_CRN = @CRNCheck AND grade_name = @grade_name";
                var studentGetter = new StudentDAL();
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@CRNCheck", CRNCheck);
                    cmd.Parameters.AddWithValue("@grade_name", gradeName);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int studentIdOrdinal = reader.GetOrdinal("student_uid");
                        int gradeDescriptionOrdinal = reader.GetOrdinal("grade_description");
                        int totalPointsOrdinal = reader.GetOrdinal("grade_total_points");
                        int gradeEarnedOrdinal = reader.GetOrdinal("grade_earned_points");
                        int gradeTypeOrdinal = reader.GetOrdinal("grade_type");
                        int gradeFeedbackOrdinal = reader.GetOrdinal("grade_feedback");
                        int gradeItemIdOrdinal = reader.GetOrdinal("grade_def_id");
                        int isPublicOrdinal = reader.GetOrdinal("is_public");
                        int timeGradedOrdinal = reader.GetOrdinal("time_graded");

                        while (reader.Read())
                        {
                            var studentUID = reader[studentIdOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(studentIdOrdinal);
                            var gradeDescription = reader[gradeDescriptionOrdinal] == DBNull.Value
                                ? default(string)
                                : reader.GetString(gradeDescriptionOrdinal);
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

                            var currGradedItem = new GradeItem(gradeName, currStudent, gradeEarned, gradeFeedback, totalPoints,
                                gradeType, gradeDescription, gradeItemId, isPublic, timeGraded);
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