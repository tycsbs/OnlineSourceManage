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
    public class UserInfoController : Controller
    {
        //
        // GET: /UserInfo/

        public ActionResult Index()
        {
            return View("GetUsers");
        }

        private readonly UsersBll _bll = new UsersBll();
        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllUsers()
        {
            int t = _bll.GetAllUserList().Count;
            var pageIndex = int.Parse(Request["page"]);  //当前页  
            var pageSize = int.Parse(Request["rows"]);  //页面行数 
            List<Users> list = _bll.GetUsersInPage(pageSize, pageIndex);

            return Json(new { total = t, rows = list }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 设置用户权限
        /// </summary>
        /// <param name="useable"></param>
        /// <param name="uId"></param>
        /// <returns></returns>
        public ActionResult UserRightsManage(int useable, int uId)
        {
            useable = useable == 0 ? 1 : 0;
            var result = _bll.UserRightsManage(useable, uId);
            var s = result ? "ok" : "error";
            return Content(s);
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="uId"></param>
        /// <returns></returns>
        public ActionResult DeleteUser(int uId)
        {
            bool result = _bll.DeleteUsers(uId);
            var s = result ? "ok" : "error";
            return Content(s);
        }

        /// <summary>
        /// 根据指定类别统计用户数据
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult GetUsersInType(string type)
        {
            DataTable dt = new DataTable();
            dt = _bll.GetUsersOrderByType(type);

            List<string> strName = new List<string>();
            List<int> strNum = new List<int>();


            foreach (DataRow dataRow in dt.Rows)
            {
                strName.Add(dataRow[0] as string);
                strNum.Add(Convert.ToInt32(dataRow[1]));

            }
            return Json(new { name = strName, num = strNum }, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 角色管理
        /// </summary>
        /// <param name="uId"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public ActionResult SetAdmin(int uId, int role)
        {
            role = role == 0 ? 1 : 0;
            var result = _bll.UserAdminManage(uId, role);
            var s = result ? "ok" : "error";
            return Content(s);
        }
        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public ActionResult GetUserByKey(string keys)
        {
            List<Users> list = _bll.GetUsersByKey(keys);
            return Json(new { total = list.Count, rows = list }, JsonRequestBehavior.AllowGet);

        }


       
        /// <summary>
        /// 导出EXCEL
        /// </summary>
        /// <returns></returns>
        public FileResult OutToExcel()
        {


            //创建Excel文件的对象
            HSSFWorkbook book = new HSSFWorkbook();

            //添加一个sheet
            ISheet sheet1 = book.CreateSheet("Sheet1");

            //获取Users数据
            DataTable dt = _bll.GetAllUsersInTable();
            //给sheet1添加第一行的头部标题

            var headerRow = sheet1.CreateRow(0);
            headerRow.HeightInPoints = 40;
            headerRow.CreateCell(0).SetCellValue("用户表信息一览");

            //CellStyle
            ICellStyle headerStyle = book.CreateCellStyle();
            headerStyle.Alignment = HorizontalAlignment.Center;// 左右居中    
            headerStyle.VerticalAlignment = VerticalAlignment.Center;// 上下居中 
            // 设置单元格的背景颜色（单元格的样式会覆盖列或行的样式）    
            headerStyle.FillForegroundColor = (short)8;
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
            string[] columnName = { "编号", "用户名", "角色", "性别", "所在地", "时间", "删除状态", "密码", "禁用状态" };
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
                    excelRow.CreateCell(j).SetCellValue(dtRow[j].ToString());
                    sheet1.AutoSizeColumn(j);
                }

            }

            // 写入到客户端 
            MemoryStream ms = new MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", "user.xls");

        }
    }
}
