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
    public class FacultyDAL 
    {

        public  List<Faculty> GetFaculties()
        {
            List<Faculty> faculties = new List<Faculty>();

            XmlDocument doc = new XmlDocument();

            string pathData = BaseDAL.DataPath;

            doc.LoadXml(File.ReadAllText(pathData));

            foreach (XmlNode nd in doc.DocumentElement.ChildNodes)
            {
                var faculty = new Faculty
                {
                    Id = int.Parse(nd.Attributes["Id"].Value),
                    Name = nd.Attributes["Name"].Value,
                    FoundingYear = int.Parse(nd.Attributes["FoundingYear"].Value),
                    Description = nd.InnerText.Trim()
                };
                faculties.Add(faculty);
            }

            #region old code
            /*this.Faculties.Add(new Faculty
            {
                Id = 1,
                Name = "Cibernetica Economica",
                FoundingYear = 1980,
                Description = "Faculatea de economie"

            });

            this.Faculties.Add(new Faculty
            {
                Id = 2,
                Name = "Informatica Economica",
                FoundingYear = 1985,
                Description = "Faculate de informatica si economie"

            });

            this.Faculties.Add(new Faculty
            {
                Id = 3,
                Name = "Statistica",
                FoundingYear = 1990,
                Description = "Faculatea de statistica"

            });*/
            #endregion

            return faculties;
        }

        public void Delete(int facultyId)
        {
            XmlDocument doc = LoadData();

            //string pathData = BaseDAL.DataPath(Application.StartupPath, @"Data\Faculties.xml");

            //doc.SelectNodes(pathData);

            XmlNode nd = doc.SelectSingleNode(@"/Faculties/Faculty[@Id=" + facultyId + "]");

            nd.ParentNode.RemoveChild(nd);

            //doc.SelectSingleNode();
            /// stergere Xpath

            //foreach (XmlNode nd in doc.DocumentElement.ChildNodes)
            //{
            //    if (int.Parse(nd.Attributes["Id"].Value) == facultyId)
            //    {
            //        nd.ParentNode.RemoveChild(nd);
            //    }
            //}


            SaveData(doc);

        }

        private void SaveData(XmlDocument doc)
        {
            string pathData = BaseDAL.DataPath;
            File.WriteAllText(pathData, doc.InnerXml);

        }
        private XmlDocument LoadData()
        {
            XmlDocument doc = new XmlDocument();

            string pathData = BaseDAL.DataPath;

            doc.LoadXml(File.ReadAllText(pathData));

            return doc;

        }
        public void Add(Faculty faculty)
        {
            XmlDocument doc = LoadData();


            XmlNode newNode = doc.CreateNode(XmlNodeType.Element, "Faculty", "");
            
            List<Faculty> allFaculties = GetFaculties();


            var attrName = doc.CreateAttribute("Name");
            attrName.Value = faculty.Name;
            newNode.Attributes.Append(attrName);

            var attrFoundingYear = doc.CreateAttribute("FoundingYear");
            attrFoundingYear.Value = faculty.FoundingYear.ToString();
            newNode.Attributes.Append(attrFoundingYear);

            var attrId = doc.CreateAttribute("Id");
            attrId.Value = (allFaculties.Max(x => x.Id) + 1).ToString();
            newNode.Attributes.Append(attrId);


            newNode.InnerText= faculty.Description;

            doc.DocumentElement.AppendChild(newNode);
            SaveData(doc);
        }

        public void Save(Faculty faculty)
        {
            throw new NotImplementedException();
        }
    }
}
