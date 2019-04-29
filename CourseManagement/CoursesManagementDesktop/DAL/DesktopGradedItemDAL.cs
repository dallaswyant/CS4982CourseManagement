using System.Data;
using System.Windows.Controls;
using CourseManagement.DAL;
using MySql.Data.MySqlClient;

namespace CoursesManagementDesktop.DAL
{
    internal class DesktopGradedItemDAL
    {
        #region Methods

        /// <summary>
        ///     populates the datagrid object to view all grades
        ///     if invalid input the data grid will not be populated
        /// </summary>
        /// <param name="CRNCheck">the crn number for course</param>
        /// <param name="gradeName">the assignment name</param>
        /// <param name="grid">the datagrid object</param>
        public void populateDataGrid(int CRNCheck, string gradeName, DataGrid grid)
        {
            var DataBaseConnection = DbConnection.GetConnection();

            using (DataBaseConnection)
            {
                DataBaseConnection.Open();
                var selectQuery =
                    "Select Concat(fname,\" \", lname) as Student_Name, student_grade_items.grade_earned_points as Earned_Points, grade_defs.grade_total_points as Total_Points,grade_type as Grade_Type,student_grade_items.grade_feedback as Feedback FROM personal_info,student_grade_items,grade_defs,students WHERE personal_info.uid = students.uid AND student_grade_items.student_uid = students.uid AND grade_defs.grade_def_id = student_grade_items.grade_def_id AND grade_defs.course_CRN = @CRNCheck AND grade_defs.grade_name = @grade_name AND grade_defs.grade_def_id = student_grade_items.grade_def_id ";
                using (var cmd = new MySqlCommand(selectQuery, DataBaseConnection))
                {
                    cmd.Parameters.AddWithValue("@CRNCheck", CRNCheck);
                    cmd.Parameters.AddWithValue("@grade_name", gradeName);

                    cmd.ExecuteNonQuery();

                    var dataAdapter = new MySqlDataAdapter(cmd);
                    var dt = new DataTable("Student Grades");
                    dataAdapter.Fill(dt);
                    grid.ItemsSource = dt.DefaultView;
                    DataBaseConnection.Close();
                }
            }
        }

        #endregion
    }
}