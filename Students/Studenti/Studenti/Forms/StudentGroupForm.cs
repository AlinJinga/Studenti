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

namespace Studenti.Forms
{
    public partial class StudentGroupForm : Form
    {
        public StudentGroupForm()
        {
            InitializeComponent();

            this.cmbFaculties.DataSource = faculties = new FacultySQL().GetFaculties();
            this.cmbFaculties.SelectedIndex = 0;
        }

        private int Id;
        List<Faculty> faculties = new List<Faculty>();
        private void GroupForm_Load(object sender, EventArgs e)
        {


            


        }

        private void nudYear_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            if (this.ValidateForm())
            {
                this.DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        public StudentGroup GetSavedStudentGroup()
        {
            return new StudentGroup
            {
                Name = this.txtName.Text,
                Description = this.txtDescription.Text,
                Faculty = this.lblFaculty.Text,
                Year = (int)this.nmYear.Value,
                Id = this.Id,
                FacultyId = ((Faculty)cmbFaculties.SelectedItem).Id

            };
        }

        public void SetSavedStudentGroup(StudentGroup studentGroup)
        {
            this.txtName.Text = studentGroup.Name;
            this.txtDescription.Text = studentGroup.Description;
            //this.lblFaculty.Text = studentgroup.Faculty;
            this.Id = studentGroup.Id;
            this.Text = "Edit student group";



            var selectedFaculty = this.faculties.Find(x => x.Id == studentGroup.FacultyId);
            this.cmbFaculties.SelectedIndex = this.faculties.IndexOf(selectedFaculty);
        }

        private bool ValidateForm()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(this.txtName.Text))
            {
                isValid = false;
                this.eP.SetError(this.txtName, "Name is required.");
            }
            else

            {
                this.eP.SetError(this.txtName, string.Empty);
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

        private void btnInfo_Click(object sender, EventArgs e)
        {

            if (this.ValidateForm())
            {
                this.DialogResult = DialogResult.OK;
                Close();
            }




        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void cmbFaculties_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
