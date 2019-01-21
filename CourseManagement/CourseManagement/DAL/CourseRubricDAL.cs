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
        public CourseRubric GetCourseRubricByCRN(int CRNCheck)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            
            using (conn)
            {

                conn.Open();
                GradedItemDAL gradedStuff = new GradedItemDAL();

                var selectQuery = "SELECT * FROM rubrics, courses WHERE rubrics.rubric_id = courses.rubric_id AND courses.CRN = @CRNCheck";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@CRNCheck", CRNCheck);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {

                        int CRNOrdinal = reader.GetOrdinal("CRN");
                        int assignmentTypesOrdinal = reader.GetOrdinal("assignment_types");
                        int weightPerTypeOrdinal = reader.GetOrdinal("weight_per_type");
                        int rubricIDOrdinal = reader.GetOrdinal("rubric_id");

                        while (reader.Read())
                        {
                            int CRN = reader[CRNOrdinal] == DBNull.Value ? default(int) : reader.GetInt32(CRNOrdinal);
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
                            return rubric;

                        }
                    }
                    
                }
            }

            return null;
        }

        public void AddCourseRubric(Course courseToAddTo, CourseRubric rubricToAdd)
        {
            string assignment_types = "";
            string weight_per_types = "";
            for (int i = 0; i < courseToAddTo.CourseRubric.GradeTypeWithWeights.Count; i++)
            {
                if (i == courseToAddTo.CourseRubric.GradeTypeWithWeights.Count - 1)
                {
                    assignment_types += courseToAddTo.CourseRubric.GradeTypeWithWeights.ElementAt(i).Key;
                    weight_per_types += courseToAddTo.CourseRubric.GradeTypeWithWeights.ElementAt(i).Value;
                }
                else
                {
                    assignment_types += courseToAddTo.CourseRubric.GradeTypeWithWeights.ElementAt(i).Key + "/";
                    weight_per_types += courseToAddTo.CourseRubric.GradeTypeWithWeights.ElementAt(i).Value + "/";
                }
            }
            MySqlConnection conn = DbConnection.GetConnection();
            using (conn)
            {
                conn.Open();
                var selectQuery =
                    "INSERT INTO rubrics(assignment_types, weight_per_type) VALUES (@assignment_types,@weight_per_type)";
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@assignment_types", assignment_types);
                    cmd.Parameters.AddWithValue("@weight_per_type", weight_per_types);

                    cmd.ExecuteNonQuery();
                }
                selectQuery =
                    "UPDATE courses SET rubric_id = @rubric_id WHERE courses.CRN = @CRNCheck)";
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@rubric_id", rubricToAdd.RubricID);
                    cmd.Parameters.AddWithValue("@CRNCheck", courseToAddTo.CourseInfo.CRN);

                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void UpdateCourseRubric(Course courseToAdd, CourseRubric rubricToUpdate)
        {
            string assignment_types = "";
            string weight_per_types = "";
            for (int i = 0; i < courseToAdd.CourseRubric.GradeTypeWithWeights.Count; i++)
            {
                if (i == courseToAdd.CourseRubric.GradeTypeWithWeights.Count - 1)
                {
                    assignment_types += courseToAdd.CourseRubric.GradeTypeWithWeights.ElementAt(i).Key;
                    weight_per_types += courseToAdd.CourseRubric.GradeTypeWithWeights.ElementAt(i).Value;
                }
                else
                {
                    assignment_types += courseToAdd.CourseRubric.GradeTypeWithWeights.ElementAt(i).Key + "/";
                    weight_per_types += courseToAdd.CourseRubric.GradeTypeWithWeights.ElementAt(i).Value + "/";
                }
            }
            MySqlConnection conn = DbConnection.GetConnection();
            using (conn)
            {
                conn.Open();
                var selectQuery =
                    "UPDATE rubrics SET assignment_types=@assignment_types, weight_per_type=@weight_per_type WHERE rubrics.rubric_id = @rubric_id";
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@assignment_types", assignment_types);
                    cmd.Parameters.AddWithValue("@weight_per_type", weight_per_types);

                    cmd.ExecuteNonQuery();
                }

                selectQuery =
                    "UPDATE courses SET rubric_id = @rubric_id WHERE courses.CRN = @CRNCheck)";
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@rubric_id", rubricToUpdate.RubricID);
                    cmd.Parameters.AddWithValue("@CRNCheck", courseToAdd.CourseInfo.CRN);

                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                conn.Close();
            }
        }
    }
}