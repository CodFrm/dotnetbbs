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
        public static MySqlDataReader userMsg;
        protected RequestDelegate requestDelegate;
        public AuthMiddleware(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;
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
                var data = Db.table("user_token").where("token", context.Request.Cookies["token"].ToString()).
                     where("uid", context.Request.Cookies["uid"].ToString()).find();
                if (!data.HasRows)
                {
                    context.Response.Redirect("/Home/Login");
                }
                else
                {
                    userMsg = Db.table("user").where("uid", context.Request.Cookies["uid"].ToString()).find();
                    if (data.HasRows)
                    {
                        isLogin = true;
                        userMsg.Read();
                    }
                    else
                    {
                        context.Response.Redirect("/Home/Login");
                    }
                }
            }


            await requestDelegate.Invoke(context);
        }
    }
}
