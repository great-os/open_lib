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

        // protected bool m_ProcessNameParams(string sql, Dictionary<string, object> dic, object[] paramArr)
        // {
        //     bool beginParam = false;
        //     string fieldName = "", word = "";
        //     int paramCount = 0;
        //     StringBuilder sb = new StringBuilder("");
        //     if (m_ApostropheCount(sql) % 2 == 1)
        //     {
        //         throw new Exception("Symbal \"'\" must be in pairs,please check SQL statement");
        //     }
        //     for (int i = 0; i < sql.Length; i++)
        //     {
        //         word = sql.Substring(i, 1);
        //         switch (word)
        //         {
        //             case " ":
        //             case ",":
        //             case ")":
        //                 {
        //                     if (beginParam)
        //                     {
                                
        //                     }
        //                     break;
        //                 }
        //                 break;
        //             default:
        //                 break;
        //         }
        //     }
        //     return true;
        // }

// Private Function m_ProcessNameParams(mSql As String, mDic As CHashTable, mParams() As Variant) As Boolean
//   Dim mNewSql As String, mWord As String, mFieldName As String
//   Dim mParamCount As Long, i As Long, comaCount As Long
//   Dim mBeginParam As Boolean
  
//   If m_ApostropheCount(mSql) Mod 2 = 1 Then
//     Err.Raise 110000000, "Symbal " '" must be in pairs,please check SQL statement"
//   End If
  
//   'init mDic
//   mBeginParam = False
//   mFieldName = ""
//   mParamCount = 0
  
//   For i = 1 To Len(mSql)
//     mWord = Mid(mSql, i, 1)
//     Select Case mWord
//       Case " ", ",", ")"
//         mNewSql = mNewSql & mWord
//         If mBeginParam Then
//           ReDim Preserve mParams(mParamCount)
//           mParams(mParamCount) = mDic.Item(mFieldName)
//           mFieldName = ""
//           mParamCount = mParamCount + 1
//         End If
//         mBeginParam = False
//       Case "'"
//         comaCount = comaCount + 1
//         mNewSql = mNewSql & mWord
//       Case "@"
//         If comaCount Mod 2 = 0 Then
//           mBeginParam = True
//           mNewSql = mNewSql & "?"
//         Else
//           'odd number of "'" means that "@" is only string of content
//           mNewSql = mNewSql & mWord
//         End If
//       Case Else
//         If mBeginParam = False Then
//           mNewSql = mNewSql & mWord
//         Else
//           mFieldName = mFieldName & mWord
//         End If
//     End Select
//   Next i
//   'all done but check last word for that last word maybe param
//   If mFieldName <> "" Then
//     ReDim Preserve mParams(mParamCount)
//     mParams(mParamCount) = mDic.Item(mFieldName)
//     mFieldName = ""
//   End If
//   'return
//   mSql = mNewSql
//   m_ProcessNameParams = True
// End Function
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
