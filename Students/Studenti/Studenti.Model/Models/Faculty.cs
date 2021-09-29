using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studenti.Models
{
    public class Faculty : BaseEntity
    {
        public string Name { get; set; }
        public int FoundingYear { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return this.Name;
        }

    }
}
