using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bbs.Lib
{
    public class AuthMiddleware
    {
        public static bool isLogin = false;
        public static int uid;
        public static ArrayList userMsg;
        protected RequestDelegate requestDelegate;
        public AuthMiddleware(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;
            userMsg = new ArrayList();
        }

        protected string[] _allow = new string[] { "index" };

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path.ToString();
            int pos = path.IndexOf("/", 1);
            if (pos > 0)
            {
                path = path.Substring(1, pos - 1).ToLower();
            }
            if ((context.Request.Cookies["token"] == null || context.Request.Cookies["uid"] == null))
            {
                if (((IList)_allow).Contains(path))
                {
                    context.Response.Redirect("/Home/Login");
                }
            }
            else
            {
                using (Db db = Db.table("user_token"), db2 = Db.table("user")) {
                    using (ResultCollection data = db.where("token", context.Request.Cookies["token"].ToString()).
                         where("uid", context.Request.Cookies["uid"].ToString()).find())
                    {
                        if (!data.HasRows)
                        {
                            context.Response.Redirect("/Home/Login");
                        }
                        else
                        {
                            bool HasRows = data.HasRows;
                            using (ResultCollection userMsgResult = db2.where("uid", context.Request.Cookies["uid"].ToString()).find())
                            {
                                if (HasRows)
                                {
                                    isLogin = true;
                                    uid = int.Parse(context.Request.Cookies["uid"].ToString());
                                    userMsgResult.Read();
                                    userMsg.Add(userMsgResult["uid"]);
                                    userMsg.Add(userMsgResult["username"]);
                                }
                                else
                                {
                                    context.Response.Redirect("/Home/Login");
                                }
                            }
                        }
                    }   
                }
            }
            await requestDelegate.Invoke(context);
        }
    }
}
