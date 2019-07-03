using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace workshop5.Models
{
    public class BookService
    {
        private string GetDBConnectionString()
        {
            return
                System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
        }

        public List<Models.Books> GetBookByCondtioin(Models.Search arg)
        {
            //sql更改
            DataTable dt = new DataTable();
            string sql = @"SELECT
                            bcl.BOOK_CLASS_NAME AS BookClass,
                            bd.BOOK_NAME AS BookName,
                            FORMAT(bd.BOOK_BOUGHT_DATE,'yyyy/MM/dd') AS BoughtDate,
                            bc.CODE_NAME AS BookStatus,
                            ISNULL(mm.USER_ENAME,'') AS BookKeeper
                           FROM
                            BOOK_DATA bd 
                            INNER JOIN BOOK_CLASS bcl ON bd.BOOK_CLASS_ID = bcl.BOOK_CLASS_ID
                            INNER JOIN BOOK_CODE bc ON bd.BOOK_STATUS = bc.CODE_ID AND bc.CODE_TYPE = 'BOOK_STATUS'
                            LEFT JOIN MEMBER_M mm ON bd.BOOK_KEEPER = mm.[USER_ID]
                    where bd.BOOK_NAME LIKE '%'+@BookName+'%'
                        and bd.BOOK_CLASS_ID LIKE @BookClassId+'%'
                        and ISNULL(mm.USER_ENAME,'') LIKE '%'+@BookKeeper+'%'
                        and bc.CODE_NAME LIKE @BookStatus+'%'
                           ";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BookName", arg.BookName == null ? string.Empty : arg.BookName));
                cmd.Parameters.Add(new SqlParameter("@BookClassId", arg.BookClassId == null ? string.Empty : arg.BookClassId));
                cmd.Parameters.Add(new SqlParameter("@BookKeeper", arg.BookKeeper == null ? string.Empty : arg.BookKeeper));
                cmd.Parameters.Add(new SqlParameter("@BookStatus", arg.BookStatus == null ? string.Empty : arg.BookStatus));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapBooksDataToList(dt);
        }

        private List<Models.Books> MapBooksDataToList(DataTable bookData)
        {
            List<Models.Books> result = new List<Books>();
            foreach (DataRow row in bookData.Rows)
            {
                result.Add(new Books()
                {
                    BookName = row["BookName"].ToString(),
                    BookClass = row["BookClass"].ToString(),
                    BoughtDate = row["BoughtDate"].ToString(),
                    BookStatus = row["BookStatus"].ToString(),
                    BookKeeper = row["BookKeeper"].ToString(),

                });
            }
            return result;
        }
    }
}