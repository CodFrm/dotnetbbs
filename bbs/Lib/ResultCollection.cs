using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bbs.Lib
{
    public class ResultCollection : IDisposable
    {
        private MySqlDataReader mySqlDataReader;
        public ResultCollection(MySqlDataReader mySqlDataReader)
        {
            this.mySqlDataReader = mySqlDataReader;
        }

        ~ResultCollection()
        {
            Close();
        }
        public void Dispose()
        {
            Close();
        }
        public void Close()
        {
            if (!mySqlDataReader.IsClosed)
            {
                mySqlDataReader.Close();
            }
        }

        public string this[string key]
        {
            get { return mySqlDataReader.GetString(mySqlDataReader.GetOrdinal(key)); }
        }

        public object this[int key]
        {
            get { return mySqlDataReader[key]; }
        }

        public bool HasRows { get { return mySqlDataReader.HasRows; } }

        public bool Read()
        {
            return mySqlDataReader.Read();
        }

        public object GetValue(int i)
        {
            return mySqlDataReader.GetValue(i);
        }
    }
}
