using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseManagement.DAL;
using MySql.Data.Common;
using MySql.Data.MySqlClient;

namespace CoursesManagementDesktop.DAL
{
    class OfflineDBBuilder
    {
        public void CopyAllDBDataToLocal()
        {
            SQLiteConnection.CreateFile("../../Data/MyDatabase.sqlite");
            this.DumpDBData();
        }

        public void DumpDBData()
        {

            MySqlConnection mySqlConnection = DbConnection.GetConnection();

            SQLiteConnection sqLiteConnection = new SQLiteConnection("Data Source=../../Data/MyDatabase.sqlite;Version=3;");

            mySqlConnection.Open();

            sqLiteConnection.Open();

            this.copyRoles(mySqlConnection, sqLiteConnection);//
            this.copyUsers(mySqlConnection, sqLiteConnection);//
            this.copyPersonalInfo(mySqlConnection, sqLiteConnection);//
            this.copyUserHasRole(mySqlConnection,sqLiteConnection);//
            this.copyTeachers(mySqlConnection, sqLiteConnection);//
            this.copyStudents(mySqlConnection, sqLiteConnection);//
            this.copySemesters(mySqlConnection, sqLiteConnection);//
            this.copyCourses(mySqlConnection, sqLiteConnection);//
            this.copyRubrics(mySqlConnection, sqLiteConnection);//
            this.copyStudentHasCourses(mySqlConnection, sqLiteConnection);//
            this.copyTeacherTeachesCourses(mySqlConnection, sqLiteConnection);//
            this.copyGradeDefs(mySqlConnection, sqLiteConnection);//
            this.copyStudentGradeItems(mySqlConnection, sqLiteConnection);//

            mySqlConnection.Close();

            sqLiteConnection.Close();

        }

        private void copyUsers(MySqlConnection mySqlConnection, SQLiteConnection sqLiteConnection)
        {

            string query = "";

            MySqlCommand mySqlCommand = new MySqlCommand("select uid,password from users", mySqlConnection);
            var Reader = mySqlCommand.ExecuteReader();

            int i = 0;
            query = "DROP TABLE IF EXISTS `users`; CREATE TABLE `users`(uid VARCHAR(45),password TINYTEXT NOT NULL,PRIMARY KEY(uid));";
            SQLiteCommand sqLiteCommand = new SQLiteCommand(query, sqLiteConnection);
            sqLiteCommand.ExecuteNonQuery();
            while (Reader.Read())

            {

                query = "insert into users(uid,password) values('" + Reader.GetValue(0).ToString() + "','" + Reader.GetValue(1).ToString() + "')";

                sqLiteCommand = new SQLiteCommand(query, sqLiteConnection);

                i = i + sqLiteCommand.ExecuteNonQuery();

            }
            Reader.Close();
        }

        private void copyUserHasRole(MySqlConnection mySqlConnection, SQLiteConnection sqLiteConnection)
        {
            string query = "";

            MySqlCommand mySqlCommand = new MySqlCommand("select roles_role_id,user_uid from user_has_role", mySqlConnection);
            var Reader = mySqlCommand.ExecuteReader();

            int i = 0;
            query = "DROP TABLE IF EXISTS `user_has_role`; CREATE TABLE `user_has_role`(roles_role_id INTEGER,user_uid VARCHAR(45),FOREIGN KEY(roles_role_id) REFERENCES `roles`(role_id)ON DELETE CASCADE ON UPDATE CASCADE,FOREIGN KEY(user_uid) REFERENCES `users`(uid)ON DELETE CASCADE ON UPDATE CASCADE,PRIMARY KEY(roles_role_id, user_uid)); ";
            SQLiteCommand sqLiteCommand = new SQLiteCommand(query, sqLiteConnection);
            sqLiteCommand.ExecuteNonQuery();
            while (Reader.Read())
            {

                query = "insert into user_has_role(roles_role_id,user_uid) values('" + Reader.GetValue(0).ToString() + "','" + Reader.GetValue(1).ToString() + "')";

                sqLiteCommand = new SQLiteCommand(query, sqLiteConnection);

                i = i + sqLiteCommand.ExecuteNonQuery();

            }
            Reader.Close();
        }

        private void copyTeacherTeachesCourses(MySqlConnection mySqlConnection, SQLiteConnection sqLiteConnection)
        {
            string query = "";

            MySqlCommand mySqlCommand = new MySqlCommand("select * from teacher_teaches_courses", mySqlConnection);
            var Reader = mySqlCommand.ExecuteReader();

            int i = 0;
            query = "DROP TABLE IF EXISTS `teacher_teaches_courses`; CREATE TABLE `teacher_teaches_courses`(teacher_uid VARCHAR(45),courses_CRN INTEGER,FOREIGN KEY(teacher_uid) REFERENCES `users`(uid)ON DELETE CASCADE ON UPDATE CASCADE,FOREIGN KEY(courses_CRN) REFERENCES `courses`(CRN)ON DELETE CASCADE ON UPDATE CASCADE,PRIMARY KEY(teacher_uid, courses_CRN)); ";
            SQLiteCommand sqLiteCommand = new SQLiteCommand(query, sqLiteConnection);
            sqLiteCommand.ExecuteNonQuery();
            while (Reader.Read())

            {

                query = "insert into teacher_teaches_courses(teacher_uid,courses_CRN)values('" + Reader.GetValue(0).ToString() + "','" + Reader.GetValue(1).ToString() + "')";

                sqLiteCommand = new SQLiteCommand(query, sqLiteConnection);

                i = i + sqLiteCommand.ExecuteNonQuery();

            }
            Reader.Close();
        }

        private void copyTeachers(MySqlConnection mySqlConnection, SQLiteConnection sqLiteConnection)
        {
            string query = "";

            MySqlCommand mySqlCommand = new MySqlCommand("select * from teachers", mySqlConnection);
            var Reader = mySqlCommand.ExecuteReader();

            int i = 0;
            query = "DROP TABLE IF EXISTS `teachers`; CREATE TABLE `teachers`(uid VARCHAR(45) NOT NULL,email VARCHAR(45) NOT NULL,public_email BIT(1) NOT NULL,phone_number VARCHAR(45) NOT NULL,office_location VARCHAR(45) NOT NULL,office_hours VARCHAR(45) NOT NULL,FOREIGN KEY(uid) REFERENCES `users`(uid)ON DELETE CASCADE ON UPDATE CASCADE,PRIMARY KEY(uid)); ";
            SQLiteCommand sqLiteCommand = new SQLiteCommand(query, sqLiteConnection);
            sqLiteCommand.ExecuteNonQuery();
            while (Reader.Read())

            {

                query = "insert into teachers(uid,email,public_email,phone_number,office_location,office_hours) values('" + Reader.GetValue(0).ToString() + "','" + Reader.GetValue(1).ToString() + "','" + Reader.GetValue(2) + "','" + Reader.GetValue(3).ToString() + "','" + Reader.GetValue(4) + "','" + Reader.GetValue(5).ToString() + "')";

                sqLiteCommand = new SQLiteCommand(query, sqLiteConnection);

                i = i + sqLiteCommand.ExecuteNonQuery();

            }
            Reader.Close();
        }

        private void copyStudentHasCourses(MySqlConnection mySqlConnection, SQLiteConnection sqLiteConnection)
        {
            string query = "";

            MySqlCommand mySqlCommand = new MySqlCommand("select * from student_has_courses", mySqlConnection);
            var Reader = mySqlCommand.ExecuteReader();

            int i = 0;
            query =
                "DROP TABLE IF EXISTS `student_has_courses`; CREATE TABLE `student_has_courses`(student_uid VARCHAR(45),courses_CRN INTEGER,grade_earned CHAR,FOREIGN KEY(student_uid) REFERENCES `users`(uid)ON DELETE CASCADE ON UPDATE CASCADE,FOREIGN KEY(courses_CRN) REFERENCES `courses`(CRN)ON DELETE CASCADE ON UPDATE CASCADE,PRIMARY KEY(student_uid, courses_CRN));";
            SQLiteCommand sqLiteCommand = new SQLiteCommand(query, sqLiteConnection);
            sqLiteCommand.ExecuteNonQuery();
            while (Reader.Read())
            {

                query = "insert into student_has_courses(student_uid,courses_CRN,grade_earned) values('" + Reader.GetValue(0).ToString() + "','" + Reader.GetValue(1).ToString() + "','" + Reader.GetValue(2) + "')";

                sqLiteCommand = new SQLiteCommand(query, sqLiteConnection);

                i = i + sqLiteCommand.ExecuteNonQuery();

            }
            Reader.Close();
        }

        private void copyStudentGradeItems(MySqlConnection mySqlConnection, SQLiteConnection sqLiteConnection)
        {
            string query = "";

            MySqlCommand mySqlCommand = new MySqlCommand("select * from student_grade_items", mySqlConnection);
            var Reader = mySqlCommand.ExecuteReader();

            int i = 0;
            query = "DROP TABLE IF EXISTS `student_grade_items`; CREATE TABLE `student_grade_items`(grade_def_id INTEGER,student_uid VARCHAR(45),grade_earned_points DECIMAL(5, 2),grade_feedback TEXT,time_graded DATETIME,FOREIGN KEY(student_uid) REFERENCES `users`(uid)ON DELETE CASCADE ON UPDATE CASCADE,FOREIGN KEY(grade_def_id) REFERENCES `grade_defs`(grade_def_id)ON DELETE CASCADE ON UPDATE CASCADE,PRIMARY KEY(grade_def_id, student_uid)); ";
            SQLiteCommand sqLiteCommand = new SQLiteCommand(query, sqLiteConnection);
            sqLiteCommand.ExecuteNonQuery();
            while (Reader.Read())
            {
                query = "insert into student_grade_items(grade_def_id,student_uid,grade_earned_points,grade_feedback,time_graded) values('" + Reader.GetValue(0).ToString() + "','" + Reader.GetValue(1).ToString() + "','" + Reader.GetValue(2) + "','" + Reader.GetValue(3).ToString() + "','" + Reader.GetValue(4) + "')";

                sqLiteCommand = new SQLiteCommand(query, sqLiteConnection);

                i = i + sqLiteCommand.ExecuteNonQuery();
            }
            Reader.Close();
        }

        private void copyStudents(MySqlConnection mySqlConnection, SQLiteConnection sqLiteConnection)
        {
            string query = "";

            MySqlCommand mySqlCommand = new MySqlCommand("select * from students", mySqlConnection);
            var Reader = mySqlCommand.ExecuteReader();

            int i = 0;
            query = "DROP TABLE IF EXISTS `students`; CREATE TABLE `students`(uid VARCHAR(45) NOT NULL,email VARCHAR(45) NOT NULL,degree_name VARCHAR(45) NOT NULL,FOREIGN KEY(uid) REFERENCES `users`(uid)ON DELETE CASCADE ON UPDATE CASCADE,PRIMARY KEY(uid)); ";
            SQLiteCommand sqLiteCommand = new SQLiteCommand(query, sqLiteConnection);
            sqLiteCommand.ExecuteNonQuery();
            while (Reader.Read())
            {

                query = "insert into students(uid,email,degree_name) values('" + Reader.GetValue(0).ToString() + "','" + Reader.GetValue(1).ToString() + "','" + Reader.GetValue(2) + "')";

                sqLiteCommand = new SQLiteCommand(query, sqLiteConnection);

                i = i + sqLiteCommand.ExecuteNonQuery();

            }
            Reader.Close();
        }

        private void copySemesters(MySqlConnection mySqlConnection, SQLiteConnection sqLiteConnection)
        {
            string query = "";

            MySqlCommand mySqlCommand = new MySqlCommand("select * from semesters", mySqlConnection);
            var Reader = mySqlCommand.ExecuteReader();

            int i = 0;
            query = "DROP TABLE IF EXISTS `semesters`; CREATE TABLE `semesters`(semester_name VARCHAR(45) NOT NULL,start_date DATETIME NOT NULL,end_date DATETIME NOT NULL,final_grade_deadline DATETIME NOT NULL,add_drop_deadline DATETIME NOT NULL,PRIMARY KEY(semester_name)); ";
            SQLiteCommand sqLiteCommand = new SQLiteCommand(query, sqLiteConnection);
            sqLiteCommand.ExecuteNonQuery();
            while (Reader.Read())

            {

                query =
                    "insert into semesters(semester_name,start_date,end_date,final_grade_deadline,add_drop_deadline) values('" +
                    Reader.GetValue(0).ToString() + "','" + Reader.GetValue(1).ToString() + "','" + Reader.GetValue(2) +
                    "','" + Reader.GetValue(3).ToString() + "','" + Reader.GetValue(4) + "')";

                sqLiteCommand = new SQLiteCommand(query, sqLiteConnection);

                i = i + sqLiteCommand.ExecuteNonQuery();

            }

            Reader.Close();
        }

        private void copyRubrics(MySqlConnection mySqlConnection, SQLiteConnection sqLiteConnection)
        {
            string query = "";

            MySqlCommand mySqlCommand = new MySqlCommand("select * from rubrics", mySqlConnection);
            var Reader = mySqlCommand.ExecuteReader();

            int i = 0;
            query = "DROP TABLE IF EXISTS `rubrics`; CREATE TABLE `rubrics`(rubric_id INTEGER AUTO_INCREMENT,assignment_types VARCHAR(45),weight_per_type VARCHAR(45),CRN INTEGER,FOREIGN KEY(CRN) REFERENCES `courses`(CRN)ON DELETE CASCADE ON UPDATE CASCADE,PRIMARY KEY(rubric_id)); ";
            SQLiteCommand sqLiteCommand = new SQLiteCommand(query, sqLiteConnection);
            sqLiteCommand.ExecuteNonQuery();
            while (Reader.Read())
            {

                query = "insert into rubrics(rubric_id,assignment_types,weight_per_type,CRN) values('" + Reader.GetValue(0).ToString() + "','" + Reader.GetValue(1).ToString() + "','" + Reader.GetValue(2) + "','" + Reader.GetValue(3) + "')";

                sqLiteCommand = new SQLiteCommand(query, sqLiteConnection);

                i = i + sqLiteCommand.ExecuteNonQuery();

            }
            Reader.Close();
        }

        private void copyRoles(MySqlConnection mySqlConnection, SQLiteConnection sqLiteConnection)
        {
            string query = "";

            MySqlCommand mySqlCommand = new MySqlCommand("select role_id,role_name from roles", mySqlConnection);
            var Reader = mySqlCommand.ExecuteReader();

            int i = 0;
            query = "DROP TABLE IF EXISTS `roles`; CREATE TABLE `roles`(role_id INTEGER AUTO_INCREMENT,role_name VARCHAR(30) NOT NULL,PRIMARY KEY(role_id)); ";
            SQLiteCommand sqLiteCommand = new SQLiteCommand(query, sqLiteConnection);
            sqLiteCommand.ExecuteNonQuery();
            while (Reader.Read())

            {

                query = "insert into roles(role_id,role_name) values('" + Reader.GetValue(0).ToString() + "','" + Reader.GetValue(1).ToString() + "')";

                sqLiteCommand = new SQLiteCommand(query, sqLiteConnection);

                i = i + sqLiteCommand.ExecuteNonQuery();

            }
            Reader.Close();
        }

        private void copyPersonalInfo(MySqlConnection mySqlConnection, SQLiteConnection sqLiteConnection)
        {
            string query = "";

            MySqlCommand mySqlCommand = new MySqlCommand("select * from personal_info", mySqlConnection);
            var Reader = mySqlCommand.ExecuteReader();

            int i = 0;
            query = "DROP TABLE IF EXISTS `personal_info`; CREATE TABLE `personal_info`(uid VARCHAR(45) NOT NULL,fname VARCHAR(45) NOT NULL,minit CHAR,lname VARCHAR(45) NOT NULL,addr_id INTEGER,phone_number VARCHAR(12),sex CHAR NOT NULL,dob DATETIME NOT NULL,race VARCHAR(45) NOT NULL,email VARCHAR(45) NOT NULL,SSN CHAR(9),FOREIGN KEY(uid) REFERENCES `users`(uid)ON DELETE NO ACTION ON UPDATE CASCADE,PRIMARY KEY(uid)); ";
            SQLiteCommand sqLiteCommand = new SQLiteCommand(query, sqLiteConnection);
            sqLiteCommand.ExecuteNonQuery();
            while (Reader.Read())

            {

                query = "insert into personal_info(uid,fname,minit,lname,addr_id,phone_number,sex,dob,race,email,SSN) values('" + Reader.GetValue(0).ToString() + "','" + Reader.GetValue(1).ToString() + "','" + Reader.GetValue(2) + "','" + Reader.GetValue(3).ToString() + "','" + Reader.GetValue(4) + "','" + Reader.GetValue(5).ToString() + "','" + Reader.GetValue(6) + "','" + Reader.GetValue(7).ToString() + "','" + Reader.GetValue(8) + "','" + Reader.GetValue(9).ToString() + "','" + Reader.GetValue(10) + "')";

                sqLiteCommand = new SQLiteCommand(query, sqLiteConnection);

                i = i + sqLiteCommand.ExecuteNonQuery();

            }
            Reader.Close();
        }

        private void copyGradeDefs(MySqlConnection mySqlConnection, SQLiteConnection sqLiteConnection)
        {
            string query = "";

            MySqlCommand mySqlCommand = new MySqlCommand("select * from grade_defs", mySqlConnection);
            var Reader = mySqlCommand.ExecuteReader();

            int i = 0;
            query = "DROP TABLE IF EXISTS `grade_defs`; CREATE TABLE `grade_defs`(grade_def_id INTEGER AUTO_INCREMENT,grade_total_points DECIMAL(5, 2),grade_description TINYTEXT,grade_type VARCHAR(45),grade_name VARCHAR(45),is_public BIT(1) NOT NULL,course_CRN INTEGER,FOREIGN KEY(course_CRN) REFERENCES `courses`(CRN)ON DELETE CASCADE ON UPDATE CASCADE,PRIMARY KEY(grade_def_id)); ";
            SQLiteCommand sqLiteCommand = new SQLiteCommand(query, sqLiteConnection);
            sqLiteCommand.ExecuteNonQuery();
            while (Reader.Read())

            {

                query = "insert into grade_defs(grade_def_id,grade_total_points,grade_description,grade_type,grade_name,is_public,course_CRN) values('" + Reader.GetValue(0).ToString() + "','" + Reader.GetValue(1).ToString() + "','" + Reader.GetValue(2) + "','" + Reader.GetValue(3).ToString() + "','" + Reader.GetValue(4) + "','" + Reader.GetValue(5).ToString() + "','" + Reader.GetValue(6) + "')";

                sqLiteCommand = new SQLiteCommand(query, sqLiteConnection);

                i = i + sqLiteCommand.ExecuteNonQuery();

            }
            Reader.Close();
        }

        private void copyCourses(MySqlConnection mySqlConnection, SQLiteConnection sqLiteConnection)
        {
            string query = "";

            MySqlCommand mySqlCommand = new MySqlCommand("select * from courses", mySqlConnection);
            var Reader = mySqlCommand.ExecuteReader();

            int i = 0;
            query = "DROP TABLE IF EXISTS `courses`; CREATE TABLE `courses`(CRN INTEGER AUTO_INCREMENT,dept_name VARCHAR(45),course_name VARCHAR(45) NOT NULL,course_desc TINYTEXT NOT NULL,section_num VARCHAR(45) NOT NULL,seats_max INTEGER NOT NULL,location VARCHAR(45) NOT NULL,semester_name VARCHAR(45),course_time_id INTEGER,FOREIGN KEY(semester_name) REFERENCES `semesters`(semester_name)ON DELETE CASCADE ON UPDATE CASCADE,PRIMARY KEY(CRN)); ";
            SQLiteCommand sqLiteCommand = new SQLiteCommand(query, sqLiteConnection);
            sqLiteCommand.ExecuteNonQuery();
            while (Reader.Read())

            {

                query = "insert into courses(CRN,dept_name,course_name,course_desc,section_num,seats_max,location,semester_name,course_time_id) values('" + Reader.GetValue(0).ToString() + "','" + Reader.GetValue(1).ToString() + "','" + Reader.GetValue(2) + "','" + Reader.GetValue(3).ToString() + "','" + Reader.GetValue(4) + "','" + Reader.GetValue(5).ToString() + "','" + Reader.GetValue(6) + "','" + Reader.GetValue(7).ToString() + "','" + Reader.GetValue(8) + "')";

                sqLiteCommand = new SQLiteCommand(query, sqLiteConnection);

                i = i + sqLiteCommand.ExecuteNonQuery();

            }
            Reader.Close();
        }


    }
}
