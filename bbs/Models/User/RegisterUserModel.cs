using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bbs.Models.User
{
    public class RegisterUserModel
    {
        [StringLength(10, MinimumLength = 3,ErrorMessage ="用户名格式不正确(3-10字符)")]
        [Required(ErrorMessage ="用户名不能为空")]
        public string user { get; set; }

        [RegularExpression(@"^[\w]{6,16}$",ErrorMessage ="密码不正确(6-16个字符)")]
        [Required(ErrorMessage ="请输入密码")]
        public string passwd { get; set; }

        [Compare("passwd", ErrorMessage = "两次输入的密码不同")]
        [Required(ErrorMessage ="确认密码不能为空")]
        public string confirm { get; set; }
    }
}
