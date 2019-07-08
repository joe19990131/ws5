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

        //search
        public List<Models.Books> GetBookByCondtioin(Models.Search arg)
        {
            //sql更改
            DataTable dt = new DataTable();
            string sql = @"SELECT
                            bcl.BOOK_CLASS_NAME AS BookClass,
                            bd.BOOK_NAME AS BookName,
                            bd.BOOK_ID AS BookId,
                            FORMAT(bd.BOOK_BOUGHT_DATE,'yyyy/MM/dd') AS BoughtDate,
                            bc.CODE_NAME AS BookStatus,
                            bc.CODE_ID AS BookStatusId,
                            ISNULL(mm.USER_ENAME,'') AS BookKeeper,
                            ISNULL(bd.BOOK_KEEPER,'') AS BookKeeperId
                           FROM
                            BOOK_DATA bd 
                            INNER JOIN BOOK_CLASS bcl ON bd.BOOK_CLASS_ID = bcl.BOOK_CLASS_ID
                            INNER JOIN BOOK_CODE bc ON bd.BOOK_STATUS = bc.CODE_ID AND bc.CODE_TYPE = 'BOOK_STATUS'
                            LEFT JOIN MEMBER_M mm ON bd.BOOK_KEEPER = mm.[USER_ID]
                    where bd.BOOK_NAME LIKE '%'+@BookName+'%'
                        and (bd.BOOK_CLASS_ID = @BookClassId OR ''=@BookClassId)
                        and (ISNULL(bd.BOOK_KEEPER,'') = @BookKeeperId OR '' = @BookKeeperId)
                        and (bc.CODE_ID = @BookStatusId OR '' = @BookStatusId)
                           ";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BookName", arg.BookName == null ? string.Empty : arg.BookName));
                cmd.Parameters.Add(new SqlParameter("@BookClassId", arg.BookClassId == null ? string.Empty : arg.BookClassId));
                cmd.Parameters.Add(new SqlParameter("@BookKeeperId", arg.BookKeeperId == null ? string.Empty : arg.BookKeeperId));
                cmd.Parameters.Add(new SqlParameter("@BookStatusId", arg.BookStatusId == null ? string.Empty : arg.BookStatusId));
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
                    BookId = (int)row["BookId"],
                    BookClass = row["BookClass"].ToString(),
                    BoughtDate = row["BoughtDate"].ToString(),
                    BookStatus = row["BookStatus"].ToString(),
                    BookKeeper = row["BookKeeper"].ToString(),

                });
            }
            return result;
        }

        //delete
        public bool DeleteBookById(int BookId)
        {
            try
            {
                string sql = "Delete FROM BOOK_DATA Where BOOK_ID=@BookId";
                using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@BookId", BookId));
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //insert
        public void InsertBook(Models.Books arg)
        {
            DataTable dt = new DataTable();
            string sql = @" INSERT INTO BOOK_DATA
						 (
							 BOOK_NAME,
                             BOOK_AUTHOR,
                             BOOK_PUBLISHER,
                             BOOK_NOTE,
                             BOOK_BOUGHT_DATE,
                             BOOK_CLASS_ID,
                             BOOK_STATUS
						 )
						VALUES
						(
							 @BookName,@BookAuthor, @BookPublisher, @BookNote, @BoughtDate, @BookClassId,'A'       
						)";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BookName", arg.BookName == null ? string.Empty : arg.BookName));
                cmd.Parameters.Add(new SqlParameter("@BookAuthor", arg.BookAuthor == null ? string.Empty : arg.BookAuthor));
                cmd.Parameters.Add(new SqlParameter("@BookPublisher", arg.BookPublisher == null ? string.Empty : arg.BookPublisher));
                cmd.Parameters.Add(new SqlParameter("@BookNote", arg.BookNote == null ? string.Empty : arg.BookNote));
                cmd.Parameters.Add(new SqlParameter("@BoughtDate", arg.BoughtDate == null ? (object)DBNull.Value : arg.BoughtDate));
                cmd.Parameters.Add(new SqlParameter("@BookClassId", arg.BookClassId == null ? string.Empty : arg.BookClassId));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }

        }

        public Models.Books GetOriginData(int BookId)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT BOOK_ID AS BookId,
                                    bd.BOOK_NAME AS BookName,
		                            bd.BOOK_AUTHOR AS BookAuthor,
		                            bd.BOOK_PUBLISHER AS BookPublisher,
		                            bd.BOOK_NOTE AS BookNote,
		                            FORMAT(bd.BOOK_BOUGHT_DATE, 'yyyy/MM/dd') AS BoughtDate,
		                            bc1.BOOK_CLASS_ID AS BookClassId,
		                            bc.CODE_NAME AS BookStatus,
                                    bc.CODE_ID AS BookStatusId,
		                            mm.USER_ENAME AS BookKeeper,
                                    bd.BOOK_KEEPER AS BookKeeperId
                            FROM BOOK_DATA bd
							INNER JOIN BOOK_CODE bc ON bd.BOOK_STATUS = bc.CODE_ID
							INNER JOIN BOOK_CLASS bc1 ON bd.BOOK_CLASS_ID = bc1.BOOK_CLASS_ID
							LEFT JOIN MEMBER_M mm ON bd.BOOK_KEEPER = mm.[USER_ID]
                            WHERE bc.CODE_TYPE = 'BOOK_STATUS'
							AND bd.BOOK_ID = @BookId";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BookId", BookId));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }

            return this.MapEditData(dt);
        }

        private Models.Books MapEditData(DataTable dt)
        {
            Models.Books result = new Models.Books();

            result.BookId = (int)dt.Rows[0]["BookId"];
            result.BookName = dt.Rows[0]["BookName"].ToString();
            result.BookAuthor = dt.Rows[0]["BookAuthor"].ToString();
            result.BookPublisher = dt.Rows[0]["BookPublisher"].ToString();
            result.BookNote = dt.Rows[0]["BookNote"].ToString();
            result.BoughtDate = dt.Rows[0]["BoughtDate"].ToString();
            result.BookClassId = dt.Rows[0]["BookClassId"].ToString();
            result.BookStatus = dt.Rows[0]["BookStatus"].ToString();
            result.BookStatusId = dt.Rows[0]["BookStatusId"].ToString();
            result.BookKeeper = dt.Rows[0]["BookKeeper"].ToString();
            result.BookKeeperId = dt.Rows[0]["BookKeeperId"].ToString();

            return result;
        }


        //LendRecord
        public List<Models.LendRecord> GetRecordByCondtioin(int bookId)
        {
            //sql更改
            DataTable dt = new DataTable();
            string sql = @"SELECT 
                            FORMAT(blr.LEND_DATE,'yyyy/MM/dd') AS LendDate,
                            blr.KEEPER_ID AS KeeperId,
                            mm.USER_ENAME AS UserEname,
                            mm.USER_CNAME AS UserCname
                            FROM 
                            BOOK_LEND_RECORD blr INNER JOIN MEMBER_M mm
                            ON blr.KEEPER_ID = mm.USER_ID
                            WHERE blr.BOOK_ID = @BookId";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BookId", bookId));

                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapRecordDataToList(dt);
        }

        private List<LendRecord> MapRecordDataToList(DataTable dt)
        {
            List<Models.LendRecord> result = new List<LendRecord>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new LendRecord()
                {
                    LendDate = row["LendDate"].ToString(),
                    KeeperId = row["KeeperId"].ToString(),
                    UserEname = row["UserEname"].ToString(),
                    UserCname = row["UserCname"].ToString()


                });
            }
            return result;
        }

    }
}