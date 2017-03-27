using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modle
{
    [Serializable]
    public class Chapter
    {
        public Chapter()
        { }
        #region Model
        private int _chid;
        private int _cid;
        private string _chname;
        private DateTime? _starttime = DateTime.Now;
        private string _mark;
        private int? _isdel = 0;
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
        public int cId
        {
            set { _cid = value; }
            get { return _cid; }
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
        public DateTime? starttime
        {
            set { _starttime = value; }
            get { return _starttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string mark
        {
            set { _mark = value; }
            get { return _mark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? isDel
        {
            set { _isdel = value; }
            get { return _isdel; }
        }
        #endregion Model

    }
}
