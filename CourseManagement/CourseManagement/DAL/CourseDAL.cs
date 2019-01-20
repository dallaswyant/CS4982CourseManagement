using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseManagement.App_Code;
using MySql.Data.MySqlClient;

namespace CourseManagement.DAL
{
    public class CourseDAL
    {
        /// <summary>
        /// Gets the person by identifier.
        /// </summary>
        /// <param name="personIDCheck">The person identifier check.</param>
        /// <returns>a person with the matching personID</returns>
        public CourseCollection GetCourseByTeacherID(int teacherIDCheck)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            CourseCollection coursesTaught = new CourseCollection();
            using (conn)
            {

                conn.Open();
                GradedItemDAL gradedStuff = new GradedItemDAL();
                
                var selectQuery = "select courses.*, rubrics.assignment_types, rubrics.weight_per_type from courses, teacher_teaches_courses, rubrics WHERE teacher_teaches_courses.courses_CRN = courses.CRN AND courses.rubric_id = rubrics.rubric_id AND teacher_teaches_courses.teacher_id = @teacherID";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@teacherID", teacherIDCheck);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        
                        int CRNOrdinal = reader.GetOrdinal("CRN");
                        int courseNameOrdinal = reader.GetOrdinal("course_name");
                        int sectionNumberOrdinal = reader.GetOrdinal("section_num");
                        int creditHoursOrdinal = reader.GetOrdinal("credit_hours");
                        int maxSeatsOrdinal = reader.GetOrdinal("seats_max");
                        int locationOrdinal = reader.GetOrdinal("location");
                        int assignmentTypesOrdinal = reader.GetOrdinal("assignment_types");
                        int weightPerTypeOrdinal = reader.GetOrdinal("weight_per_type");

                        while (reader.Read())
                        {
                            int CRN = reader[CRNOrdinal] == DBNull.Value ? default(int) : reader.GetInt32(CRNOrdinal);
                            string courseName = reader[courseNameOrdinal] == DBNull.Value ? default(string) : reader.GetString(courseNameOrdinal);
                            string sectionNumber = reader[sectionNumberOrdinal] == DBNull.Value ? default(string) : reader.GetString(sectionNumberOrdinal);
                            int creditHours = reader[creditHoursOrdinal] == DBNull.Value ? default(int) : reader.GetInt32(creditHoursOrdinal);
                            int maxSeats = reader[maxSeatsOrdinal] == DBNull.Value ? default(int) : reader.GetInt32(maxSeatsOrdinal);
                            string location = reader[locationOrdinal] == DBNull.Value ? default(string) : reader.GetString(locationOrdinal);
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
                                rubricStuff.Add(types[i],Convert.ToInt32(weights[i]));
                            }
                            CourseRubric rubric = new CourseRubric(rubricStuff);
                            List<GradedItem> listOfGrades = gradedStuff.GetGradedItemsByCRN(CRN);
                            TeacherDAL teacherGetter = new TeacherDAL();
                            Teacher currTeacher = teacherGetter.GetAllTeachers();
                            /*
                            CourseInfo currCourseInfo = new CourseInfo(courseName, );
                            Course currentCourse = new Course(listOfGrades,);
                            Address currAddress = new Address(addr1, addr2, city, state, zipcode, country);
                            Person currPerson = new Person(personID, fname, minit, lname, ssn, gender, phoneNumber, bdate, currAddress);
                            
                            return currPerson;
                            */
                        }
                    }
                }
                conn.Close();
            }

            return null;
        }

        /// <summary>
        /// Gets the person by identifier.
        /// </summary>
        /// <param name="personIDCheck">The person identifier check.</param>
        /// <returns>a person with the matching personID</returns>
        public CourseCollection GetCoursesByStudentID(int studentIDCheck)
        {
            MySqlConnection conn = DbConnection.GetConnection();
            CourseCollection coursesTaught = new CourseCollection();
            using (conn)
            {

                conn.Open();
                var selectQuery = "select courses.* from courses, students, student_has_courses WHERE students.student_id = student_has_courses.student_id AND student_has_courses.courses_CRN = courses.CRN AND students.student_id = @studentID";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@studentID", studentIDCheck);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int personIdOrdinal = reader.GetOrdinal("personID");
                        int fnameOrdinal = reader.GetOrdinal("fname");
                        int minitOrdinal = reader.GetOrdinal("minit");
                        int lnameOrdinal = reader.GetOrdinal("lname");
                        int ssnOrdinal = reader.GetOrdinal("ssn");
                        int genderOrdinal = reader.GetOrdinal("gender");
                        int bdateOrdinal = reader.GetOrdinal("bdate");
                        int phoneOrdinal = reader.GetOrdinal("phonenumber");
                        int addr1Ordinal = reader.GetOrdinal("addr1");
                        int addr2Ordinal = reader.GetOrdinal("addr2");
                        int cityOrdinal = reader.GetOrdinal("city");
                        int stateOrdinal = reader.GetOrdinal("state");
                        int zipcodeOrdinal = reader.GetOrdinal("zipcode");
                        int countryOrdinal = reader.GetOrdinal("country");

                        while (reader.Read())
                        {
                            /**
                            int personID = reader[personIdOrdinal] == DBNull.Value ? default(int) : reader.GetInt32(personIdOrdinal);
                            string fname = reader[fnameOrdinal] == DBNull.Value ? default(string) : reader.GetString(fnameOrdinal);
                            char minit = reader[minitOrdinal] == DBNull.Value ? default(char) : reader.GetChar(minitOrdinal);
                            string lname = reader[lnameOrdinal] == DBNull.Value ? default(string) : reader.GetString(lnameOrdinal);
                            string ssn = reader[ssnOrdinal] == DBNull.Value ? default(string) : reader.GetString(ssnOrdinal);
                            string gender = reader[genderOrdinal] == DBNull.Value ? default(string) : reader.GetString(genderOrdinal);
                            DateTime bdate = reader[bdateOrdinal] == DBNull.Value ? default(DateTime) : reader.GetDateTime(bdateOrdinal);
                            string addr1 = reader[addr1Ordinal] == DBNull.Value ? default(string) : reader.GetString(addr1Ordinal);
                            string addr2 = reader[addr2Ordinal] == DBNull.Value ? default(string) : reader.GetString(addr2Ordinal);
                            string city = reader[cityOrdinal] == DBNull.Value ? default(string) : reader.GetString(cityOrdinal);
                            string state = reader[stateOrdinal] == DBNull.Value ? default(string) : reader.GetString(stateOrdinal);
                            string zipcode = reader[zipcodeOrdinal] == DBNull.Value ? default(string) : reader.GetString(zipcodeOrdinal);
                            string country = reader[countryOrdinal] == DBNull.Value ? default(string) : reader.GetString(countryOrdinal);
                            string phoneNumber = reader[phoneOrdinal] == DBNull.Value ? default(string) : reader.GetString(phoneOrdinal);
                            Address currAddress = new Address(addr1, addr2, city, state, zipcode, country);
                            Person currPerson = new Person(personID, fname, minit, lname, ssn, gender, phoneNumber, bdate, currAddress);
                            return currPerson;
                            **/

                        }
                    }
                }
                conn.Close();
            }

            return null;
        }
    }
}