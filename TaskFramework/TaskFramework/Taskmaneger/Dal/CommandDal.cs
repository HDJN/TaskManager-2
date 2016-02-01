using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFramework.Taskmaneger.Model;
using System.Data.SqlClient;
using ZF.Db.SqlHelper;
using System.Data;

namespace TaskFramework.Taskmaneger.Dal
{
    public class CommandDal
    {
        public Command Get(string conn, long id)
        {
            Command cmd = new Command();
            string sql = @"SELECT Id,TaskId,NodeId,CommandType,CreateTime,State FROM Command WHERE Id=@id";
            SqlParameter[] paramters = new SqlParameter[] { new SqlParameter("@id", id) };
            DataTable dt = SqlServerHelper.Get(conn, sql, paramters);
            if (dt.Rows.Count > 0)
            {
                cmd = Command.CreateModel(dt.Rows[0]);
            }
            else
            {
                cmd = null;
            }
            return cmd;
        }

        public void Edit(string conn, Command cmd)
        {
            string sql = "UPDATE Command SET CommandType = @cmdtype,State=@state WHERE Id=@id";
            SqlParameter[] paramters = new SqlParameter[]
            {
                new SqlParameter("@cmdtype",(int)cmd.CommandType),
                new SqlParameter("@state",cmd.State),
                new SqlParameter("@id",cmd.Id)
            };
            SqlServerHelper.ExecuteNonQuery(conn, sql, paramters);
        }

        public void Delete(string conn, long id)
        {
            string sql = "DELETE FROM Command WHERE Id=@id";
            SqlParameter[] paramters = new SqlParameter[]
            {
                new SqlParameter("@id",id)
            };
            SqlServerHelper.ExecuteNonQuery(conn, sql, paramters);
        }

        public void UpdateByTaskId(string conn, long nodeid, int state)
        {
            string sql = "UPDATE Command SET State=@state WHERE NodeId=@nodeid";
            SqlParameter[] paramters = new SqlParameter[]
            {
                new SqlParameter("@state",state),
                new SqlParameter("@nodeid",nodeid)
            };
            SqlServerHelper.ExecuteNonQuery(conn, sql, paramters);
        }
    }
}
