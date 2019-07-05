using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace workshop5.Controllers
{
    public class MainController : Controller
    {   Models.CodeService codeService = new Models.CodeService();
        Models.BookService bookService = new Models.BookService();
        // GET: Main
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult BookClass()
        {
            return Json(this.codeService.GetClassCodeTable());
          
        }
        public JsonResult BookKeeper()
        {
            return Json(this.codeService.GetBookKeeperTable());
        }

        public JsonResult BookStatus() {
            return Json(this.codeService.GetBookStatusTable());
        }
        [HttpPost()]
        public JsonResult BookGrid(Models.Search arg)
        {
            return Json(this.bookService.GetBookByCondtioin(arg));
        }
         [HttpPost()]
        public JsonResult DeleteBook(int bookId) {
                return Json(this.bookService.DeleteBookById(bookId));
        }
        [HttpGet()]
        public ActionResult InsertBook()
        {
            return View();
        }
        [HttpPost()]
        public JsonResult InsertBookJson(Models.Books book)
        {
            bookService.InsertBook(book);
            return Json("新增成功");
        }

        public JsonResult LendRecord(int bookId)
        {
            return Json(this.bookService.GetRecordByCondtioin(bookId));
        }
    }
}