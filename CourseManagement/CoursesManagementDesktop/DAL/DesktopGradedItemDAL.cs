using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CourseManagement.App_Code;
using CourseManagement.DAL;
using MySql.Data.MySqlClient;

namespace CoursesManagementDesktop.DAL
{
    class DesktopGradedItemDAL
    {

        public void populateDataGrid(int CRNCheck, string gradeName, DataGrid grid)
        {
            
            MySqlConnection conn = DbConnection.GetConnection();

            using (conn)
            {
                conn.Open();
                var selectQuery =
                    "SELECT  name As \"Student Name\" ,grade_earned_points as \"Points Earned\",grade_total_points as \"Possible Points\",grade_feedback as \"FeedBack\",grade_name as \"Assignment Name\" FROM grade_items,grade_belongs_to_courses,students WHERE grade_items.grade_item_id = grade_belongs_to_courses.grade_item_id And students.uid = grade_items.student_uid   AND grade_belongs_to_courses.courses_CRN = @CRNCheck AND grade_name = @grade_name";
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@CRNCheck", CRNCheck);
                    cmd.Parameters.AddWithValue("@grade_name", gradeName);

                    
                    cmd.ExecuteNonQuery();

                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable("Student Grades");
                    dataAdapter.Fill(dt);
                    grid.ItemsSource = dt.DefaultView;
                    conn.Close();
                }
            }
        }
    }
}

