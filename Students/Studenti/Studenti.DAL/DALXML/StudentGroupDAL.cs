using Studenti.DAL.DAL.v2;
using Studenti.DAL.Interfaces;
using Studenti.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using System.Xml;

namespace Studenti.DAL.DAL
{
    public class StudentGroupDAL
    {
        public static List<StudentGroup> GetStudentGroups()
        {
            List<StudentGroup> studentGroups = new List<StudentGroup>();

            XmlDocument doc = new XmlDocument();

            string pathData = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            doc.LoadXml(File.ReadAllText(pathData));

            foreach (XmlNode nd in doc.DocumentElement.ChildNodes)
            {
                var studentgroup = new StudentGroup
                {
                    Id = int.Parse(nd.Attributes["Id"].Value),
                    Name = nd.Attributes["Name"].Value,
                    Faculty = nd.Attributes["Faculty"].Value,
                    Year = int.Parse(nd.Attributes["Year"].Value),
                    Description = nd.InnerText.Trim()


                };

                studentGroups.Add(studentgroup);
            }

            return studentGroups;

            #region old code
            /*this.StudentGroups.Add(new StudentGroup
            {
                Id = 1,
                Name = "1360",
                Faculty = "Cibernetica",
                Description = "Grupa defavorizata"
            });

            this.StudentGroups.Add(new StudentGroup
            {
                Id = 2,
                Name = "1400",
                Faculty = "Cibernetica",
                Description = "Grupa de olimpici"
            });

            this.StudentGroups.Add(new StudentGroup
            {
                Id = 3,
                Name = "1450",
                Faculty = "Cibernetica",
                Description = "Grupa de Informatica"
            });*/
            #endregion

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

        public void Delete(int stgroupId)
        {


            XmlDocument doc = LoadData();


            //doc.SelectNodes(pathData);



            XmlNode nd = doc.SelectSingleNode(@"/Data/StudentGroups/StudentGroup[@Id=" + stgroupId+ "]");

            nd.ParentNode.RemoveChild(nd);
            /// stergere Xpath
            //foreach (XmlNode nd in doc.DocumentElement.ChildNodes)
            //{
            //    if (int.Parse(nd.Attributes["Id"].Value) == stgroupId)
            //    {
            //       
            //    }
            //}


            SaveData(doc);

        }

        public void Add(StudentGroup studentGroup)
        {
            XmlDocument doc = LoadData();


            XmlNode newNode = doc.CreateNode(XmlNodeType.Element, "StudentGroup", "");

            List<StudentGroup> allStudentGroups = GetStudentGroups();

            var attrId = doc.CreateAttribute("Id");
            attrId.Value = (allStudentGroups.Max(x => x.Id) + 1).ToString();
            newNode.Attributes.Append(attrId);

            var attrName = doc.CreateAttribute("Name");
            attrName.Value = studentGroup.Name.ToString();
            newNode.Attributes.Append(attrName);

            var attrFaculty = doc.CreateAttribute("Faculty");
            attrFaculty.Value = "Cibernetica Economica";
            newNode.Attributes.Append(attrFaculty);

            var attrYear = doc.CreateAttribute("Year");
            attrYear.Value = studentGroup.Year.ToString();
            newNode.Attributes.Append(attrYear);

            newNode.InnerText = studentGroup.Description;
            doc.DocumentElement.AppendChild(newNode);
            SaveData(doc);


        }




    }

}
