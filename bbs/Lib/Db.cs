﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bbs.Lib
{
    public class Db :IDisposable
    {
        protected MySqlConnection sqlConnection = null;
        protected MySqlCommand sqlCommand;
        protected long lastId;

        public static string _fix = "bbs_";

        public String _table = "";
        public String _where = "";
        public String _operator = "and";
        public String _limit = "";
        public String _order = "";
        public String _join = "";
        public String _field = "*";

        public Db(String table)
        {
            initConnect();
            sqlCommand = new MySqlCommand("", sqlConnection);
            sqlCommand.Parameters.Clear();
            _table = table;
        }

        public void Dispose()
        {
            Close();
        }

        public void initConnect()
        {
            sqlConnection = new MySqlConnection("server=localhost;port=3306;database=bbs;user=root;password=;sslmode=none;charset=utf8;");
            sqlConnection.Open();
        }

        public void Close()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
            sqlConnection = null;
        }

        public static Db table(String table)
        {
            return new Db(_fix + table);
        }

        public void initMember()
        {
            _where = "";
        }

        public Db where(String where)
        {
            if (_where != "")
            {
                _where += " " + _operator + " ";
                _operator = "and";
            }
            else
            {
                _where += "where ";
            }
            _where += where;
            return this;
        }

        public Db where(Dictionary<String, object> valuePairs)
        {
            foreach (var key in valuePairs)
            {
                this.where(key.Key, key.Value);
            }
            return this;
        }

        public Db _or()
        {
            _operator = "or";
            return this;
        }

        public Db where(String Key, object Value)
        {

            this.where("`" + Key + "`=@" + Key);
            sqlCommand.Parameters.Add(new MySqlParameter("@" + Key, Value));
            return this;
        }

        public ResultCollection find()
        {
            sqlCommand.CommandText = "select "+_field+" from " + _table + " " + _join + " " + _where + " limit 1";
            ResultCollection dataReader = new ResultCollection(sqlCommand.ExecuteReader());
            initMember();
            return dataReader;
        }
       

        public Db field(String field)
        {
            _field = field;
            return this;
        }

        public ResultCollection select()
        {
            sqlCommand.CommandText = "select "+_field+" from " + _table + " " + _join + " " + _where + " " + _order + " " + _limit;
            ResultCollection dataReader = new ResultCollection(sqlCommand.ExecuteReader());
            initMember();
            return dataReader;
        }

        public Db join(string table, string on, string type = "inner")
        {
            _join += type + " join " + _fix + table + " on " + on;
            return this;
        }

        public Db order(string field, string mode = "desc")
        {
            _order = "order by " + field + " " + mode;
            return this;
        }

        public Db limit(int n, int m)
        {
            _limit = "limit " + n.ToString() + "," + m.ToString();
            return this;
        }

        public Db limit(int n)
        {
            _limit = "limit " + n.ToString();
            return this;
        }

        public int insert(Dictionary<String, object> valuePairs)
        {
            sqlCommand.CommandText = "insert into " + _table;
            sqlCommand.Parameters.Clear();
            sqlCommand.CommandText += "(";
            String val = "";
            foreach (var key in valuePairs)
            {
                sqlCommand.CommandText += "`" + key.Key + "`,";
                val += "@" + key.Key + ",";
                sqlCommand.Parameters.Add(new MySqlParameter("@" + key.Key, key.Value));
            }
            sqlCommand.CommandText = sqlCommand.CommandText.Substring(0, sqlCommand.CommandText.Length - 1);
            val = val.Substring(0, val.Length - 1);
            sqlCommand.CommandText += ")";
            sqlCommand.CommandText += "values(" + val + ")";
            int number = sqlCommand.ExecuteNonQuery();
            initMember();
            lastId = sqlCommand.LastInsertedId;
            return number;
        }

        public long lastInsertId()
        {
            return lastId;
        }

        public int update(Dictionary<String, object> valuePairs)
        {

            sqlCommand.CommandText = "update  " + _table + " set ";
            foreach (var key in valuePairs)
            {
                sqlCommand.CommandText += "`" + key.Key + "`=@" + key.Key + "u,";
                sqlCommand.Parameters.Add(new MySqlParameter("@" + key.Key + "u", key.Value));
            }
            sqlCommand.CommandText = sqlCommand.CommandText.Substring(0, sqlCommand.CommandText.Length - 1);
            sqlCommand.CommandText += " " + _where;
            int number = sqlCommand.ExecuteNonQuery();
            initMember();
            return number;
        }

    }
}
