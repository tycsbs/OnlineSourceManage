using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modle;

namespace BLL
{
    public class EduProcessBll
    {
        #region 关系转对象


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public EduProcess DataRowToModel(DataRow row)
        {
            EduProcess model = new EduProcess();
            if (row != null)
            {
                if (row["sId"] != null && row["sId"].ToString() != "")
                {
                    model.sId = int.Parse(row["sId"].ToString());
                }
                if (row["uId"] != null && row["uId"].ToString() != "")
                {
                    model.uId = int.Parse(row["uId"].ToString());
                }
                if (row["cId"] != null && row["cId"].ToString() != "")
                {
                    model.cId = int.Parse(row["cId"].ToString());
                }
                if (row["chId"] != null && row["chId"].ToString() != "")
                {
                    model.chId = int.Parse(row["chId"].ToString());
                }
            }
            return model;
        }
        #endregion
    }
}
