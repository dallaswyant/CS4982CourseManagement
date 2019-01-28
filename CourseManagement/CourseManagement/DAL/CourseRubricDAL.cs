using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using CourseManagement.App_Code;
using CourseManagement.Views;
using MySql.Data.MySqlClient;

namespace CourseManagement.DAL
{
    [DataObject(true)]
    public class CourseRubricDAL
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<RubricItem> GetCourseRubricByCRN(int CRNCheck)
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
                            List<RubricItem> rubricStuff = new List<RubricItem>();
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
                                    if (!string.IsNullOrWhiteSpace(types[i]))
                                    {
                                        RubricItem rubricItem = new RubricItem(CRNCheck, types[i], Convert.ToInt32(weights[i]), i);
                                        rubricStuff.Add(rubricItem);
                                    }


                            }
                            return rubricStuff;
                        }
                    }
                    
                }
            }

            return null;
        }

        public void AddCourseRubric(int CRN, List<RubricItem> rubricToAdd)
        {
            string assignment_types = "";
            string weight_per_types = "";
            for (int i = 0; i < rubricToAdd.Count; i++)
            {
                if (i == rubricToAdd.Count - 1)
                {
                    assignment_types += rubricToAdd[i].AssignmentType;
                    weight_per_types += rubricToAdd[i].AssignmentWeight;
                }
                else
                {
                    assignment_types += rubricToAdd[i].AssignmentType + "/";
                    weight_per_types += rubricToAdd[i].AssignmentWeight + "/";
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

        public void DeleteCourseRubric(int CRN, int index)
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
        [DataObjectMethod(DataObjectMethodType.Update)]
        public void UpdateCourseRubric(int crn, string assignmentType, int assignmentWeight, int index)
        {
            int CRN = (int) HttpContext.Current.Session["CRN"];
            List<RubricItem> rubric = GetCourseRubricByCRN(CRN);
            RubricItem item = rubric.Find(x =>
                x.AssignmentType.Equals(assignmentType) && x.AssignmentWeight == assignmentWeight);

            

            string assignment_types = "";
            string weight_per_types = "";
            for (int i = 0; i < rubric.Count; i++)
            {
                if (i == item.Index && i != rubric.Count - 1)
                {
                    assignment_types += item.AssignmentType + "/";
                    weight_per_types += item.AssignmentWeight + "/";
                }
                else if(i != rubric.Count - 1)
                {
                    assignment_types += rubric[i].AssignmentType + "/";
                    weight_per_types += rubric[i].AssignmentWeight + "/";
                } else if (item.Index == i && i == rubric.Count - 1)
                {
                    assignment_types += item.AssignmentType;
                    weight_per_types += item.AssignmentWeight;
                }
                else
                {
                    assignment_types += rubric[i].AssignmentType;
                    weight_per_types += rubric[i].AssignmentWeight;
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



        [DataObjectMethod(DataObjectMethodType.Delete)]
        public void DeleteCourseRubric(int CRN, string assignmentType, int assignmentWeight, int index)
        {
           
            RubricItem item =  HttpContext.Current.Session["RubricItemToDelete"] as RubricItem;
            List<RubricItem> rubric = GetCourseRubricByCRN(item.CRN);

            string assignment_types = "";
            string weight_per_types = "";
            for (int i = 0; i < rubric.Count; i++)
            {
                if (i == item.Index && i == rubric.Count - 1)
                {
                    if (assignment_types.Length > 1)
                    {
                    assignment_types = assignment_types.Substring(0, assignment_types.Length - 1);
                    weight_per_types = weight_per_types.Substring(0, weight_per_types.Length - 1);
                    }

                } else if (i == item.Index)
                {

                }
                else if(i != rubric.Count - 1)
                {
                    assignment_types += rubric[i].AssignmentType + "/";
                    weight_per_types += rubric[i].AssignmentWeight + "/";
                }
                else
                {
                    assignment_types += rubric[i].AssignmentType;
                    weight_per_types += rubric[i].AssignmentWeight;
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
                    cmd.Parameters.AddWithValue("@CRN", item.CRN);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }


        [DataObjectMethod(DataObjectMethodType.Insert)]
        public void InsertCourseRubric(string assignmentType, int assignmentWeight)
        {
            int CRN = (int) HttpContext.Current.Session["CRN"];

            
            List<RubricItem> rubric = GetCourseRubricByCRN(CRN);
            RubricItem item = new RubricItem(CRN, assignmentType, assignmentWeight, rubric.Count);
            rubric.Add(item);


            string assignment_types = "";
            string weight_per_types = "";
            for (int i = 0; i < rubric.Count-1; i++)
            {

                    assignment_types += rubric[i].AssignmentType + "/";
                    weight_per_types += rubric[i].AssignmentWeight + "/";
            }

            assignment_types += item.AssignmentType;
            weight_per_types += item.AssignmentWeight;
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
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<string> GetAssignmentTypesByCRN(int CRNCheck)
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
                            List<RubricItem> rubricStuff = new List<RubricItem>();
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
                                if (!string.IsNullOrWhiteSpace(types[i]))
                                {
                                    RubricItem rubricItem = new RubricItem(CRNCheck, types[i], Convert.ToInt32(weights[i]), i);
                                    rubricStuff.Add(rubricItem);
                                }


                            }
                            List<String> gradeTypes = new List<string>();
                            foreach(var item in rubricStuff)
                            {
                                gradeTypes.Add(item.AssignmentType);
                            }

                            return gradeTypes;
                        }
                    }

                }
            }

            return null;

        }
    }
}