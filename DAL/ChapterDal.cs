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
            string sql = "SELECT dbo.Course.cName,dbo.Chapter.chName,dbo.Chapter.chId,dbo.Chapter.cId,dbo.Chapter.starttime,dbo.Chapter.mark, dbo.Chapter.isDel,[types] ,dbo.chapter_resource.srcType,dbo.chapter_resource.srcUrl FROM( dbo.Course JOIN dbo.Chapter ON dbo.Course.cId = dbo.Chapter.cId LEFT JOIN dbo.chapter_resource ON dbo.Chapter.chId = dbo.chapter_resource.chId) WHERE dbo.Course.isDel = '0' ORDER BY dbo.Chapter.cId";
            return SqlHelper.SqlDataTable(sql);
        }
        /// <summary>
        /// 统计章节信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetChapterForChart()   
        {
            string sql = "SELECT dbo.Course.cName,COUNT(0) AS num FROM ( dbo.Course JOIN dbo.Chapter ON dbo.Course.cId = dbo.Chapter.cId)WHERE dbo.Course.isDel = '0' GROUP BY cName";
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
            string sql = string.Format("SELECT dbo.Course.cName,chName,chId,dbo.Chapter.cId,dbo.Chapter.starttime,dbo.Chapter.isDel,dbo.Chapter.mark,[types] FROM( dbo.Course JOIN dbo.Chapter ON dbo.Course.cId = dbo.Chapter.cId) WHERE dbo.Course.isDel = '0' AND dbo.Chapter.isDel = '0' AND chId = '{0}' ORDER BY dbo.Chapter.cId", id);
            return SqlHelper.SqlDataTable(sql);
        }

        /// <summary>
        /// 根据章节名称查找课程
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public DataTable GetChapterBySearch(string keys)
        {
            string sql = string.Format("SELECT dbo.Course.cName,chName,chId,dbo.Chapter.cId,dbo.Chapter.starttime,dbo.Chapter.isDel,dbo.Chapter.mark,[types] FROM( dbo.Course JOIN dbo.Chapter ON dbo.Course.cId = dbo.Chapter.cId) WHERE dbo.Course.cName LIKE '%{0}%' OR [types] LIKE '%{0}%' OR chId LIKE '%{0}%' AND  dbo.Course.isDel = '0' AND dbo.Chapter.isDel = '0' ORDER BY dbo.Chapter.cId", keys);
            return SqlHelper.SqlDataTable(sql);

        }
        #endregion
        /// <summary>
        /// 编辑章节信息
        /// </summary>
        /// <param name="chId"></param>
        /// <param name="chName"></param>
        /// <param name="mark"></param>
        /// <returns></returns>
        public bool UpdateChapter(int chId, string chName, string mark)
        {
            string sql = string.Format("UPDATE dbo.Chapter SET chName='{0}' ,mark='{1}'  WHERE chId = '{2}'",chName,mark,chId);
            return SqlHelper.ExcuteNonQuery(sql) > 0;
        }
        /// <summary>
        /// 添加文件信息
        /// </summary>
        /// <param name="chId"></param>
        /// <param name="filesUrl"></param>
        /// <param name="filesType"></param>
        /// <returns></returns>
        public bool AddChapterFiles(int chId,string filesUrl,string filesType)
        {
            string sql = string.Format(" INSERT dbo.chapter_resource( chId ,srcUrl , srcType )VALUES  ( {0} ,N'{1}' ,N'{2}' )",chId,filesUrl,filesType);
            return SqlHelper.ExcuteNonQuery(sql) > 0;
        }

    }
}
