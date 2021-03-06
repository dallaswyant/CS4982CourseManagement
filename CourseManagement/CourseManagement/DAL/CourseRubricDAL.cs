﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using CourseManagement.Models;
using CourseManagement.Views;
using MySql.Data.MySqlClient;

namespace CourseManagement.DAL
{
    /// <summary>
    /// This Class defines a Course Rubric DAL for interacting with course rubrics on the Database
    /// </summary>
    [DataObject(true)]
    public class CourseRubricDAL
    {

        /// <summary>
        /// Gets the course rubric by CRN.
        /// </summary>
        /// <param name="CRNCheck">The CRN to check.</param>
        /// <returns>
        /// A list of RubricItems for the selected CRN
        /// </returns>
        /// <preconditions>
        /// CRNCheck must be greater than or equal to 0
        /// </preconditions>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<RubricItem> GetCourseRubricByCRN(int CRNCheck)
        {
            if (CRNCheck <= 0)
            {
                throw new Exception("CRNCheck must be greater than or equal to 0");
            }
            MySqlConnection dbConnection = DbConnection.GetConnection();
            
            using (dbConnection)
            {

                dbConnection.Open();
                var selectQuery = "SELECT * FROM rubrics WHERE rubrics.CRN = @CRNCheck";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, dbConnection))
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


        /// <summary>
        /// Adds the course rubric.
        /// </summary>
        /// <param name="CRNCheck">The CRN check.</param>
        /// <param name="rubricToAdd">The rubric to add.</param>
        /// <preconditions>
        /// CRNCheck must be greater than or equal to 0
        /// AND
        /// The list of rubric items cannot be null
        /// </preconditions>
        /// <postcondition>
        /// The database now has the rubric selected for the given CRNCheck
        /// </postcondition>
        public void AddCourseRubric(int CRNCheck, List<RubricItem> rubricToAdd)
        {
            if (CRNCheck <= 0)
            {
                throw new Exception("CRNCheck must be greater than or equal to 0");
            }

            if (rubricToAdd == null)
            {
                throw new Exception("The list of rubric items cannot be null");
            }
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
            MySqlConnection dbConnection = DbConnection.GetConnection();
            using (dbConnection)
            {
                dbConnection.Open();
                var selectQuery =
                    "INSERT INTO rubrics(assignment_types, weight_per_type,CRN) VALUES (@assignment_types,@weight_per_type,@CRNToAdd)";
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@assignment_types", assignment_types);
                    cmd.Parameters.AddWithValue("@weight_per_type", weight_per_types);
                    cmd.Parameters.AddWithValue("@CRN", CRNCheck);

                    cmd.ExecuteNonQuery();
                }

                dbConnection.Close();
            }
        }

        /// <summary>
        /// Updates the course rubric.
        /// </summary>
        /// <param name="crn">The CRN.</param>
        /// <param name="assignmentType">Type of the assignment.</param>
        /// <param name="assignmentWeight">The assignment weight.</param>
        /// <param name="original_AssignmentType">Type of the original assignment.</param>
        /// <param name="original_AssignmentWeight">The original assignment weight.</param>
        /// <param name="index">The index.</param>
        /// <param name="original_Index">Index of the original.</param>
        /// <param name="original_Crn">The original CRN.</param>
        [DataObjectMethod(DataObjectMethodType.Update)]
        public void UpdateCourseRubric(int crn, string assignmentType, int assignmentWeight, string original_AssignmentType, int original_AssignmentWeight, int index, int original_Index, int original_Crn)
        {

            
            List<RubricItem> rubric = GetCourseRubricByCRN(original_Crn);
            RubricItem original_item = rubric.Find(x =>
                x.AssignmentType.Equals(original_AssignmentType) && x.AssignmentWeight == original_AssignmentWeight);

            

            string assignment_types = "";
            string weight_per_types = "";
            for (int i = 0; i < rubric.Count; i++)
            {
                if (i == original_item.Index && i != rubric.Count - 1)
                {
                    assignment_types += assignmentType + "/";
                    weight_per_types += assignmentWeight + "/";
                }
                else if(i != rubric.Count - 1)
                {
                    assignment_types += rubric[i].AssignmentType + "/";
                    weight_per_types += rubric[i].AssignmentWeight + "/";
                } else if (original_item.Index == i && i == rubric.Count - 1)
                {
                    assignment_types += assignmentType;
                    weight_per_types += assignmentWeight;
                }
                else
                {
                    assignment_types += rubric[i].AssignmentType;
                    weight_per_types += rubric[i].AssignmentWeight;
                }
            }
            MySqlConnection dbConnection = DbConnection.GetConnection();
            using (dbConnection)
            {
                dbConnection.Open();

                
                var selectQuery =
                    "UPDATE rubrics SET assignment_types=@assignment_types, weight_per_type=@weight_per_type WHERE CRN = @CRN";
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@assignment_types", assignment_types);
                    cmd.Parameters.AddWithValue("@weight_per_type", weight_per_types);
                    cmd.Parameters.AddWithValue("@CRN", original_Crn);
                    cmd.ExecuteNonQuery();
                }

                dbConnection.Close();
            }
        }



        /// <summary>
        /// Deletes the course rubric.
        /// </summary>
        /// <param name="crn">The CRN.</param>
        /// <param name="assignmentType">Type of the assignment.</param>
        /// <param name="assignmentWeight">The assignment weight.</param>
        /// <param name="original_AssignmentType">Type of the original assignment.</param>
        /// <param name="original_AssignmentWeight">The original assignment weight.</param>
        /// <param name="index">The index.</param>
        /// <param name="original_Index">Index of the original.</param>
        /// <param name="original_Crn">The original CRN.</param>
        [DataObjectMethod(DataObjectMethodType.Delete)]
        public bool DeleteCourseRubric(int crn, string assignmentType, int assignmentWeight, string original_AssignmentType, int original_AssignmentWeight, int index, int original_Index, int original_Crn)
        {
           
            
            List<RubricItem> rubric = GetCourseRubricByCRN(original_Crn);
            RubricItem original_item = rubric.Find(x =>
                x.AssignmentType.Equals(original_AssignmentType) && x.AssignmentWeight == original_AssignmentWeight);
            bool canDelete = false;
            MySqlConnection dbConnection = DbConnection.GetConnection();
            using (dbConnection)
            {
                dbConnection.Open();
                var selectQuery =
                    "SELECT * from grade_defs WHERE grade_defs.grade_type = @type_to_check AND grade_defs.course_CRN = @CRNCheck";
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@type_to_check", original_item.AssignmentType);
                    cmd.Parameters.AddWithValue("@CRNCheck", original_Crn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int totalPointsOrdinal = reader.GetOrdinal("grade_total_points");
                        int gradeTypeOrdinal = reader.GetOrdinal("grade_type");
                        if (reader.Read())
                        {
                            canDelete = false;
                        }
                        else
                        {
                            canDelete = true;
                        }
                    }

                    dbConnection.Close();
                }
            }

            if (canDelete)
            {
                string assignment_types = "";
                string weight_per_types = "";
                for (int i = 0; i < rubric.Count; i++)
                {
                    if (i == original_item.Index && i == rubric.Count - 1)
                    {
                        if (assignment_types.Length > 1)
                        {
                            assignment_types = assignment_types.Substring(0, assignment_types.Length - 1);
                            weight_per_types = weight_per_types.Substring(0, weight_per_types.Length - 1);
                        }

                    }
                    else if (i == original_item.Index)
                    {

                    }
                    else if (i != rubric.Count - 1)
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

                dbConnection = DbConnection.GetConnection();
                using (dbConnection)
                {
                    dbConnection.Open();
                    var selectQuery =
                        "UPDATE rubrics SET assignment_types=@assignment_types, weight_per_type=@weight_per_type WHERE CRN = @CRN";
                    using (MySqlCommand cmd = new MySqlCommand(selectQuery, dbConnection))
                    {
                        cmd.Parameters.AddWithValue("@assignment_types", assignment_types);
                        cmd.Parameters.AddWithValue("@weight_per_type", weight_per_types);
                        cmd.Parameters.AddWithValue("@CRN", original_item.CRN);
                        cmd.ExecuteNonQuery();
                    }

                    dbConnection.Close();
                }
            }

            return canDelete;
        }




        /// <summary>
        /// Inserts the course rubric.
        /// </summary>
        /// <param name="assignmentType">Type of the assignment.</param>
        /// <param name="assignmentWeight">The assignment weight.</param>
        /// <preconditions>
        /// Assignment type cannot be null
        /// AND
        /// Assignment weight must be greater than or equal to zero
        /// </preconditions>
        /// <postcondition>
        /// Inserts a new course rubric item for the given CRN in the session state object
        /// </postcondition>
        [DataObjectMethod(DataObjectMethodType.Insert)]
        public void InsertCourseRubric(string assignmentType, int assignmentWeight)
        {
            if (assignmentType == null)
            {
                throw new Exception("Assignment type cannot be null");
            }

            if (assignmentWeight < 0)
            {
                throw new Exception("Assignment weight must be greater than or equal to zero");
            }
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
            MySqlConnection dbConnection = DbConnection.GetConnection();
            using (dbConnection)
            {
                dbConnection.Open();
                var updateQuery =
                    "UPDATE rubrics SET assignment_types=@assignment_types, weight_per_type=@weight_per_type WHERE CRN = @CRN";
                using (MySqlCommand cmd = new MySqlCommand(updateQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@assignment_types", assignment_types);
                    cmd.Parameters.AddWithValue("@weight_per_type", weight_per_types);
                    cmd.Parameters.AddWithValue("@CRN", CRN);
                    cmd.ExecuteNonQuery();
                }
                dbConnection.Close();
            }
        }

        public void InsertCourseRubric(string assignmentType, int assignmentWeight,int crn)
        {
            if (assignmentType == null)
            {
                throw new Exception("Assignment type cannot be null");
            }

            if (assignmentWeight < 0)
            {
                throw new Exception("Assignment weight must be greater than or equal to zero");
            }
            

            
            List<RubricItem> rubric = GetCourseRubricByCRN(crn);
            RubricItem item = new RubricItem(crn, assignmentType, assignmentWeight, rubric.Count);
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
            MySqlConnection dbConnection = DbConnection.GetConnection();
            using (dbConnection)
            {
                dbConnection.Open();
                var updateQuery =
                    "UPDATE rubrics SET assignment_types=@assignment_types, weight_per_type=@weight_per_type WHERE CRN = @CRN";
                using (MySqlCommand cmd = new MySqlCommand(updateQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@assignment_types", assignment_types);
                    cmd.Parameters.AddWithValue("@weight_per_type", weight_per_types);
                    cmd.Parameters.AddWithValue("@CRN", crn);
                    cmd.ExecuteNonQuery();
                }
                dbConnection.Close();
            }
        }


        /// <summary>
        /// Gets the assignment types by CRN.
        /// </summary>
        /// <param name="CRNCheck">The CRN check.</param>
        /// <returns>
        /// A list of strings representing the different type of
        /// assignments for the rubrics for the selected CRNCheck
        /// </returns>
        /// <preconditions>
        /// CRNCheck must be greater than or equal to 0
        /// </preconditions>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<string> GetAssignmentTypesByCRN(int CRNCheck)
        {
            if (CRNCheck <= 0)
            {
                throw new Exception("CRNCheck must be greater than or equal to 0");
            }
            MySqlConnection dbConnection = DbConnection.GetConnection();

            using (dbConnection)
            {

                dbConnection.Open();
                var selectQuery = "SELECT * FROM rubrics WHERE rubrics.CRN = @CRNCheck";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, dbConnection))
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