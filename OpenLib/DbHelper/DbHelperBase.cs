using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OpenLib.DbHelper
{
    public class DbHelperBase
    {
        protected string m_ConnString;
        protected string m_FilePath;
        protected bool m_AutoConnect;

        #region ClassBase
        public DbHelperBase()
        {
            m_ConnString = "";
            m_FilePath = "";
            m_AutoConnect = true;
        }
        #endregion

        #region Property
        public string ConnectionString
        {
            get
            {
                return m_ConnString;
            }
            set
            {
                m_ConnString = value;
            }
        }
        public bool IsReady
        {
            get
            {
                return ConnectionString.Length > 0;
            }
        }
        #endregion

        #region Protected Methods
        protected virtual void openConn()
        {
            m_AutoConnect = false;
            openConn();
        }
        protected virtual void closeConn()
        {
            m_AutoConnect = true;
            closeConn();
        }

        protected virtual int m_ApostropheCount(string sql)
        {
            return sql.Length - sql.Replace("'", "").Length;
        }

        protected bool m_ProcessNameParams(ref string sql, Dictionary<string, object> dic, ref object[] paramArr)
        {
            bool beginParam = false;
            string fieldName = "", word = "";
            int comaCount = 0;
            List<object> paramList = new List<object>();
            object paramValue;

            StringBuilder sb = new StringBuilder("");
            if (m_ApostropheCount(sql) % 2 == 1)
            {
                throw new Exception("Symbal \"'\" must be in pairs,please check SQL statement");
            }
            for (int i = 0; i < sql.Length; i++)
            {
                word = sql.Substring(i, 1);
                switch (word)
                {
                    case " ":
                    case ",":
                    case ")":
                    case ";":
                        {
                            sb.Append(word);
                            if (beginParam)
                            {
                                if (!dic.TryGetValue(fieldName,out paramValue))
                                {
                                    throw new Exception("Can't find keyname match of Param " + fieldName);
                                }
                                fieldName = "";
                                paramList.Add(paramValue);
                            }
                            beginParam = false;
                            break;
                        }
                    case "'":
                        {
                            comaCount += 1;
                            sb.Append(word);
                            break;
                        }
                    case "@":
                        {
                            if (comaCount % 2 == 0)
                            {
                                beginParam = true;
                                sb.Append("?");
                            }
                            else
                            {
                                sb.Append(word);
                            }
                            break;
                        }
                    default:
                        {
                            if (beginParam == false)
                            {
                                sb.Append(word);
                            }
                            else
                            {
                                fieldName = fieldName + word;
                            }
                            break;
                        }
                }
            }
            if (fieldName.Length>0)
            {
                if (!dic.TryGetValue(fieldName, out paramValue))
                {
                    throw new Exception("Can't find keyname match of Param " + fieldName);
                }
                fieldName = "";
                paramList.Add(paramValue);
            }
            sql = sb.ToString();
            paramArr = paramList.ToArray();
            return true;
        }
        #endregion

        #region Public Methods
        public virtual void OpenDB()
        {
            m_AutoConnect = false;
            openConn();
        }

        public virtual void CloseDB()
        {
            m_AutoConnect = true;
            closeConn();
        }
        #endregion
    }
}
