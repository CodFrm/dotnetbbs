using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using bbs.Models;
using bbs.Lib;
using Microsoft.AspNetCore.Http;
using bbs.Models.User;

namespace bbs.Controllers
{
    public class HomeController : AuthController
    {
        public IActionResult Index()
        {
            if (isLogin) { ViewData["myPost"] = UserPostsModel.myPostList((int)ViewData["uid"]); }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

    }
}
