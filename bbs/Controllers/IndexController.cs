using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bbs.Lib;
using bbs.Models;
using Microsoft.AspNetCore.Mvc;

namespace bbs.Controllers
{
    public class IndexController : Controller
    {
        public IActionResult Post()
        {
            return View();
        }


        public JsonResult PostArticle()
        {

            return Json(new ErrorJsonModel(-1, "错误"));
        }

        public JsonResult Plate()
        {
            var results = Db.table("plate").select();
            var rows = new ArrayList();
            while (results.Read())
            {
                rows.Add(new PlateRow((int)results.GetValue(0), results.GetValue(1).ToString()));
            }
            return Json(rows);
        }

        [HttpPost]
        public JsonResult UploadFile(int type)
        {
            return Json(new ErrorJsonModel(-1, type.ToString()));
        }

        class PlateRow
        {
            public PlateRow(int pid, string title)
            {
                this.pid = pid;
                this.title = title;
            }
            public int pid { get; set; }
            public string title { get; set; }
        }
    }

}