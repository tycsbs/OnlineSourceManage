using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Modle;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace OnlineSourceManage.Controllers
{
    public class ChapterController : Controller
    {
        //
        // GET: /Chapter/

        private readonly ChapterBll _bll = new ChapterBll();
        public ActionResult ChapterPage()
        {
            return View("Chapter");
        }
        /// <summary>
        /// 获取所有课程的所有章节
        /// </summary>
        /// <returns></returns>
        public ActionResult GetChapter()
        {
            List<Chapter> list = _bll.GetAllChapter();
            var pageIndex = int.Parse(Request["page"]); //当前页  
            var pageSize = int.Parse(Request["rows"]); //页面行数 
            IEnumerable<Chapter> courseList = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return Json(new { total = list.Count, rows = courseList }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取所有课程的所有章节
        /// </summary>
        /// <returns></returns>
        public ActionResult GetChapterFile()
        {
            List<ChapterSource> list = _bll.GetChapterFile();
            var pageIndex = int.Parse(Request["page"]); //当前页  
            var pageSize = int.Parse(Request["rows"]); //页面行数 
            IEnumerable<ChapterSource> courseList = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return Json(new { total = list.Count, rows = courseList }, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// NPOI excel 导出
        /// </summary>
        /// <returns></returns>
        public FileResult OutToExcel()
        {
            //创建Excel文件的对象
            HSSFWorkbook book = new HSSFWorkbook();

            //添加一个sheet
            ISheet sheet1 = book.CreateSheet("Sheet1");

            //获取chapter数据
            DataTable dt = _bll.GetChapterlist();
            //给sheet1添加第一行的头部标题

            var headerRow = sheet1.CreateRow(0);
            headerRow.HeightInPoints = 40;
            headerRow.CreateCell(0).SetCellValue("课程信息表");

            //CellStyle
            ICellStyle headerStyle = book.CreateCellStyle();
            headerStyle.Alignment = HorizontalAlignment.Center; // 左右居中    
            headerStyle.VerticalAlignment = VerticalAlignment.Center; // 上下居中 
            // 设置单元格的背景颜色（单元格的样式会覆盖列或行的样式）    
            headerStyle.FillForegroundColor = 8;
            //定义font
            IFont font = book.CreateFont();
            font.FontHeightInPoints = 35;
            font.Boldweight = 700;
            font.Color = HSSFColor.Green.Index;
            headerStyle.SetFont(font);

            headerRow.GetCell(0).CellStyle = headerStyle;
            sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, dt.Columns.Count - 1));


            IRow row1 = sheet1.CreateRow(1);


            //将数据逐步写入sheet1各个行
            string[] columnName = { "课程名称", "章节名称", "课程编号", "章节编号", "创建时间", "描述信息", "删除", "课程类型" };
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = row1.CreateCell(i);
                cell.SetCellValue(columnName[i]);
            }

            int rowIndex = 2;
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                DataRow dtRow = dt.Rows[i];
                IRow excelRow = sheet1.CreateRow(rowIndex++);
                for (int j = 0; j < dtRow.ItemArray.Length; j++)
                {
                    excelRow.CreateCell(j).SetCellValue(dtRow[j].ToString() == "0" ? "正常" : dtRow[j].ToString());
                    sheet1.AutoSizeColumn(j);
                }
            }

            // 写入到客户端 
            MemoryStream ms = new MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", "课程章节信息表.xls");

        }

        /// <summary>
        /// 获取章节的统计信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetChapterForChart()
        {
            DataTable dt = _bll.GetChapterForChart();
            List<int> numlist = new List<int>();
            List<string> strlist = new List<string>();

            foreach (DataRow dataRow in dt.Rows)
            {
                numlist.Add(Convert.ToInt32(dataRow[1]));
                strlist.Add(dataRow[0] as string);
            }

            return Json(new { name = strlist, num = numlist }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除章节
        /// </summary>
        /// <param name="chId"></param>
        /// <returns></returns>
        public ActionResult DeleteChapter(int chId)
        {

            var isok = _bll.DeleteChapter(chId);
            return Content(isok ? "ok" : "error");
        }
        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public ActionResult SearchChapter(string keys)
        {
            List<Chapter> list = _bll.GetChapterBySearch(keys);
            return Json(new { total = list.Count, rows = list }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取课程信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCourseInfo()
        {
            CourseBll bll = new CourseBll();
            List<Course> list = bll.GetAllCourse(true);
            return Json(new { total = list.Count, rows = list }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取所有课程列表
        /// </summary>
        /// <param name="cId"></param>
        /// <param name="chName"></param>
        /// <param name="mark"></param>
        /// <returns></returns>
        public ActionResult AddChapter(int cId, string chName, string mark)
        {
            Chapter chapter = new Chapter()
            {
                cId = cId,
                chName = chName,
                mark = mark
            };
            var isok = _bll.AddChapter(chapter);
            return Content(isok ? "ok" : "error");
        }
        /// <summary>
        /// 章节信息编辑
        /// </summary>
        /// <param name="chId"></param>
        /// <param name="chName"></param>
        /// <param name="mark"></param>
        /// <returns></returns>
        public ActionResult EditChapter(int chId, string chName, string mark)
        {
            var isok = _bll.UpdateChapter(chId, chName, mark);
            return Content(isok ? "ok" : "error");
        }
        /// <summary>
        /// 根据课程id 查找对应的章节
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public ActionResult GetChaptersById(int cid)
        {
            List<chapterBase> chapters = _bll.GetChapterListById(cid);
            return Json(new { total = chapters.Count, rows = chapters }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="file"></param>
        /// <param name="chId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddChapterFile(HttpPostedFileBase file, int chId)
        {
            Random random = new Random();
            var r = DateTime.Now.ToFileTimeUtc().ToString();
            var fileName = r + Path.GetFileName(file.FileName);
            var fileType = Path.GetExtension(fileName).Substring(1).ToUpper();
            {
                var filePath = Path.Combine(Request.MapPath(@"~/Files"), fileName);
                var s = filePath.Substring(filePath.LastIndexOf("Files") + 0);
                try
                {
                    file.SaveAs(filePath);
                    bool isok = _bll.AddChapterFiles(chId, s, fileType,file.FileName);
                    if (isok)
                    {
                        return Redirect("ChapterPage");
                    }
                    return Content("上传失败");
                }
                catch
                {
                    return Content("error", "text/plain");
                }
            }
        }
        /// <summary>
        /// 删除指定资源文件
        /// </summary>
        /// <param name="chId"></param>
        /// <returns></returns>
        public ActionResult DeleteFile(int id)
        {
            var isok = _bll.DeleteFile(id);
            return Content(isok ? "ok" : "error");
        }
    }
}
