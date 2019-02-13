using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseManagementLibrary.DAL
{
    public class DepartmentAdminDAL
    {
        //Add new course
        //INSERT INTO courses (dept_name, course_name, section_num, credit_hours, seats_max, location, semester_id) VALUES (@dept_name, @course_name, @section_num, @credit_hours, @seats_max, @location, @semester_id)

        //Delete course
        //DELETE FROM courses WHERE CRN = @CRN;

        //Edit course
        //UPDATE courses SET (course_name=@course_name, section_num=@section_num, credit_hours=@credit_hours, seats_max=@seats_max, location=@location, semester_id=@semester_id) WHERE CRN = @CRN;

        //Assign teacher to course
        //INSERT INTO teacher_teaches_courses (teacher_uid, courses_CRN) VALUES (@teacher_UID, @CRN)
    }
}