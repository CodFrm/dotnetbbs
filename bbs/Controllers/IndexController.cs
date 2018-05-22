using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using bbs.Lib;
using bbs.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace bbs.Controllers
{
    public class IndexController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;


        public IndexController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

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
        public JsonResult UploadFile()
        {
            var uploadfile = Request.Form.Files[0];

            var now = DateTime.Now;
            var webRootPath = _hostingEnvironment.WebRootPath;
            var filePath = string.Format("/data/img/{0}/{1}/{2}/", now.ToString("yyyy"), now.ToString("MM"), now.ToString("dd"));

            if (!Directory.Exists(webRootPath + filePath))
            {
                Directory.CreateDirectory(webRootPath + filePath);
            }

            if (uploadfile != null)
            {
                //文件后缀
                var fileExtension = Path.GetExtension(uploadfile.FileName);

                //判断后缀是否是图片
                const string fileFilt = ".gif|.jpg|.php|.jsp|.jpeg|.png|......";
                if (fileExtension == null)
                {
                    return Json(new FileUploadRet(0, "错误的文件", ""));
                }
                if (fileFilt.IndexOf(fileExtension.ToLower(), StringComparison.Ordinal) <= -1)
                {
                    return Json(new FileUploadRet(0, "错误的文件", ""));
                }
                long length = uploadfile.Length;
                if (length > 1024 * 1024 * 4)
                {
                    return Json(new FileUploadRet(0, "文件大小过大,0-4M之间", ""));
                }

                var strDateTime = DateTime.Now.ToString("yyMMddhhmmssfff"); //取得时间字符串
                var strRan = Convert.ToString(new Random().Next(100, 999)); //生成三位随机数
                var saveName = strDateTime + strRan + fileExtension;

                using (FileStream fs = System.IO.File.Create(webRootPath + filePath + saveName))
                {
                    uploadfile.CopyTo(fs);
                    fs.Flush();
                }
                return Json(new FileUploadRet(1, "上传成功", filePath + saveName));
            }
            return Json(new FileUploadRet(0, "参数错误", ""));
        }

        class FileUploadRet
        {
            public FileUploadRet(int s, string m, string u)
            {
                success = s; message = m; url = u;
            }
            public int success;
            public string message;
            public string url;
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