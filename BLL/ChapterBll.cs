using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Modle;

namespace BLL
{
    public class ChapterBll
    {

        private readonly  ChapterDal _dal = new ChapterDal();

        #region 章节增加方法

        public bool AddChapter(Chapter chapter)
        {
            return _dal.AddChapter(chapter);
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
           
            return _dal.DeleteChapter(chId);
        }

        /// <summary>
        /// 删除指定章节的指定资源文件
        /// </summary>
        /// <param name="chId"></param>
        /// <returns></returns>
        public bool DeleteFile(int chId)
        {
            return _dal.DeleteChapter(chId);
        }
        #endregion

        #region 查询章节内容
        /// <summary>
        /// 获取所有章节列表
        /// </summary>
        /// <returns></returns>
        public List<Chapter> GetAllChapter()
        {

            DataTable dt = _dal.GetAllChapter();
            return (from DataRow dr in dt.Rows select DataRowToModel(dr)).ToList();
        }
        /// <summary>
        /// 获取章节资源信息
        /// </summary>
        /// <returns></returns>
        public List<ChapterSource> GetChapterFile()
        {
            DataTable dt = _dal.GetChapterFile();
            return (from DataRow dr in dt.Rows select DataRowToFile(dr)).ToList();
        }

        public DataTable GetChapterlist()   
        {
            return _dal.GetAllChapter();
        }

        /// <summary>
        /// 统计章节信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetChapterForChart()   
        {
            return _dal.GetChapterForChart();
        }




        /// <summary>
        /// 根据章节Id查找课程章节
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public List<chapterBase> GetChapterListById(int cid)
        {
            
            DataTable dt = _dal.GetChapterListById(cid);
            return (from DataRow dr in dt.Rows select DataRowToChapterBase(dr)).ToList();
        }

        /// <summary>
        /// 根据章节Id查找课程章节
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Chapter GetChapterById(int id)
        {
            DataTable dt = _dal.GetChapterById(id);
            return (from DataRow dr in dt.Rows select DataRowToModel(dr)).ToList()[0];
        }


        /// <summary>
        /// 根据章节名称查找课程
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public List<Chapter> GetChapterBySearch(string keys)
        {
            DataTable dt = _dal.GetChapterBySearch(keys);
            return (from DataRow dr in dt.Rows select DataRowToModel(dr)).ToList();

        }
        #endregion
        /// <summary>
        /// 修改章节信息
        /// </summary>
        /// <param name="chId"></param>
        /// <param name="chName"></param>
        /// <param name="mark"></param>
        /// <returns></returns>
        public bool UpdateChapter(int chId,string chName,string mark)
        {
            return _dal.UpdateChapter(chId,chName,mark);
        }
        /// <summary>
        /// 添加文件信息
        /// </summary>
        /// <param name="chId"></param>
        /// <param name="filesUrl"></param>
        /// <param name="filesType"></param>
        /// <returns></returns>
        public bool AddChapterFiles(int chId, string filesUrl, string filesType)
        {
            return _dal.AddChapterFiles(chId, filesUrl, filesType);
        }


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
                if (row["cName"] != null)
                {
                    model.cName = row["cName"].ToString();
                }
                if (row["starttime"] != null)
                {
                    model.starttime = row["starttime"].ToString();
                }
                if (row["mark"] != null)
                {
                    model.mark = row["mark"].ToString();
                }
                if (row["types"] != null)
                {
                    model.types = row["types"].ToString();
                }
                if (row["isDel"] != null && row["isDel"].ToString() != "")
                {
                    model.isDel = int.Parse(row["isDel"].ToString());
                }
            }
            return model;
        }

        public ChapterSource DataRowToFile(DataRow row)
        {
            ChapterSource model = new ChapterSource();
            if (row != null)    
            {
                if (row["chId"] != null && row["chId"].ToString() != "")
                {
                    model.chId = int.Parse(row["chId"].ToString());
                }
                if (row["chName"] != null)
                {
                    model.chName = row["chName"].ToString();
                }
                if (row["srcUrl"] != null)
                {
                    model.srcUrl = row["srcUrl"].ToString();
                }
                if (row["srcType"] != null)
                {
                    model.srcType = row["srcType"].ToString();
                }
                if (row["isDel"] != null && row["isDel"].ToString() != "")
                {
                    model.isDel = int.Parse(row["isDel"].ToString());
                }
                if (row["starttime"] != null)
                {
                    model.starttime = row["starttime"].ToString();
                }
            }
            return model;
        }

        public chapterBase DataRowToChapterBase(DataRow row)
        {
            chapterBase model = new chapterBase();
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
