using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace workshop5.Models
{
    public class CodeService
    {
        public List<SelectListItem> GetClassCodeTable()
        {
            DataTable dt = new DataTable();
            string sql = @"Select Distinct BOOK_CLASS_ID AS BookClassId ,BOOK_CLASS_NAME As BookClass 
                           From BOOK_CLASS 
                           ";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                //cmd.Parameters.Add(new SqlParameter("@Class", Class));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapDropDownListData(dt,"BookClass","BookClassId");
        }

        public List<SelectListItem> GetBookKeeperTable()
        {
            DataTable dt = new DataTable();
            string sql = @"Select Distinct USER_ID AS UserId ,USER_ENAME As UserEname 
                           From MEMBER_M 
                           ";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                //cmd.Parameters.Add(new SqlParameter("@Class", Class));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapDropDownListData(dt,"UserEname","UserEname");
        }
 

        public List<SelectListItem> GetBookStatusTable()
        {
            DataTable dt = new DataTable();
            string sql = @"Select Distinct CODE_NAME AS CodeName  
                           From BOOK_CODE
                            where CODE_TYPE LIKE 'BOOK_STATUS'
                           ";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                //cmd.Parameters.Add(new SqlParameter("@Class", Class));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapDropDownListData(dt,"CodeName","CodeName");
        }

        private string GetDBConnectionString()
        {
            return
               System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
        }

        private List<SelectListItem> MapDropDownListData(DataTable dt,string text,string val)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            result.Add(new SelectListItem()
            {
                Text = "請選擇",
                Value = ""
            });
            foreach (DataRow row in dt.Rows)
            {

                result.Add(new SelectListItem()
                {
                    Text = row[text].ToString(),
                    Value = row[val].ToString()
                });
            }
            return result;
        }
    }
}