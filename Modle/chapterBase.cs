using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modle
{
    public class chapterBase
    {
        public chapterBase()
        {
            isDel = 0;
        }

        public int chId { get; set; }
       
        public int cId { set; get; }

        public string chName { set; get; }

        public string starttime { set; get; }

        public string mark { set; get; }
        
        public int? isDel { set; get; }
    }
}
