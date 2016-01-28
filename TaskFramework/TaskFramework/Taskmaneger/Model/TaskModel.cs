using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZF.Db;

namespace TaskFramework.Taskmaneger.Model
{
    public class TaskModel
    {
        public long Id { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public long CategoryId { get; set; }
        public long NodeId { get; set; }
        public DateTime TaskCreateTime { get; set; }
        public DateTime? TaskUpdateTime { get; set; }
        public DateTime? TaskStartTime { get; set; }
        public DateTime? TaskStopTime { get; set; }
        public string TaskCron { get; set; }
        /// <summary>
        /// 任务状态 1为正在运行 0为停止
        /// </summary>
        public int TaskState { get; set; }
        public string TaskClassPath { get; set; }
        public string TaskClassNamespace { get; set; }

        public static TaskModel CreateModel(DataRow dr)
        {
            TaskModel model = new TaskModel();
            if (dr.Table.Columns.Contains("Id"))
            {
                model.Id = LibConvert.ObjToInt64(dr["Id"]);
            }
            if (dr.Table.Columns.Contains("TaskName"))
            {
                model.TaskName = LibConvert.ObjToStr(dr["TaskName"]);
            }
            if (dr.Table.Columns.Contains("TaskDescription"))
            {
                model.TaskDescription = LibConvert.ObjToStr(dr["TaskDescription"]);
            }
            if (dr.Table.Columns.Contains("CategoryId"))
            {
                model.CategoryId = LibConvert.ObjToInt64(dr["CategoryId"]);
            }
            if (dr.Table.Columns.Contains("NodeId"))
            {
                model.NodeId = LibConvert.ObjToInt64(dr["NodeId"]);
            }
            if (dr.Table.Columns.Contains("TaskCreateTime"))
            {
                model.TaskCreateTime = LibConvert.ObjToDateTime(dr["TaskCreateTime"]);
            }
            if (dr.Table.Columns.Contains("TaskUpdateTime"))
            {
                model.TaskUpdateTime = LibConvert.ObjToDateTimeOrNull(dr["TaskUpdateTime"]);
            }
            if (dr.Table.Columns.Contains("TaskStartTime"))
            {
                model.TaskStartTime = LibConvert.ObjToDateTimeOrNull(dr["TaskStartTime"]);
            }
            if (dr.Table.Columns.Contains("TaskStopTime"))
            {
                model.TaskStopTime = LibConvert.ObjToDateTimeOrNull(dr["TaskStopTime"]);
            }
            if (dr.Table.Columns.Contains("TaskCron"))
            {
                model.TaskCron = LibConvert.ObjToStr(dr["TaskCron"]);
            }
            if (dr.Table.Columns.Contains("TaskState"))
            {
                model.TaskState = LibConvert.ObjToInt(dr["TaskState"]);
            }
            if (dr.Table.Columns.Contains("TaskClassPath"))
            {
                model.TaskClassPath = LibConvert.ObjToStr(dr["TaskClassPath"]);
            }
            if (dr.Table.Columns.Contains("TaskClassNamespace"))
            {
                model.TaskClassNamespace = LibConvert.ObjToStr(dr["TaskClassNamespace"]);
            }
            return model;
        }
    }
}
