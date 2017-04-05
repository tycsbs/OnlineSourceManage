using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modle
{
   [Serializable]
    public class ChapterSource
    {
        public ChapterSource()
        {
            isDel = 0;
        }

        public string timeStamp { set; get; }       
      
        public int chId { set; get; }
        public int id { set; get; } 

        public string chName { set; get; }

        public string srcUrl { set; get; }

        public string srcType { set; get; }

        public int? isDel { set; get; }

       
    }
}
