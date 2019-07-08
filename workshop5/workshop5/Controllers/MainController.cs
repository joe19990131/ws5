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

        public JsonResult DropDownList(string type) {
            switch (type){
                case "class":
                    return Json(this.codeService.GetClassCodeTable());
                    break;
                case "keeper":
                    return Json(this.codeService.GetBookKeeperTable());
                    break;
                case "status":
                    return Json(this.codeService.GetBookStatusTable());
                    break;
                default:
                    return Json(true);
            }
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

        public JsonResult EditGetData(int bookId)
        {
            return Json(this.bookService.GetOriginData(bookId));
        }

        public JsonResult UpdateData(Models.Books book) {
            bookService.UpdateBookData(book);
            return Json("修改成功");
        }
    }
}