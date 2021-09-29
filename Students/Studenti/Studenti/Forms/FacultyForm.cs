using System;
using Studenti.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Studenti.Forms
{
    public partial class FacultyForm : Form
    {
        public FacultyForm()
        {
            InitializeComponent();
            
        }

        private int Id;
        private void txtDescription_TextChanged(object sender, EventArgs e)
        {

        }

        private void FacultyForm_Load(object sender, EventArgs e)
        {

        }

        public Faculty GetSavedFaculty()
        {
            return new Faculty
            {
                Name = this.txtName.Text,
                FoundingYear = int.Parse(this.txtFoundingYear.Text),
                Description = this.txtDescription.Text,
                Id = this.Id


            };

            
        }


        public void SetSavedFaculty(Faculty faculty)
        {
            this.txtName.Text = faculty.Name;
            this.txtFoundingYear.Text = faculty.FoundingYear.ToString();
            this.txtDescription.Text = faculty.Description;
            this.Id = faculty.Id;
            this.Text = "Edit Faculty";

            



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

            if (string.IsNullOrEmpty(this.txtFoundingYear.Text))
            {
                isValid = false;
                this.eP.SetError(this.txtFoundingYear, "Founding year is required.");
            }
            else
            {

                int number;
                bool success = int.TryParse(this.txtFoundingYear.Text, out number);
                int year = (int)(DateTime.Now.Year);
                       
                
                if (success && number > 1950 && number < year)
                {
                    this.eP.SetError(this.txtFoundingYear, string.Empty);
                }
                else
                {
                    isValid = false;
                    this.eP.SetError(this.txtFoundingYear, "Founding year must be between 1950 and 2021.");
                }
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

        private void txtFoundingYear_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
