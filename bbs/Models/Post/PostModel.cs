using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bbs.Models.Post
{
    public class PostModel
    {
        [StringLength(32, MinimumLength = 3, ErrorMessage = "标题格式不正确(3-32个字)")]
        [Required(ErrorMessage = "标题不能为空")]
        public string title { get; set; }

        [StringLength(10000, MinimumLength = 16, ErrorMessage = "内容格式不正确(16-10000个字)")]
        [Required(ErrorMessage = "请输入内容")]
        public string  content { get; set; }

        [Required(ErrorMessage = "发布的分区不能为空")]
        public string aid { get; set; }
    }
}
