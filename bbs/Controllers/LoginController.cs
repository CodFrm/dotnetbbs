﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bbs.Lib;
using bbs.Models;
using bbs.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace bbs.Controllers
{
    public class LoginController : Controller
    {
        public JsonResult Login(string username, string password)
        {
            var data = Db.table("user").where("username", username)._or().where("uid", username).find();
            if (data.HasRows)
            {
                data.Read();
                if (data[2].ToString() == password)
                {
                    HttpContext.Response.Cookies.Append("token",TokenModel.createToken((int)data[0]));
                    return Json(new ErrorJsonModel(0, "success"));
                }
                return Json(new ErrorJsonModel(10005, "密码不正确"));
            }
            return Json(new ErrorJsonModel(10001, "不存在的用户"));
        }

        public JsonResult Register(string user, string passwd, string confirm)
        {
            var m = new RegisterUserModel
            {
                user = user,
                passwd = passwd,
                confirm = confirm
            };
            var a = TryValidateModel(m);
            if (ModelState.IsValid)
            {
                var data = Db.table("user").where("username", user)._or().where("uid", user).find();
                if (data.HasRows)
                {
                    return Json(new ErrorJsonModel(-1, "用户名存在"));
                }
                var userData = new Dictionary<string, object>();
                userData.Add("username", user);
                userData.Add("password", passwd);
                userData.Add("reg_time", Functions.timestamp());
                Db.table("user").insert(userData);
                return Json(new ErrorJsonModel(0, "注册成功"));

            }
            return Json(new ErrorJsonModel(-1, getErrorMsg(ModelState)));
        }

        private string getErrorMsg(ModelStateDictionary modelStateDictionary)
        {
            string errorMsg = "";
            foreach (var key in ModelState.Keys.ToList())
            {
                var errors = modelStateDictionary[key].Errors.ToList();
                if (modelStateDictionary[key].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    errorMsg = modelStateDictionary[key].Errors[0].ErrorMessage;
                    break;
                }
            }
            return errorMsg;
        }
    }
}