using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modle;

namespace DAL
{
    public class ChapterDal
    {

        #region 章节增加方法

        public bool AddChapter(Chapter chapter)
        {
            string sql = string.Format("INSERT dbo.Chapter( chName ,mark,cId ) VALUES  ( N'{0}' ,'{1}','{2}')", chapter.chName, chapter.mark, chapter.cId);
            return SqlHelper.ExcuteNonQuery(sql) > 0;
        }

        #endregion

        #region 删除指定章节
        /// <summary>
        /// 根据Id删除章节
        /// </summary>
        /// <param name="chId">章节id</param>
        /// <returns></returns>
        public bool DeleteChapter(int chId)
        {
            string sql = String.Format("update [dbo].[Chapter] set isDel = 1 where chId = '{0}'", chId);
            return SqlHelper.ExcuteNonQuery(sql) > 0;
        }
        #endregion

        #region 查询章节内容
        /// <summary>
        /// 获取所有章节列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllChapter()
        {
            string sql = "SELECT * FROM dbo.Chapter WHERE isDel = '0'";
            return SqlHelper.SqlDataTable(sql);
        }


        /// <summary>
        /// 根据章节Id查找课程章节
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public DataTable GetChapterListById(int cid)
        {
            string sql = string.Format("SELECT * FROM [dbo].[Chapter] WHERE cId = '{0}' AND  isDel = '0'", cid);
            return SqlHelper.SqlDataTable(sql);
        }

        /// <summary>
        /// 根据章节Id查找课程章节
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetChapterById(int id)
        {
            string sql = string.Format("SELECT * FROM [dbo].[Chapter] WHERE chId = '{0}' AND  isDel = '0'", id);
            return SqlHelper.SqlDataTable(sql);
        }

        /// <summary>
        /// 根据章节名称查找课程
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public DataTable GetChapterBySearch(string keys)
        {
            string sql = string.Format("SELECT * FROM dbo.Chapter WHERE chName LIKE '%{0}%' AND isDel ='0'", keys);
            return SqlHelper.SqlDataTable(sql);

        }
        #endregion

    }
}
