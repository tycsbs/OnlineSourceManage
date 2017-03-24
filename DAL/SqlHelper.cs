using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SqlHelper
    {
        private static readonly string ConnStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

        /// <summary>
        /// 读取数据库返回datatable
        /// </summary>
        /// <param name="sql">数据库查询语言</param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static DataTable SqlDataTable(string sql, params SqlParameter[] parms)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                DataTable dt = new DataTable();
                using (SqlDataAdapter sda = new SqlDataAdapter(sql, conn))
                {
                    sda.Fill(dt);
                }
                return dt;
            }
        }

        /// <summary>
        /// 查询数据返回数据的个数
        /// </summary>
        /// <param name="sql">查询数据库语句</param>
        /// <param name="param">数据化查询值</param>
        /// <returns>返回int类型</returns>
        public static int ExcuteNonQuery(string sql, params SqlParameter[] param)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddRange(param);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// 查询数据并返回object对象类型
        /// </summary>
        /// <param name="sql">查询数据库语句</param>
        /// <param name="param">数据化查询值</param>
        /// <returns>返回object</returns>
        public static object ExcuteScalar(string sql, params SqlParameter[] param)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddRange(param);
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql">查询数据库语句</param>
        /// <param name="param">数据化查询值</param>
        /// <returns>返回SqlDataReader</returns>
        public static SqlDataReader ExcuteReader(string sql, params SqlParameter[] param)
        {
            SqlConnection conn = new SqlConnection(ConnStr);
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddRange(param);
                    conn.Open();
                    return cmd.ExecuteReader();
                }
            }
            catch (Exception)
            {
                // 如果在执行Reader方法的时候，出现异常，那么程序停止，关闭Connection的方法就不会执行
                // 因此需要手动关闭
                conn.Dispose();
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql">数据库查询语句</param>
        /// <param name="param">数据化查询的值</param>
        /// <returns>DataSet</returns>
        public static DataSet ExcuteDataSet(string sql, params SqlParameter[] param)
        {
            DataSet dataSet = new DataSet();
            using (SqlDataAdapter sda = new SqlDataAdapter(sql, ConnStr))
            {
                sda.SelectCommand.Parameters.AddRange(param);
                sda.Fill(dataSet);
            }
            return dataSet;
        }
    }
}
