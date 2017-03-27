using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modle;

namespace BLL
{
    public class ChapterBll
    {


        #region 关系转对象
        public Chapter DataRowToModel(DataRow row)
        {
            Chapter model = new Chapter();
            if (row != null)
            {
                if (row["chId"] != null && row["chId"].ToString() != "")
                {
                    model.chId = int.Parse(row["chId"].ToString());
                }
                if (row["cId"] != null && row["cId"].ToString() != "")
                {
                    model.cId = int.Parse(row["cId"].ToString());
                }
                if (row["chName"] != null)
                {
                    model.chName = row["chName"].ToString();
                }
                if (row["starttime"] != null)
                {
                    model.starttime = row["starttime"].ToString();
                }
                if (row["mark"] != null)
                {
                    model.mark = row["mark"].ToString();
                }
                if (row["isDel"] != null && row["isDel"].ToString() != "")
                {
                    model.isDel = int.Parse(row["isDel"].ToString());
                }
            }
            return model;
        }
        #endregion
    }
}
