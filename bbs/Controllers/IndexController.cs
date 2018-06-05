﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using bbs.Lib;
using bbs.Models;
using bbs.Models.Post;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MySql.Data.MySqlClient;

namespace bbs.Controllers
{
    public class IndexController : AuthController
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
        [Route("/post/{pid}.html")]
        public IActionResult Post(int pid)
        {
            using (var db = Db.table("post as a"))
            {
                using (var data = db.join("user as b", "a.post_uid=b.uid").where("pid", pid).find())
                {
                    if (data.HasRows)
                    {
                        data.Read();
                        ViewData["title"] = data["post_title"];
                        ViewData["content"] = data["post_content"];
                        ViewData["time"] = data["post_time"];
                        ViewData["pid"] = data["pid"];
                        //递归出分区
                        int aid = int.Parse(data["post_aid"]);
                        var arealist = new ArrayList();
                        while (aid > 0)
                        {
                            using (var db1 = Db.table("area"))
                            {
                                var area = db1.where("aid", aid).find();
                                if (area.HasRows)
                                {
                                    area.Read();
                                    aid = int.Parse(area["area_father"]);
                                    arealist.Add(new AreaModel(aid, area["area_name"]));
                                }
                            }
                        }
                        ViewData["area"] = arealist;
                        return View("Article");
                    }
                }
            }
            return View("404");
        }

        public JsonResult PostArticle(string title, string content, string aid)
        {
            var m = new PostModel()
            {
                title = Functions.FilterXSS(title),
                content = Functions.FilterXSS(content),
                aid = aid
            };
            TryValidateModel(m);
            if (ModelState.IsValid)
            {
                using (Db db = Db.table("area"), db1 = Db.table("post"))
                {
                    var area = db.where("aid", aid).find();
                    if (!area.HasRows)
                    {
                        return Json(new ErrorJsonModel(-1, "不存在的分区"));
                    }
                    area.Read();
                    if (int.Parse(area["area_father"]) <= 0)
                    {
                        return Json(new ErrorJsonModel(-1, "父类分区,不能发帖"));
                    }
                    var postData = new Dictionary<string, object>();
                    postData.Add("post_title", m.title);
                    postData.Add("post_content", m.content);
                    postData.Add("post_aid", aid);
                    postData.Add("post_uid",ViewData["uid"]);
                    postData.Add("post_time", Functions.timestamp());
                    postData.Add("post_end_reply_time", Functions.timestamp());
                    postData.Add("post_reply_number", 0);
                    db1.insert(postData);
                    return Json(new PostSuccessJson(0, "发帖成功", db1.lastInsertId()));
                }
            }
            return Json(new ErrorJsonModel(-1, Functions.getErrorMsg(ModelState)));
        }

        public class PostSuccessJson
        {
            public int code;
            public string msg;
            public long pid;
            public PostSuccessJson(int code, string msg, long pid)
            {
                this.code = code;
                this.msg = msg;
                this.pid = pid;
            }
        }

        public JsonResult Area(string aid = "-1")
        {
            var db = Db.table("area");
            if (int.Parse(aid) > 0)
            {
                db.where("area_father", aid);
            }
            else
            {
                db.where("area_father", 0);
            }
            var results = db.order("area_priority").select();
            var rows = new ArrayList();
            while (results.Read())
            {
                rows.Add(new AreaRow((int)results.GetValue(0), results.GetValue(1).ToString(), results.GetValue(3).ToString()));
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

        class AreaRow
        {
            public AreaRow(int aid, string title, string logo)
            {
                this.aid = aid;
                this.logo = logo;
                this.title = title;
            }
            public int aid { get; set; }

            public string logo { get; set; }

            public string title { get; set; }
        }
    }

}