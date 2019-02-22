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
                //var selectQuery =     "Select Concat(fname," ", lname) as name, student_grade_items.grade_earned_points, grade_defs.grade_total_points,grade_type,student_grade_items.grade_feedback FROM personal_info,student_grade_items,grade_defs,students WHERE personal_info.uid = students.uid AND student_grade_items.student_uid = students.uid AND grade_defs.grade_def_id = student_grade_items.grade_def_id AND grade_defs.course_CRN = @CRNCheck";
                //using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                //{
                //    cmd.Parameters.AddWithValue("@CRNCheck", CRNCheck);
                //    cmd.Parameters.AddWithValue("@grade_name", gradeName);

                    
                //    cmd.ExecuteNonQuery();

                //    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
                //    DataTable dt = new DataTable("Student Grades");
                //    dataAdapter.Fill(dt);
                //    grid.ItemsSource = dt.DefaultView;
                //    conn.Close();
                //}
            }
        }
    }
}

