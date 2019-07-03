using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace workshop5.Controllers
{
    public class MainController : Controller
    {Models.CodeService codeService = new Models.CodeService();
        // GET: Main
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult BookClass()
        {
            return Json(this.codeService.GetClassCodeTable());
          
        }
    }
}