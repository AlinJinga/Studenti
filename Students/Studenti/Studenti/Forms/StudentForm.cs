using Studenti.DAL.DALSQL;
using Studenti.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Studenti
{
    public partial class StudentForm : Form
    {
        public StudentForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets the saved student
        /// </summary>
        /// <returns></returns>
        /// 
        private int Id;

        private List<StudentGroup> studentgroups { get; set; }
        private List<Faculty> faculties { get; set; }
        public Student GetSavedStudent()
        {
            return new Student
            {
                FirstName = this.txtFirstName.Text,
                LastName = this.txtLastName.Text,
                Address = this.txtAddress.Text,
                //Age = int.Parse(this.txtAge.Text),
                DateofBirth = this.dtDateofBirth.Value,
                Sex = this.cmbSex.SelectedItem.ToString(),
                Description = this.txtDescription.Text,
                Id = this.Id,
                StudentGroupId = ((StudentGroup)cmbGroups.SelectedItem).Id


            };


        }

        public void SetSavedStudent(Student student)
        {
            this.txtFirstName.Text = student.FirstName;
            this.txtLastName.Text = student.LastName;
            this.txtAddress.Text = student.Address;
            this.dtDateofBirth.Value = student.DateofBirth;
            this.Id = student.Id;
            this.txtDescription.Text = student.Description;

            this.Text = "Edit Student";
            //var selectedGroup = this.studentgroups.Find(x => x.Id == student.StudentGroupId);
            //this.cmbGroups.SelectedIndex = this.studentgroups.IndexOf(selectedGroup);


        }

        private bool ValidateForm()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(this.txtLastName.Text))
            {
                isValid = false;
                this.eP.SetError(this.txtLastName, "Last Name is required.");
            }
            else

            {
                this.eP.SetError(this.txtLastName, string.Empty);
            }

            if (string.IsNullOrEmpty(this.txtFirstName.Text))
            {
                isValid = false;
                this.eP.SetError(this.txtFirstName, "First Name is required.");
            }
            else

            {
                this.eP.SetError(this.txtFirstName, string.Empty);
            }

            if (string.IsNullOrEmpty(this.txtAddress.Text))
            {
                isValid = false;
                this.eP.SetError(this.txtAddress, "Address is required.");
            }
            else

            {
                this.eP.SetError(this.txtAddress, string.Empty);
            }
            if (string.IsNullOrEmpty(this.txtDescription.Text))
            {
                isValid = false;
                this.eP.SetError(this.txtDescription, "Description is required.");
            }
            else
            {
                this.eP.SetError(this.txtDescription, string.Empty);
            }

            return isValid;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {

            if (this.ValidateForm())
            {
                this.DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void StudentForm_Load(object sender, EventArgs e)
        {
            this.cmbSex.SelectedIndex = 0;

            this.cmbFaculties.DataSource = new FacultySQL().GetFaculties();
            this.cmbFaculties.SelectedIndex = 0;
            this.cmbGroups.DataSource = studentgroups = new StudentGroupSQL().GetStudentGroups();
            this.cmbGroups.SelectedIndex = 0;
        }

        public string[] GetGroupById(int id)
        {
            return studentgroups.Where(x => x.FacultyId == id).Select(l => l.Name).ToArray();
        }
        private void lblFirstName_Click(object sender, EventArgs e)
        {

        }

        private void txtAge_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblAge_Click(object sender, EventArgs e)
        {

        }

        private void dtDateofBirth_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtExtraInfo_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbFaculties_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (studentgroups != null)
            {
               // cmbGroups.Items.Clear();
                int id = ((Faculty)cmbFaculties.SelectedItem).Id;

                cmbGroups.DataSource = studentgroups.FindAll(x => x.FacultyId == id);
            }
            //foreach (string groupName in GetGroupById(id))
            //{
            //    this.cmbGroups.Items.Add(groupName);
            //}
        }

        private void cmbSex_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
