using Studenti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studenti.DAL.Interfaces
{
    public interface IStudentDAL
    {
        List<Student> GetStudents();

        void Add(Student student);

        void Delete(int studentId);

        void Update (Student student);
    }
}
