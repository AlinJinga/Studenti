using Studenti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studenti.DAL.Interfaces
{
    public interface IStudentGroupDAL
    {
        List<StudentGroup> GetStudentGroups();
        void Add(StudentGroup studentGroup);
        void Update(StudentGroup studentGroup);
        bool Delete(int stgroupId);
    }

}
