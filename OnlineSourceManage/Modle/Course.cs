using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modle
{
    public class Course
    {
        public Course()
        {
            isDel = 0;
        }
        
        public int cId { set; get; }

        public string cName { set; get; }

        public int? levelNum { set; get; }

        public string startTime { set; get; }

        public int isDel { set; get; }

        public string mark { set; get; }

        public string types { set; get; }   
    }
}
