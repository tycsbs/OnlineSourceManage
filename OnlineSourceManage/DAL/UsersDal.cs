using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modle;

namespace DAL
{
    public class UsersDal
    {
        #region 新增用户


        /// <summary>
        /// 增加用户
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns>int</returns>
        public int AddUser(Users user)
        {
            if (CheckUserExist(user.uName, user.pwd))
            {
                var sql =
                    string.Format("insert into [dbo].[User] (uName,role,sex,home,pwd) values ('{0}','{1}','{2}','{3}','{4}')",
                        user.uName, user.role, user.sex, user.home, user.pwd);
                return SqlHelper.ExcuteNonQuery(sql);
            }
            return 0;
        }

        /// <summary>
        /// 检测用户名和密码是否已存在
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool CheckUserExist(string name, string pwd)
        {
            var sql = string.Format("SELECT * FROM dbo.[User] WHERE uName = '{0}' AND pwd = '{1}'", name, pwd);
            int count = SqlHelper.ExcuteNonQuery(sql);
            return count < 1;
        }

        #endregion

        #region 删除用户
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userlist">用户id列表 -- '1','2','3'</param>
        /// <returns></returns>
        public bool DeleteUsers(int userlist)
        {
           
            string sql = string.Format("update [dbo].[User] set isDel = 1 where uId = '{0}'",userlist);

            int count = SqlHelper.ExcuteNonQuery(sql);
            return count > 0;
        }

        #endregion

        #region 查询用户
        /// <summary>
        /// 获取全部用户
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetAllUsersInTable()
        {
            string sql = "SELECT * FROM [dbo].[User] WHERE isDel = '0'";
            DataTable dt = SqlHelper.SqlDataTable(sql);
            return dt;
        }
        /// <summary>
        /// 获取所有用户对象
        /// </summary>
        /// <returns>list</returns>
        public List<Users> GetAllUserList()
        {
            string sql = "SELECT * FROM [dbo].[User] WHERE isDel = '0' ORDER BY uId DESC";
            DataTable dt = SqlHelper.SqlDataTable(sql);


            return (from DataRow dataRow in dt.Rows select DataRowToUser(dataRow)).ToList();
        }

        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<Users> GetUsersByKey(string key)
        {
            string sql = string.Format("SELECT * FROM [dbo].[User] WHERE uName LIKE '%{0}%' OR home LIKE '%{1}%' OR role LIKE '%{2}%' AND  isDel = '0'", key, key, key);
            DataTable dt = SqlHelper.SqlDataTable(sql);

            return (from DataRow dataRow in dt.Rows select DataRowToUser(dataRow)).ToList();
        }

        /// <summary>
        /// 根据Id查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Users GetUsersById(int id)
        {
            string sql = string.Format("SELECT * FROM [dbo].[User] WHERE uId = '{0}' AND  isDel = '0'", id);
            DataTable dt = SqlHelper.SqlDataTable(sql);
            Users u = new Users();
            foreach (DataRow dataRow in dt.Rows)
            {
                 u =  DataRowToUser(dataRow);
            }
            return u;
        }


        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="pageSize"> 显示条数</param>
        /// <param name="pageIndex"> 分页索引</param>
        /// <returns></returns>
        public List<Users> GetUsersInPage(int pageSize, int pageIndex)
        {
            int start = pageSize * (pageIndex - 1) + 1;
            int end = pageSize * pageIndex + 1;
            var sql =
                string.Format(
                    "SELECT * FROM ( SELECT *  from [User] T WHERE isDel = '0' ) T WHERE T.uId BETWEEN '{0}' AND '{1}'  ORDER BY uId DESC", start, end);
            DataTable dt = SqlHelper.SqlDataTable(sql);


            return (from DataRow dataRow in dt.Rows select DataRowToUser(dataRow)).ToList();
        }

        /// <summary>
        /// 根据不同类别统计各个维度的用户人数
        /// </summary>
        /// <param name="type">sex home role</param>
        /// <returns></returns>
        public DataTable GetUsersOrderByType(string t)
        {
            var sql = string.Format("SELECT {0} ,count(0) AS num FROM [dbo].[User] WHERE isDel = '0' GROUP BY {1} ",t,t);
            DataTable dt =  SqlHelper.SqlDataTable(sql);
            return dt;

        }



        #endregion

        #region 编辑用户信息及权限
        /// <summary>
        /// 编辑用户信息，设置用户权限
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool EditUser(Users user)
        {
            
            var sql = string.Format("update [dbo].[User] set uName = '{0}' , pwd = '{1}' , home = '{2}' , role = '{3}'  where uId = '{4}'",  user.uName, user.pwd, user.home,user.role, user.uId);
            return SqlHelper.ExcuteNonQuery(sql) > 0;
        }

        /// <summary>
        /// 设置用户权限
        /// </summary>
        /// <param name="useable"></param>
        /// <param name="uId"></param>
        /// <returns></returns>
        public bool UserRightsManage(int useable,int uId)
        {
            var sql = string.Format("update dbo.[User] set useable = '{0}' where uId = '{1}'", useable, uId);
            return SqlHelper.ExcuteNonQuery(sql) > 0;
        }

        /// <summary>
        /// 管理员管理
        /// </summary>
        /// <param name="useable"></param>
        /// <param name="uId"></param>
        /// <returns></returns>
        public bool UserAdminManage(int uId, int roleFlag)
        {
            var sql = string.Format("update dbo.[User] set role = '{0}' where uId = '{1}'", roleFlag, uId);
            return SqlHelper.ExcuteNonQuery(sql) > 0;
        }
        #endregion

        #region 一级地区查询
        /// <summary>
        /// 一级地区查找
        /// </summary>
        /// <returns></returns>
        public List<string> GetProvice()
        {
            string sql = "SELECT AreaName FROM tblArea WHERE AreaPId = '0'";
            DataTable dt = SqlHelper.SqlDataTable(sql);
            List<string> areaList = new List<string>();
            
            foreach (DataRow dr in dt.Rows)
            {
                areaList.Add(dr["AreaName"] as string);
            }
            return areaList;
        }

        #endregion


        #region 关系转对象

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Users DataRowToUser(DataRow row)
        {
            Users user = new Users();
            if (row != null)
            {
                if (row["uId"] != null && row["uId"].ToString() != "")
                {
                    user.uId = int.Parse(row["uId"].ToString());
                }
                if (row["uName"] != null)
                {
                    user.uName = row["uName"].ToString();
                }
                if (row["role"] != null && row["role"].ToString() != "")
                {
                    user.role = int.Parse(row["role"].ToString());
                }
                if (row["sex"] != null && row["sex"].ToString() != "")
                {
                    user.sex = int.Parse(row["sex"].ToString());
                }
                if (row["home"] != null)
                {
                    user.home = row["home"].ToString();
                }
                if (row["pwd"] != null)
                {
                    user.pwd = row["pwd"].ToString();
                }
                if (row["regTime"] != null && row["regTime"].ToString() != "")
                {
                    user.regTime = row["regTime"].ToString();
                }
                if (row["isDel"] != null && row["isDel"].ToString() != "")
                {
                    user.isDel = int.Parse(row["isDel"].ToString());
                }
                if (row["useable"] != null && row["useable"].ToString() != "")
                {
                    user.useable = int.Parse(row["useable"].ToString());
                }

            }
            return user;
        }
        #endregion
    }
}
