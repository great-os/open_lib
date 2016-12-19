using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace OpenLib.DbHelper
{
    public class SqliteHelper : DbHelperBase
    {
        private SQLiteConnection m_Conn;
        private SQLiteCommand m_Command;
        public SqliteHelper() { }

        #region Private
        protected override void openConn()
        {
            m_Conn = new SQLiteConnection(ConnectionString);
            m_Conn.Open();
        }

        protected override void closeConn()
        {
            m_Conn.Close();
            m_Conn = null;
        }
        #endregion

        public DataTable ExecQuery(string sql)
        {
            DataTable dt = new DataTable("_SqliteHelper");
            if (m_AutoConnect)
            {
                openConn();
            }
            m_Command = new SQLiteCommand(sql, m_Conn);
            m_Command.CommandType = CommandType.Text;
            using (SQLiteDataReader dbReader = m_Command.ExecuteReader())
            {
                dt.Load(dbReader);
            }
            if (m_AutoConnect)
            {
                closeConn();
            }
            m_Command.Dispose();
            return dt;
        }

        public DataTable ExecParamQuery(string sql, params object[] paramArr)
        {
            DataTable dt = new DataTable("_SqliteHelper");
            if (m_AutoConnect)
            {
                openConn();
            }
            m_Command = new SQLiteCommand(sql, m_Conn);
            m_Command.CommandType = CommandType.Text;
            foreach (var item in paramArr)
            {
                SQLiteParameter param = m_Command.CreateParameter();
                param.Value = item;
                m_Command.Parameters.Add(param);
            }
            using (SQLiteDataReader dbReader = m_Command.ExecuteReader())
            {
                dt.Load(dbReader);
            }
            if (m_AutoConnect)
            {
                closeConn();
            }
            m_Command.Dispose();
            return dt;
        }

        public DataTable ExecNamedQuery(string sql, Dictionary<string, object> dt)
        {
            object[] paramArr=new object[]{};
            m_ProcessNameParams(ref sql, dt, ref paramArr);
            return ExecParamQuery(sql, paramArr);
        }

        public int ExecNonQuery(string sql)
        {
            int result;
            if (m_AutoConnect)
            {
                openConn();
            }
            m_Command = new SQLiteCommand(sql, m_Conn);
            m_Command.CommandType = CommandType.Text;
            result = m_Command.ExecuteNonQuery();
            if (m_AutoConnect)
            {
                closeConn();
            }
            m_Command.Dispose();
            return result;
        }

        public int ExecParamNonQuery(string sql, params object[] paramArr)
        {
            int result;
            if (m_AutoConnect)
            {
                openConn();
            }
            m_Command = new SQLiteCommand(sql, m_Conn);
            m_Command.CommandType = CommandType.Text;
            foreach (var item in paramArr)
            {
                SQLiteParameter param = m_Command.CreateParameter();
                param.Value = item;
                m_Command.Parameters.Add(param);
            }
            result = m_Command.ExecuteNonQuery();
            if (m_AutoConnect)
            {
                closeConn();
            }
            m_Command.Dispose();
            return result;
        }

        public int ExecNamedNonQuery(string sql, Dictionary<string, object> dt)
        {
            object[] paramArr = new object[] { };
            m_ProcessNameParams(ref sql, dt, ref paramArr);
            return ExecParamNonQuery(sql, paramArr);
        }

        public object ExecCreate(string sql)
        {
            throw new NotImplementedException();
        }

        public object ExecParamCreate(string sql, params object[] paramArr)
        {
            throw new NotImplementedException();
        }

        public object ExecNamedCreate(string sql, Dictionary<string, object> dt)
        {
            throw new NotImplementedException();
        }

        public object ExecScalar(string sql)
        {
            object result;
            if (m_AutoConnect)
            {
                openConn();
            }
            m_Command = new SQLiteCommand(sql, m_Conn);
            m_Command.CommandType = CommandType.Text;
            result = m_Command.ExecuteScalar();
            if (m_AutoConnect)
            {
                closeConn();
            }
            m_Command.Dispose();
            return result;
        }

        public object ExecParamScalar(string sql, params object[] paramArr)
        {
            object result;
            if (m_AutoConnect)
            {
                openConn();
            }
            m_Command = new SQLiteCommand(sql, m_Conn);
            m_Command.CommandType = CommandType.Text;
            foreach (var item in paramArr)
            {
                SQLiteParameter param = m_Command.CreateParameter();
                param.Value = item;
                m_Command.Parameters.Add(param);
            }
            result = m_Command.ExecuteScalar();
            if (m_AutoConnect)
            {
                closeConn();
            }
            m_Command.Dispose();
            return result;
        }

        public object ExecNamedScalar(string sql, Dictionary<string, object> dt)
        {
            object[] paramArr = new object[] { };
            m_ProcessNameParams(ref sql, dt, ref paramArr);
            return ExecParamScalar(sql, paramArr);
        }
    }
}
