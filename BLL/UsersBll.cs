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
    public class UsersBll
    {
        private readonly UsersDal _dal = new UsersDal();
        

        #region 新增用户


        /// <summary>
        /// 增加用户
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns>int</returns>
        public bool AddUser(Users user)
        {
            return  _dal.AddUser(user) > 0;
        }

        /// <summary>
        /// 检测用户名和密码是否已存在
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool CheckUserExist(string name, string pwd)
        {
            return _dal.CheckUserExist(name, pwd);
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public List<Users> UserLogin(string name, string pwd)
        {
            DataTable dt = _dal.UserLogin(name, pwd);
            return (from DataRow row in dt.Rows select _dal.DataRowToUser(row)).ToList();
        }

        #endregion

        #region 删除用户
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId">用户id列表</param>
        /// <returns></returns>
        public bool DeleteUsers(int userId)
        {
            return _dal.DeleteUsers(userId);
        }

        #endregion

        #region 查询用户
        /// <summary>
        /// 获取全部用户
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetAllUsersInTable()
        {
            DataTable dt = _dal.GetAllUsersInTable();
            DataTable newTable = new DataTable();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                DataColumn dc = new DataColumn(i.ToString());
                newTable.Columns.Add(dc);   
            }

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                DataRow dtRow = dt.Rows[i];
                //DataRow new
                DataRow newTableRow = newTable.NewRow();
                for (int j = 0; j < dtRow.ItemArray.Length; j++)
                {
                    string v = dtRow[j].ToString();
                    if (j == 2)
                    {
                        v = v == "0" ? "普通用户" : "管理员";
                        //newTableRow[j] = v;
                    }
                    else if (j == 3)
                    {
                        v = v == "0" ? "男" : "女";
                        //newTableRow[j] = v;
                    }

                    else if (j == 6)
                    {
                         v = v == "0" ? "正常" : "已删除";
                         //newTableRow[j] = v;
                    }

                    else if (j == 8)
                    {
                        v = v == "0" ? "正常" : "禁用中...";
                        //newTableRow[j] = v;
                    }   
                    
                        newTableRow[j] = v;
                    
                    
                }
                newTable.Rows.Add(newTableRow);

            }
            return newTable;
        }
        /// <summary>
        /// 获取所有用户对象
        /// </summary>
        /// <returns>list</returns>
        public List<Users> GetAllUserList()
        {
            
            return _dal.GetAllUserList();
        }

        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<Users> GetUsersByKey(string key)
        {

            return _dal.GetUsersByKey(key);
        }

        /// <summary>
        /// 根据Id查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Users GetUsersById(int id)
        {
           return _dal.GetUsersById(id);
        }
        
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="pageSize"> 显示条数</param>
        /// <param name="pageIndex"> 分页索引</param>
        /// <returns></returns>
        public List<Users> GetUsersInPage(int pageSize, int pageIndex)
        {
            return _dal.GetUsersInPage(pageSize, pageIndex);

        }

        /// <summary>
        /// 根据不同类别统计各个维度的用户人数 sex home role
        /// </summary>
        /// <param name="type">sex home role</param>
        /// <returns></returns>
        public DataTable GetUsersOrderByType(string type)
        {
            return _dal.GetUsersOrderByType(type);

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
            return _dal.EditUser(user);
        }

        /// <summary>
        /// 设置用户权限
        /// </summary>
        /// <param name="useable"></param>
        /// <param name="uId"></param>
        /// <returns></returns>
        public bool UserRightsManage(int useable, int uId)
        {
            return _dal.UserRightsManage(useable, uId);
        }

        /// <summary>
        /// 管理员管理
        /// </summary>
        /// <param name="roleFlag"></param>
        /// <param name="uId"></param>
        /// <returns></returns>
        public bool UserAdminManage(int uId, int roleFlag)
        {
            return _dal.UserAdminManage(uId, roleFlag); 
        }

        /// <summary>
        /// 一级地区查找
        /// </summary>
        /// <returns></returns>
        public List<string> GetProvice()
        {
            return _dal.GetProvice();
        }
        #endregion

    }
}
