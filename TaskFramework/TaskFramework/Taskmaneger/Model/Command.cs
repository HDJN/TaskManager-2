using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZF.Db;

namespace TaskFramework.Taskmaneger.Model
{
    public class Command
    {
        public long Id { get; set; }
        public long TaskId { get; set; }
        public long NodeId { get; set; }
        /// <summary>
        /// 命令类型
        /// </summary>
        public CommandType CommandType { get; set; }
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 命令执行状态 1成功 -1执行不成功 0未执行
        /// </summary>
        public int State { get; set; }

        public static Command CreateModel(DataRow dr)
        {
            Command cmd = new Command();
            if (dr.Table.Columns.Contains("Id"))
            {
                cmd.Id = LibConvert.ObjToInt64(dr["Id"]);
            }
            if (dr.Table.Columns.Contains("TaskId"))
            {
                cmd.TaskId = LibConvert.ObjToInt64(dr["TaskId"]);
            }
            if (dr.Table.Columns.Contains("NodeId"))
            {
                cmd.NodeId = LibConvert.ObjToInt64(dr["NodeId"]);
            }
            if (dr.Table.Columns.Contains("CommandType"))
            {
                cmd.CommandType = (CommandType)LibConvert.ObjToInt(dr["CommandType"]);
            }
            if (dr.Table.Columns.Contains("CreateTime"))
            {
                cmd.CreateTime = LibConvert.ObjToDateTime(dr["CreateTime"]);
            }
            if (dr.Table.Columns.Contains("State"))
            {
                cmd.State = LibConvert.ObjToInt(dr["State"]);
            }
            return cmd;
        }
    }

    public enum CommandType
    {
        Stop = 0,
        Start = 1
    }
}
