using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace Studenti.DAL.DAL.v2
{
    public class BaseDAL
    {
        public static string DataPath
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data/DataSource.xml");
            }
        }
        
        
       
    }
}
