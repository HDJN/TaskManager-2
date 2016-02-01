using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFramework.Taskmaneger.Model;
using ZF.Db.SqlHelper;
using System.Data.SqlClient;
using System.Data;

namespace TaskFramework.Taskmaneger.Dal
{
    public class TaskDal
    {
        public TaskModel GetById(string conn, string taskid)
        {
            TaskModel task = new TaskModel();
            string sql = "SELECT Id,TaskName,TaskDescription,CategoryId,NodeId,TaskCreateTime,TaskUpdateTime,TaskStartTime,TaskStopTime,TaskCron,TaskState,TaskClassPath,TaskFileName,TaskClassNamespace FROM Task WHERE Id=@taskid";
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@taskid", taskid) };
            DataTable dt = SqlServerHelper.Get(conn, sql, parameters);
            if (dt.Rows.Count > 0)
            {
                task = TaskModel.CreateModel(dt.Rows[0]);
            }
            return task;
        }

        public List<TaskModel> List(string conn)
        {
            List<TaskModel> list = new List<TaskModel>();
            string sql = "SELECT Id,TaskName,TaskDescription,CategoryId,NodeId,TaskCreateTime,TaskUpdateTime,TaskStartTime,TaskStopTime,TaskCron,TaskState,TaskClassPath,TaskFileName,TaskClassNamespace FROM Task";
            DataTable dt = SqlServerHelper.Get(conn, sql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TaskModel model = new TaskModel();
                    model = TaskModel.CreateModel(dt.Rows[i]);
                    list.Add(model);
                }
            }
            return list;
        }

        public void AddTask(string conn, TaskModel model)
        {
            string sql = @"INSERT INTO Task (TaskName,TaskDescription,CategoryId,NodeId,TaskCreateTime,TaskUpdateTime,TaskStartTime,TaskStopTime,TaskCron,TaskState,TaskClassPath,TaskFileName,TaskClassNamespace)
                         VALUES(@name
                               ,@description
                               ,@categoryid
                               ,@nodeid
                               ,@createtime
                               ,@updatetime
                               ,@starttime
                               ,@stoptime
                               ,@cron
                               ,@state
                               ,@classpath
                               ,@filename
                               ,@classnamespace)";
            SqlParameter[] paramters = new SqlParameter[]
            {
                new SqlParameter("@name",model.TaskName),
                new SqlParameter("@description",model.TaskDescription),
                new SqlParameter("@categoryid",model.CategoryId),
                new SqlParameter("@nodeid",model.NodeId),
                new SqlParameter("@createtime",model.TaskCreateTime),
                new SqlParameter("@updatetime",model.TaskUpdateTime),
                new SqlParameter("@starttime",model.TaskStartTime),
                new SqlParameter("@stoptime",model.TaskStopTime),
                new SqlParameter("@cron",model.TaskCron),
                new SqlParameter("@state",model.TaskState),
                new SqlParameter("@classpath",model.TaskClassPath),
                new SqlParameter("@filename",model.TaskFileName),
                new SqlParameter("@classnamespace",model.TaskClassNamespace)
            };
            SqlServerHelper.ExecuteNonQuery(conn, sql, paramters);
        }

        public void EditTask(string conn, TaskModel model)
        {
            string sql = @"UPDATE Task
                           SET TaskName = @name
                              ,TaskDescription = @descript
                              ,CategoryId = @categoryid
                              ,NodeId = @nodeid
                              ,TaskUpdateTime = @updatetime
                              ,TaskCron=@cron
                              ,TaskClassPath = @classpath
                              ,TaskFileName=@filename
                              ,TaskClassNamespace = @namespace WHERE Id = @id";
            SqlParameter[] paramters = new SqlParameter[]
            {
                new SqlParameter("@name",model.TaskName),
                new SqlParameter("@descript",model.TaskDescription),
                new SqlParameter("@categoryid",model.CategoryId),
                new SqlParameter("@nodeid",model.NodeId),
                new SqlParameter("@updatetime",model.TaskUpdateTime),
                new SqlParameter("@cron",model.TaskCron),
                new SqlParameter("@classpath",model.TaskClassPath),
                new SqlParameter("@filename",model.TaskFileName),
                new SqlParameter("@namespace",model.TaskClassNamespace),
                new SqlParameter("@id",model.Id)
            };
            SqlServerHelper.ExecuteNonQuery(conn, sql, paramters);
        }

        public void StartTask(string conn,string taskid)
        {
            string sql = @"UPDATE Task
                           SET TaskStartTime = @starttime
                               ,TaskState = @state WHERE Id = @id";
            SqlParameter[] paramters = new SqlParameter[]
            {
                new SqlParameter("@starttime",DateTime.Now),
                new SqlParameter("@state",1),
                new SqlParameter("@id",taskid)
            };
            SqlServerHelper.ExecuteNonQuery(conn, sql, paramters);
        }

        public void StopTask(string conn, string taskid)
        {
            string sql = @"UPDATE Task
                           SET TaskStopTime = @stoptime
                               ,TaskState = @state WHERE Id = @id";
            SqlParameter[] paramters = new SqlParameter[]
            {
                new SqlParameter("@stoptime",DateTime.Now),
                new SqlParameter("@state",0),
                new SqlParameter("@id",taskid)
            };
            SqlServerHelper.ExecuteNonQuery(conn, sql, paramters);
        }
    }
}
