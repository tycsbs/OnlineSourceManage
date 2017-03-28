using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EduProcessDal
    {
        /// <summary>
        /// 获取学生自己的学习进度
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetUserStudyProcess(int id)
        {
            string sql = string.Format(
                    "SELECT [dbo].[User].uName,[dbo].[Course].cName,[dbo].[Chapter].chNameFROM [dbo].[eduState] JOIN [dbo].[Course] ON dbo.eduState.cId = dbo.Course.cIdJOIN [dbo].[Chapter] ON dbo.Course.cId = dbo.Chapter.cIdJOIN [dbo].[User] ON dbo.eduState.uId = [dbo].[User].uIdWHERE [dbo].[User].uId = '{0}'", id);
            return SqlHelper.SqlDataTable(sql);
        }
    }
}
