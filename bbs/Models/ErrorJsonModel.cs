using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bbs.Models
{
    public class ErrorJsonModel
    {
        public int code;
        public string msg;
        public ErrorJsonModel(int code,string msg)
        {
            this.code = code;
            this.msg = msg;
        }

    }
}
