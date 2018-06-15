using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using bbs.Lib;
using System.Threading.Tasks;

namespace bbs.Models.User
{
    public class UserPostsModel
    {
        public static ArrayList myPostList(int uid, int limit = 10)
        {
            ArrayList arrayList = new ArrayList();
            using (var db = Db.table("post").where("post_uid", uid).limit(5).order("pid"))
            {
                using (var data = db.select())
                {
                    while (data.Read())
                    {
                        ArrayList tmp = new ArrayList();
                        tmp.Add(data["pid"]);
                        tmp.Add(data["post_title"]);
                        arrayList.Add(tmp);
                    }
                }
            }
            return arrayList;

        }
    }
}
