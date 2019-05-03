using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CourseManagement.DAL;
using CourseManagement.Models;
using MySql.Data.MySqlClient;

namespace CoursesManagementDesktop.DAL
{
    class DesktopRubricDal
    {

        public void populateDataGrid(int CRNCheck, string gradeName, DataGrid grid)
        {
            
            MySqlConnection DataBaseConnection = DbConnection.GetConnection();

            using (DataBaseConnection)
            {
                DataBaseConnection.Open();
                var selectQuery = "Select Concat(fname,\" \", lname) as Student_Name, student_grade_items.grade_earned_points as Earned_Points, grade_defs.grade_total_points as Total_Points,grade_type as Grade_Type,student_grade_items.grade_feedback as Feedback FROM personal_info,student_grade_items,grade_defs,students WHERE personal_info.uid = students.uid AND student_grade_items.student_uid = students.uid AND grade_defs.grade_def_id = student_grade_items.grade_def_id AND grade_defs.course_CRN = @CRNCheck AND grade_defs.grade_name = @grade_name AND grade_defs.grade_def_id = student_grade_items.grade_def_id ";
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, DataBaseConnection))
                {
                    cmd.Parameters.AddWithValue("@CRNCheck", CRNCheck);
                    cmd.Parameters.AddWithValue("@grade_name", gradeName);


                    cmd.ExecuteNonQuery();

                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable("Student Grades");
                    dataAdapter.Fill(dt);
                    grid.ItemsSource = dt.DefaultView;
                    DataBaseConnection.Close();
                }
            }
        }




        public void GetCourseRubricByCRN(int CRNCheck,DataGrid grid)
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
                         
                        }
                    }
                    
                }
            }

            
        }
    }
}
