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
        { }
        #region Model
        private int _chid;
        private string _chname;
        private string _srcurl;
        private string _srctype;
        private int? _isdel = 0;
        private DateTime? _timestamp = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        public int chId
        {
            set { _chid = value; }
            get { return _chid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string chName
        {
            set { _chname = value; }
            get { return _chname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string srcUrl
        {
            set { _srcurl = value; }
            get { return _srcurl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string srcType
        {
            set { _srctype = value; }
            get { return _srctype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? isDel
        {
            set { _isdel = value; }
            get { return _isdel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? timeStamp
        {
            set { _timestamp = value; }
            get { return _timestamp; }
        }
        #endregion Model
    }
}
