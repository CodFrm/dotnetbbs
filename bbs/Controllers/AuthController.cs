using bbs.Lib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bbs.Controllers
{
    public class AuthController: Controller
    {
        public bool isLogin = false;
        public int uid;
        public ArrayList userMsg;
        protected string[] _allow = new string[] { "index" };

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            userMsg = new ArrayList();
            var path = Request.Path.ToString();
            int pos = path.IndexOf("/", 1);
            if (pos > 0)
            {
                path = path.Substring(1, pos - 1).ToLower();
            }
            if ((Request.Cookies["token"] == null || Request.Cookies["uid"] == null))
            {
                if (((IList)_allow).Contains(path))
                {
                    Response.Redirect("/Home/Login");
                }
            }
            else
            {
                using (Db db = Db.table("user_token"), db2 = Db.table("user"))
                {
                    using (ResultCollection data = db.where("token", Request.Cookies["token"].ToString()).
                         where("uid", Request.Cookies["uid"].ToString()).find())
                    {
                        if (!data.HasRows)
                        {
                            Response.Redirect("/Home/Login");
                        }
                        else
                        {
                            bool HasRows = data.HasRows;
                            using (ResultCollection userMsgResult = db2.where("uid", Request.Cookies["uid"].ToString()).find())
                            {
                                if (HasRows)
                                {
                                    isLogin = true;
                                    uid = int.Parse(Request.Cookies["uid"].ToString());
                                    userMsgResult.Read();
                                    userMsg.Add(userMsgResult["uid"]);
                                    userMsg.Add(userMsgResult["username"]);
                                    ViewData["usermsg"] = userMsg;
                                    ViewData["uid"] = uid;
                                    ViewData["username"] = userMsgResult["username"];
                                }
                                else
                                {
                                    Response.Redirect("/Home/Login");
                                }
                            }
                        }
                    }
                }
            }
            ViewData["isLogin"] = isLogin;
            
        }
    }
}
