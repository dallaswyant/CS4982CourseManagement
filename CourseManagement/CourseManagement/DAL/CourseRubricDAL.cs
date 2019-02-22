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
    /// <summary>
    /// This Class defines a Course Rubric DAL for interacting with course rubrics on the Database
    /// </summary>
    [DataObject(true)]
    public class CourseRubricDAL
    {
        /// <summary>
        /// Gets the course rubric by CRN.
        /// </summary>
        /// <param name="CRNCheck">The CRN check.</param>
        /// <returns>A list of rubric items representing the course rubric for the selected CRN</returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<RubricItem> GetCourseRubricByCRN(int CRNCheck)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            
            using (conn)
            {

                conn.Open();
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

        /// <summary>
        /// Adds the course rubric to the selected course.
        /// </summary>
        /// <param name="CRN">The CRN for the course.</param>
        /// <param name="rubricToAdd">The rubric to add to the course.</param>
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

            int CRN = (int) HttpContext.Current.Session["CRN"];
            List<RubricItem> rubric = GetCourseRubricByCRN(CRN);
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
                /**
                StudentDAL studentGetter = new StudentDAL();
                List<Student> students = studentGetter.GetStudentsByCRN(CRN);
                foreach (var student in students)
                {
                    var updateQuery =
                        "UPDATE grade_items SET grade_type = @newType WHERE grade_item_id = (SELECT grade_items.grade_item_id FROM grade_belongs_to_courses WHERE grade_belongs_to_courses.courses_CRN = @CRN  AND grade_belongs_to_courses.grade_item_id = grade_items.grade_item_id AND grade_items.grade_type = @oldType AND grade_items.student_uid = @studentUID)";
                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@newType", assignmentType);
                        cmd.Parameters.AddWithValue("@oldType", original_AssignmentType);
                        cmd.Parameters.AddWithValue("@CRN", CRN);
                        cmd.Parameters.AddWithValue("@studentUID", student.StudentUID);
                        cmd.ExecuteNonQuery();
                    }
                } */
                conn.Close();
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
           
            RubricItem item =  HttpContext.Current.Session["RubricItemToDelete"] as RubricItem;
            List<RubricItem> rubric = GetCourseRubricByCRN(item.CRN);
            bool canDelete = false;
            MySqlConnection conn = DbConnection.GetConnection();
            using (conn)
            {
                conn.Open();
                var selectQuery =
                    "SELECT * from grade_defs WHERE grade_defs.grade_type = @type_to_check AND grade_defs.course_CRN = @CRNCheck";
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@type_to_check", item.AssignmentType);
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

                    conn.Close();
                }
            }

            if (canDelete)
            {
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

                    }
                    else if (i == item.Index)
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

                conn = DbConnection.GetConnection();
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

            return canDelete;
        }


        /// <summary>
        /// Inserts the course rubric.
        /// </summary>
        /// <param name="assignmentType">Type of the assignment.</param>
        /// <param name="assignmentWeight">The assignment weight.</param>
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
        /// <summary>
        /// Gets the assignment types by CRN.
        /// </summary>
        /// <param name="CRNCheck">The CRN of the selected course</param>
        /// <returns> A list of assignment types for the selected course</returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<string> GetAssignmentTypesByCRN(int CRNCheck)
        {

            MySqlConnection conn = DbConnection.GetConnection();

            using (conn)
            {

                conn.Open();
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