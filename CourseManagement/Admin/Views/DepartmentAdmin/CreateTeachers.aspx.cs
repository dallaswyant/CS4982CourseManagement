using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Admin.DAL;
using Admin.Models;
using Admin.Utilities;

namespace Admin.Views.DepartmentAdmin
{
    public partial class CreateTeachers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBind();
            }
            this.states.DataSource = Locations.GetStates();
            this.countries.DataSource = Locations.GetCountries();
            this.sex.DataSource = new List<String>() {
                "Male",
                "Female"
            };
            this.race.DataSource = new List<String>() {
                "African American",
                "White",
                "Asian",
                "Hispanic",
                "Latino",
                "Pacific Islander",
                "Indian",
                "Other"
            };

            DegreeProgramDAL dal = new DegreeProgramDAL();
            this.department.DataSource = dal.GetAllDegreePrograms();
        }

        protected void createTeacher_Click(object sender, EventArgs e)
        {
            var addressLine1 = this.addressLine1.Text;
            var addressLine2 = this.addressLine2.Text;
            var zip = Int32.Parse(this.zip.Text);
            var city = this.city.Text;
            var state = this.states.SelectedValue;
            var country = this.countries.SelectedValue;
            Address studentAddress = new Address(addressLine1, addressLine2, city, zip, state, country);
            AdminDAL adminDal = new AdminDAL();
            int id = unchecked((int)adminDal.CreateAddress(studentAddress));

            var firstName = this.fname.Text;
            var lastName = this.lastname.Text;
            char middleInitial = this.middleInital.Text[0];
            var email = this.email.Text;
            var phone = this.phone.Text;
            var race = this.race.SelectedValue;
            var sex = this.sex.SelectedValue;
            var ssn = this.ssn.Text;
            var dob = DateTime.Parse(this.birthday.Text);
            PersonalStuff info = new PersonalStuff(firstName, middleInitial, lastName, id, phone, sex, dob, race, email, ssn);
            Teacher teacher = new Teacher(this.officeLocation.Text, email, this.isEmailPublic.Checked, phone, this.teacherID.Text);

            adminDal.CreateTeacher(info, teacher, this.department.SelectedValue, this.officeHours.Text);
            this.confirmation.Text = "Student created successfully.";
        }
    }
}