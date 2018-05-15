using bbs.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bbs.Models.User
{
    public class TokenModel
    {
        public static string createToken(int uid)
        {
            string token = Functions.randString(16);
            var valuePairs = new Dictionary<string, object>();
            valuePairs.Add("token", token);
            valuePairs.Add("uid", uid);
            valuePairs.Add("time", Functions.timestamp());
            Db.table("user_token").insert(valuePairs);
            return token;
        }
    }
}
