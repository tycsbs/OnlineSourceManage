using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modle
{
    [Serializable]
    public class Users
    {

        public Users()
        {
            isDel = 0;
            useable = 0;
            regTime = DateTime.Now.ToString("yy-MM-dd");
        }

        public int uId { set; get; }

        public string uName { set; get; }

        public string pwd { set; get; }

        public int role { set; get; }

        public int? sex { set; get; }

        public string home { set; get; }

        public string regTime { set; get; }

        public int? isDel { set; get; }

        public int useable { get; set;  }
    }
}
