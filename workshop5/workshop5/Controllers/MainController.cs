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
    }
}