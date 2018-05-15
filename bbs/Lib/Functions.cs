using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bbs.Lib
{
    public class Functions
    {

        /// <summary>
        /// 获取当前时间戳
        /// </summary>
        /// <returns></returns>
        public static string timestamp()
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            long timeStamp = (long)(DateTime.Now - startTime).TotalSeconds; // 相差秒数
            return timeStamp.ToString();
        }
    }
}
