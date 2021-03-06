﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bbs.Lib;
using bbs.Models;
using bbs.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace bbs.Controllers
{
    public class LoginController : AuthController
    {
        public JsonResult Login(string username, string password)
        {
            using (var db = Db.table("user"))
            {
                using (var data = Db.table("user").where("username", username)._or().where("uid", username).find())
                {
                    if (data.HasRows)
                    {
                        data.Read();
                        if (data[2].ToString() == password)
                        {
                            int uid = (int)data[0];
                            HttpContext.Response.Cookies.Append("token", TokenModel.createToken(uid));
                            HttpContext.Response.Cookies.Append("uid", uid.ToString());
                            return Json(new ErrorJsonModel(0, "success"));
                        }
                        return Json(new ErrorJsonModel(10005, "密码不正确"));
                    }
                }
            }
            return Json(new ErrorJsonModel(10001, "不存在的用户"));
        }

        public JsonResult Register(string user, string passwd, string confirm)
        {
            var m = new RegisterUserModel
            {
                user = Functions.FilterXSS(user),
                passwd = passwd,
                confirm = confirm
            };
            TryValidateModel(m);
            if (ModelState.IsValid)
            {
                using (Db db = Db.table("user"), db1 = Db.table("user"))
                {
                    using (var data = db.where("username", user)._or().where("uid", user).find())
                    {
                        if (data.HasRows)
                        {
                            return Json(new ErrorJsonModel(-1, "用户名存在"));
                        }
                        var userData = new Dictionary<string, object>();
                        userData.Add("username", m.user);
                        userData.Add("password", passwd);
                        userData.Add("reg_time", Functions.timestamp());
                        db1.insert(userData);
                    }
                }
                return Json(new ErrorJsonModel(0, "注册成功"));
            }
            return Json(new ErrorJsonModel(-1, Functions.getErrorMsg(ModelState)));
        }

    }
}