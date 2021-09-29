using Studenti.DAL;
using Studenti.DAL.DAL;
using Studenti.DAL.DALSQL;
using Studenti.DAL.Interfaces;
using Studenti.Forms;
using Studenti.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Studenti
{
    public partial class MainForm : Form
    {
        private List<Student> Students { get; set; }
        private List<StudentGroup> StudentGroups { get; set; }
        private List<Faculty> Faculties { get; set; }
        private IFacultyDAL FacultyDAL;
        private IStudentDAL StudentDAL;
        private IStudentGroupDAL StudentGroupDAL;
        private bool SingleDataSource { get; set; }

        private IFacultyDAL dalFaculty;
        private IStudentDAL dalStudent;
        private IStudentGroupDAL dalStudentGroup;

        public MainForm()
        {
            InitializeComponent();
            Students = new List<Student>();
            StudentGroups = new List<StudentGroup>();
            Faculties = new List<Faculty>();

            SingleDataSource = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["SingleDataSource"].ToString().ToLower());

            dalFaculty = new FacultySQL();
            dalStudent = new StudentSQL();

            //new StudentDALv2();
            dalStudentGroup = new StudentGroupSQL();
        }

        private void btnAdauga_Click(object sender, EventArgs e)
        {
            AddStudent();
        }



        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadStudentsGrid();
            LoadGroupsGrid();
            LoadFacultyGrid();

           //bool exitsGroups = Students.Exists(x => x.StudentGroupId == 2);


        }



        private void dgGrupa_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            AddStudentGroup();
        }

        private void LoadFacultyGrid()
        {
            this.dgFaculty.AutoGenerateColumns = false;
            if (SingleDataSource)
            {
                Faculties = dalFaculty.GetFaculties();


            }
            else
            {
                Faculties = FacultyDAL.GetFaculties();

            }

            this.dgFaculty.DataSource = Faculties;

        }

        private void LoadGroupsGrid()
        {
            this.dgStudentGroup.AutoGenerateColumns = false;
            if (SingleDataSource)
            {
                StudentGroups = dalStudentGroup.GetStudentGroups();
            }
            else
            {
                StudentGroups = StudentGroupDAL.GetStudentGroups();
            }
            this.dgStudentGroup.DataSource = StudentGroups;
        }

        private void LoadStudentsGrid()
        {
            this.dgStudents.AutoGenerateColumns = false;
            if (SingleDataSource)
            {
                Students = dalStudent.GetStudents();


            }
            else
            {
                Students = StudentDAL.GetStudents();

            }

            this.dgStudents.DataSource = Students;
        }

        private void btnAddFaculty_Click(object sender, EventArgs e)
        {
            AddFaculty();
        }

        private void dgStudents_MouseClick(object sender, MouseEventArgs e)
        {
            ShowMenu(dgStudents, e, EntityType.Student);

        }

        private void ShowMenu(DataGridView dg, MouseEventArgs e, EntityType entityType)
        {
            int currentMouseOverRow = dg.HitTest(e.X, e.Y).RowIndex;


            if (e.Button == MouseButtons.Right)
            {
                if (currentMouseOverRow >= 0)
                {
                    tsmEdit.Visible = true;
                    tsmDelete.Visible = true;


                    // cmStudent.Add(new MenuItem(string.Format("Do something to row {0}", currentMouseOverRow.ToString())));
                }

                else

                {
                    tsmEdit.Visible = false;
                    tsmDelete.Visible = false;
                }

                tsmAdd.Tag = entityType;



                cm.Show(dg, new Point(e.X, e.Y));

                if (currentMouseOverRow > -1)
                {
                    cm.Tag = dg.Rows[currentMouseOverRow].DataBoundItem;
                }
            }
        }

        private void RefreshFaculty()
        {

            this.dgFaculty.DataSource = null;
            this.dgFaculty.DataSource = Faculties;
            this.dgFaculty.Refresh();
        }

        private void RefreshStudents()
        {
            this.dgStudents.DataSource = null;
            this.dgStudents.DataSource = Students;
            this.dgStudents.Refresh();
        }

        private void RefreshStudentGroups()
        {
            this.dgStudentGroup.DataSource = null;
            this.dgStudentGroup.DataSource = StudentGroups;
            this.dgStudentGroup.Refresh();
        }

        private void AddFaculty()
        {
            FacultyForm frm = new FacultyForm();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                dalFaculty.Add(frm.GetSavedFaculty());
                Faculties = dalFaculty.GetFaculties();

                RefreshFaculty();
            }
        }

        private void AddStudent()
        {
            StudentForm frm1 = new StudentForm();


            if (frm1.ShowDialog() == DialogResult.OK)
            {

                dalStudent.Add(frm1.GetSavedStudent());
                Students = dalStudent.GetStudents();

                RefreshStudents();
            }
        }

        private void AddStudentGroup()
        {
            StudentGroupForm frm2 = new StudentGroupForm();
            if (frm2.ShowDialog() == DialogResult.OK)
            {
                dalStudentGroup.Add(frm2.GetSavedStudentGroup());
                StudentGroups = dalStudentGroup.GetStudentGroups();

                RefreshStudentGroups();
            }
        }
        private void tsmAdd_Click(object sender, EventArgs e)
        {
            EntityType entType = (EntityType)tsmAdd.Tag;


            switch (entType)
            {
                case EntityType.Faculty:

                    AddFaculty();

                    break;

                case EntityType.Student:

                    AddStudent();

                    break;

                case EntityType.StudentGroup:

                    AddStudentGroup();


                    break;
            }
        }

        private void tsmEdit_Click(object sender, EventArgs e)
        {
            //  EntityType entType = (EntityType)cm.Tag;

            object editedEntity = cm.Tag;

            if (editedEntity is Faculty)
            {
                FacultyForm frm = new FacultyForm();
                frm.SetSavedFaculty(editedEntity as Faculty);

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    dalFaculty.Update(frm.GetSavedFaculty());
                    Faculties = dalFaculty.GetFaculties();
                    this.dgFaculty.DataSource = null;
                    this.dgFaculty.DataSource = Faculties;
                    this.dgFaculty.Refresh();

                }
            }

            if (editedEntity is Student)
            {
                StudentForm frm = new StudentForm();
                frm.SetSavedStudent(editedEntity as Student);

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    // FacultyDAL.
                    dalStudent.Update(frm.GetSavedStudent());
                    Students = dalStudent.GetStudents();
                    this.dgStudents.DataSource = null;
                    this.dgStudents.DataSource = Students;
                    this.dgStudents.Refresh();
                }
            }

            if (editedEntity is StudentGroup)
            {
                StudentGroupForm frm = new StudentGroupForm();
                frm.SetSavedStudentGroup(editedEntity as StudentGroup);

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    // FacultyDAL.
                    dalStudentGroup.Update(frm.GetSavedStudentGroup());
                    StudentGroups = dalStudentGroup.GetStudentGroups();
                    this.dgStudentGroup.DataSource = null;
                    this.dgStudentGroup.DataSource = StudentGroups;
                }
            }





            // misssing for the others
        }



        private void dgFaculty_MouseClick(object sender, MouseEventArgs e)
        {
            ShowMenu(dgFaculty, e, EntityType.Faculty);
        }

        private void dgStudentGroup_MouseClick(object sender, MouseEventArgs e)
        {
            ShowMenu(dgStudentGroup, e, EntityType.StudentGroup);
        }


        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Student st = cm.Tag as Student;
            Faculty faculty = cm.Tag as Faculty;
            StudentGroup studentGroup = cm.Tag as StudentGroup;
            //bool exitsGroups = Students.Exists(x => x.StudentGroupId ==studentGroup.Id);
            DialogResult dialogResult = MessageBox.Show(Constants.DeleteMessage, Constants.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                if (st != null)
                {
                    dalStudent.Delete(st.Id);
                    LoadStudentsGrid();
                }

                if (faculty != null)
                {
                    if (dalFaculty.Delete(faculty.Id))
                    {
                        LoadFacultyGrid();
                    }
                    else
                    {
                        MessageBox.Show("Avem grupe");
                    }
                    
                }

                if (studentGroup != null)
                {
                    if (dalStudentGroup.Delete(studentGroup.Id))
                    {
                        LoadGroupsGrid();
                    }
                    else
                    {
                        MessageBox.Show("Avem studenti");
                    }
                }
              
            }


        }
    }
}
