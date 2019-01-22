using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseManagement.App_Code;
using MySql.Data.MySqlClient;

namespace CourseManagement.DAL
{
    public class CourseRubricDAL
    {
        public Dictionary<string, int> GetCourseRubricByCRN(int CRNCheck)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            
            using (conn)
            {

                conn.Open();
                GradedItemDAL gradedStuff = new GradedItemDAL();

                var selectQuery = "SELECT * FROM rubrics WHERE rubrics.CRN = @CRNCheck";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@CRNCheck", CRNCheck);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {

                        int assignmentTypesOrdinal = reader.GetOrdinal("assignment_types");
                        int weightPerTypeOrdinal = reader.GetOrdinal("weight_per_type");
                        int rubricIDOrdinal = reader.GetOrdinal("rubric_id");

                        while (reader.Read())
                        {
                            
                            int rubricID = reader[rubricIDOrdinal] == DBNull.Value ? default(int) : reader.GetInt32(rubricIDOrdinal);
                            string assignmentTypes = reader[assignmentTypesOrdinal] == DBNull.Value ? default(string) : reader.GetString(assignmentTypesOrdinal);
                            string weightPerType = reader[weightPerTypeOrdinal] == DBNull.Value ? default(string) : reader.GetString(weightPerTypeOrdinal);
                            Dictionary<string, int> rubricStuff = new Dictionary<string, int>();
                            int assingmentCount = assignmentTypes.Split('/').Length - 1;
                            int weightCount = weightPerType.Split('/').Length - 1;
                            String[] types = new String[assingmentCount];
                            String[] weights = new String[weightCount];
                            if (assignmentTypes != default(string))
                            {
                                types = assignmentTypes.Split('/');
                            }
                            if (weightPerType != default(string))
                            {
                                weights = weightPerType.Split('/');
                            }
                            for (int i = 0; i < types.Length; i++)
                            {
                                rubricStuff.Add(types[i], Convert.ToInt32(weights[i]));
                            }
                            CourseRubric rubric = new CourseRubric(rubricStuff, rubricID);
                            return rubricStuff;

                        }
                    }
                    
                }
            }

            return null;
        }

        public void AddCourseRubric(int CRN, CourseRubric rubricToAdd)
        {
            string assignment_types = "";
            string weight_per_types = "";
            for (int i = 0; i < rubricToAdd.GradeTypeWithWeights.Count; i++)
            {
                if (i == rubricToAdd.GradeTypeWithWeights.Count - 1)
                {
                    assignment_types += rubricToAdd.GradeTypeWithWeights.ElementAt(i).Key;
                    weight_per_types += rubricToAdd.GradeTypeWithWeights.ElementAt(i).Value;
                }
                else
                {
                    assignment_types += rubricToAdd.GradeTypeWithWeights.ElementAt(i).Key + "/";
                    weight_per_types += rubricToAdd.GradeTypeWithWeights.ElementAt(i).Value + "/";
                }
            }
            MySqlConnection conn = DbConnection.GetConnection();
            using (conn)
            {
                conn.Open();
                var selectQuery =
                    "INSERT INTO rubrics(assignment_types, weight_per_type,CRN) VALUES (@assignment_types,@weight_per_type,@CRNToAdd)";
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@assignment_types", assignment_types);
                    cmd.Parameters.AddWithValue("@weight_per_type", weight_per_types);
                    cmd.Parameters.AddWithValue("@CRN", CRN);

                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        public void DelteCourseRubric(int CRN)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            using (conn)
            {
                conn.Open();
                var selectQuery =
                    "DELETE FROM rubrics WHERE CRN = @CRN";
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@CRN",CRN);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void UpdateCourseRubric(int CRN, CourseRubric rubricToUpdate)
        {
            string assignment_types = "";
            string weight_per_types = "";
            for (int i = 0; i < rubricToUpdate.GradeTypeWithWeights.Count; i++)
            {
                if (i == rubricToUpdate.GradeTypeWithWeights.Count - 1)
                {
                    assignment_types += rubricToUpdate.GradeTypeWithWeights.ElementAt(i).Key;
                    weight_per_types += rubricToUpdate.GradeTypeWithWeights.ElementAt(i).Value;
                }
                else
                {
                    assignment_types += rubricToUpdate.GradeTypeWithWeights.ElementAt(i).Key + "/";
                    weight_per_types += rubricToUpdate.GradeTypeWithWeights.ElementAt(i).Value + "/";
                }
            }
            MySqlConnection conn = DbConnection.GetConnection();
            using (conn)
            {
                conn.Open();
                var selectQuery =
                    "UPDATE rubrics SET assignment_types=@assignment_types, weight_per_type=@weight_per_type WHERE CRN = @CRN";
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@assignment_types", assignment_types);
                    cmd.Parameters.AddWithValue("@weight_per_type", weight_per_types);
                    cmd.Parameters.AddWithValue("@CRN", CRN);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
    }
}