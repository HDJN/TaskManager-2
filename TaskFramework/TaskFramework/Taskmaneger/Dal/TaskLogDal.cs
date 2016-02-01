using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFramework.Taskmaneger.Model;
using System.Data.SqlClient;
using ZF.Db.SqlHelper;

namespace TaskFramework.Taskmaneger.Dal
{
    public class TaskLogDal
    {
        public static void Add(string conn, TaskLog log)
        {
            string sql = @"INSERT INTO TaskLog (LogMsg,TaskId,NodeId,CreateTime)
                            VALUES (@msg,@taskId,@nodeId,@createtime)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@msg",log.LogMsg),
                new SqlParameter("@taskId",log.TaskId),
                new SqlParameter("@nodeId",log.NodeId),
                new SqlParameter("@createtime",log.CreateTime)
            };
            SqlServerHelper.ExecuteNonQuery(conn, sql, parameters);
        }
    }
}
