using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using Studenti.DAL.DAL.v2;
using Studenti.DAL.Interfaces;
using Studenti.Models;

namespace Studenti.DAL.DAL
{
    public class StudentDAL
    {

        public List<Student> GetStudents()
        {

            List<Student> students = new List<Student>();

            XmlDocument doc = new XmlDocument();

            string pathData = BaseDAL.DataPath;

            doc.LoadXml(File.ReadAllText(pathData));

            foreach (XmlNode nd in doc.DocumentElement.ChildNodes)
            {
                var student = new Student
                {
                    Id = int.Parse(nd.Attributes["Id"].Value),
                    FirstName = nd.Attributes["FirstName"].Value,
                    LastName = nd.Attributes["LastName"].Value,
                    //Age = int.Parse(nd.Attributes["Age"].Value),
                    Address = nd.Attributes["Address"].Value,
                    Sex = nd.Attributes["Sex"].Value,
                    Description = nd.InnerText.Trim(),
                    DateofBirth = DateTime.Parse(nd.Attributes["DateofBirth"].Value)
                };
                students.Add(student);
            }


            #region old code
            //students.Add(new Student
            //{
            //    Id = 1,
            //    FirstName = "Alin",
            //    LastName = "Jinga",
            //    Age = 24,
            //    Address = "Strada Liniei",
            //    Sex = "M"
            //});

            //students.Add(new Student
            //{
            //    Id = 2,
            //    FirstName = "Ionel",
            //    LastName = "Popescu",
            //    Age = 23,
            //    Address = "Bld Iuliu Maniu",
            //    Sex = "M"
            //});

            //students.Add(new Student
            //{
            //    Id = 3,
            //    FirstName = "Alex",
            //    LastName = "Ionescu",
            //    Age = 25,
            //    Address = "Bld Timisoara",
            //    Sex = "M"
            //});

            //students.Add(new Student
            //{
            //    Id = 1,
            //    FirstName = "Alin",
            //    LastName = "Jinga",
            //    Age = 24,
            //    Address = "Strada Liniei",
            //    Sex = "M"
            //});

            //students.Add(new Student
            //{
            //    Id = 2,
            //    FirstName = "Ionel",
            //    LastName = "Popescu",
            //    Age = 23,
            //    Address = "Bld Iuliu Maniu",
            //    Sex = "M"
            //});

            //students.Add(new Student
            //{
            //    Id = 3,
            //    FirstName = "Alex",
            //    LastName = "Ionescu",
            //    Age = 25,
            //    Address = "Bld Timisoara",
            //    Sex = "M"
            //});
            #endregion

            return students;
        }
        private static void SaveData(XmlDocument doc)
        {
            string pathData = BaseDAL.DataPath;
            File.WriteAllText(pathData, doc.InnerXml);

        }

        private static XmlDocument LoadData()
        {
            XmlDocument doc = new XmlDocument();

            string pathData = BaseDAL.DataPath;

            doc.LoadXml(File.ReadAllText(pathData));

            return doc;

        }

        public void Add(Student student)
        {
            XmlDocument doc = LoadData();
            XmlNode newNode = doc.CreateNode(XmlNodeType.Element, "Student", "");
            List<Student> allstudents = GetStudents();


            var attrFirstName = doc.CreateAttribute("FirstName");
            attrFirstName.Value = student.FirstName;
            newNode.Attributes.Append(attrFirstName);

            var attrLastName = doc.CreateAttribute("LastName");
            attrLastName.Value = student.LastName;
            newNode.Attributes.Append(attrLastName);

            var attrAddress = doc.CreateAttribute("Address");
            attrAddress.Value = student.Address;
            newNode.Attributes.Append(attrAddress);

            var attrSex = doc.CreateAttribute("Sex");
            attrSex.Value = student.Sex;
            newNode.Attributes.Append(attrSex);

            var attrDateofBirth = doc.CreateAttribute("DateofBirth");
            attrDateofBirth.Value = student.DateofBirth.ToString("d");
            newNode.Attributes.Append(attrDateofBirth);

            var attrId = doc.CreateAttribute("Id");
            attrId.Value = (allstudents.Max(x => x.Id) + 1).ToString();
            newNode.Attributes.Append(attrId);

            newNode.InnerText = student.Description;

            doc.DocumentElement.AppendChild(newNode);
            SaveData(doc);


        }

        public  void Delete(int studentId)
        {


            XmlDocument doc = LoadData();


            //doc.SelectNodes(pathData);
            XmlNode nd = doc.SelectSingleNode(@"/Students/Student[@Id=" + studentId + "]");
            nd.ParentNode.RemoveChild(nd);

            // doc.SelectSingleNode();
            /// stergere Xpath
            //foreach (XmlNode nd in doc.DocumentElement.ChildNodes)
            //{
            //    if (int.Parse(nd.Attributes["Id"].Value) == studentId)
            //    {
            //        nd.ParentNode.RemoveChild(nd);
            //    }
            //}


            SaveData(doc);

        }
    }
}
