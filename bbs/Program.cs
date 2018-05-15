using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using bbs.Lib;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace bbs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Db.initConnect();
            //MySqlDataReader reader = Db.table("jx_token").where("uid","1").select();
            //String text = "";
            //while (reader.Read())
            //{
            //    text += reader.GetValue(1).ToString();
            //}
            //Dictionary<String, object> data = new Dictionary<String, object>();
            //data.Add("uid",233);
            //data.Add("token","wdqweq");
            //data.Add("time",13432);
            //reader.Close();
            ////Db.table("jx_token").insert(data);
            //Db.table("jx_token").where("uid",233).update(data);
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
