using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFramework.Taskmaneger.Model
{
    public partial class tb_version_model
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int taskid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int version { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime versioncreatetime { get; set; }

        /// <summary>
        /// 压缩文件二进制文件
        /// </summary>
        public byte[] zipfile { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zipfilename { get; set; }

    }
}
