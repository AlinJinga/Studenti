using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studenti.Models
{
    /// <summary>
    /// Student model
    /// </summary>
    public class Student: BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age
        {   
            get
            {

                return (int)Math.Floor(DateTime.Now.Subtract(this.DateofBirth).TotalDays/365);


            }
        }
        public string Sex { get; set; }
        public string Address { get; set; }
  

        public DateTime DateofBirth { get; set; }
        public string Description
        {
            get
            {
                return description;

            }
            set
            {

                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("description must have value.");
                }
                description = value;
            }
        }

        private string description;

        public int StudentGroupId { get; set; }
    }
}
