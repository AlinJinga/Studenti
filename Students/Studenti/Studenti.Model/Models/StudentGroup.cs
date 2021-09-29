using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studenti.Models
{
    public class StudentGroup: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Faculty { get; set; }
        public int Year { get; set; }

        public int FacultyId { get; set; }

        public override string ToString()
        {
            return this.Name;
        }

    }
}
