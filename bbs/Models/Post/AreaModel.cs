using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bbs.Models.Post
{
    public class AreaModel
    {
        public AreaModel(int aid,string name)
        {
            this.aid = aid;
            this.name = name;
        }
        public int aid { get; set; }
        public string name { get; set; }
    }
}
