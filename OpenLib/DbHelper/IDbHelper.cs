using System.Collections.Generic;
using System.Data;

namespace OpenLib.DbHelper
{
    public interface IDbHelper
    {
        void OpenDB();
        void CloseDB();

        DataTable ExecQuery(string sql);
        DataTable ExecParamQuery(string sql, params object[] paramArr);
        DataTable ExecNamedQuery(string sql, Dictionary<string, object> dt);

        int ExecNonQuery(string sql);
        int ExecParamNonQuery(string sql, params object[] paramArr);
        int ExecNamedNonQuery(string sql, Dictionary<string, object> dt);

        object ExecCreate(string sql);
        object ExecParamCreate(string sql, params object[] paramArr);
        object ExecNamedCreate(string sql, Dictionary<string, object> dt);

        object ExecScalar(string sql);
        object ExecParamScalar(string sql, params object[] paramArr);
        object ExecNamedScalar(string sql, Dictionary<string, object> dt);
    }
}
