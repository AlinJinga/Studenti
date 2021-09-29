using Studenti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Studenti.DAL.Interfaces
{
    public interface IFacultyDAL
    {
        List<Faculty> GetFaculties();

        bool Delete(int facultyId);


        void Add(Faculty faculty);


        void Update(Faculty faculty);

    }
}
