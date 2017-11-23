using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPITasks.Models
{
    public class workTaskDataInfo
    {
        private string _workItemid = "";

        /// <summary>
        /// 工作流程引擎中的工作项主键，生产商需要对其进行持久化。在查询任务进度、结束生产任务时会使用到
        /// </summary>
        public string workItemID
        {
            get { return _workItemid; }
            set { _workItemid = value; }
        }

        private string _programid = "";

        public string programID
        {
            get { return _programid; }
            set { _programid = value; }
        }

        private DateTime _workcreatedate;
        /// <summary>
        /// 记录添加的时间
        /// </summary>
        public DateTime workcreatedate
        {
            get { return _workcreatedate; }
            set { _workcreatedate = value; }
        }

        private string _assetname = "";
        /// <summary>
        /// 标题 
        /// </summary>
        public string assetName
        {
            get { return _assetname; }
            set { _assetname = value; }
        }


        private string _columnname = "";
        /// <summary>
        ///  栏目 
        /// </summary>
        public string columnName
        {
            get { return _columnname; }
            set { _columnname = value; }
        }

        private DateTime _playdate = new DateTime(1900, 1, 1);

        /// <summary>
        /// 预播日期 
        /// </summary>
        public DateTime playDate
        {
            get { return _playdate; }
            set { _playdate = value; }
        }

        private string _tags = "";

        /// <summary>
        /// 标签
        /// </summary>
        public string tags
        {
            get { return _tags; }
            set { _tags = value; }
        }

        private string _description = "";
        /// <summary>
        /// 描述
        /// </summary>
        public string description
        {
            get { return _description; }
            set { _description = value; }
        }

        private string _content = "";
        /// <summary>
        /// 内容
        /// </summary>
        public string content
        {
            get { return _content; }
            set { _content = value; }
        }


        private string _channelpath = "";

        /// <summary>
        /// 频道
        /// </summary>
        public string channelPath
        {
            get { return _channelpath; }
            set { _channelpath = value; }
        }

        private DateTime _modifiedDate = DateTime.Now;
        /// <summary>
        /// 修改日期
        /// </summary>

        public DateTime modifiedDate
        {
            get { return _modifiedDate; }
            set { _modifiedDate = value; }
        }

        private string _modifiedby = "";
        /// <summary>
        /// 修改人
        /// </summary>
        public string modifiedBy
        {
            get { return _modifiedby; }
            set { _modifiedby = value; }
        }

        private DateTime _creationdate = DateTime.Now;

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime creationDate
        {
            get { return _creationdate; }
            set { _creationdate = value; }
        }

        private string _createdby = "";
        /// <summary>
        /// 创建人
        /// </summary>
        public string createdBy
        {
            get { return _createdby; }
            set { _createdby = value; }
        }

        private List<string> _materialDrefs = null;
        /// <summary>
        /// 天马素材库的关联关系，结束生产任务时，需要维护：先清空，再成品关联进来
        /// </summary>
        public List<string> materialDrefs
        {
            get { return _materialDrefs; }
            set { _materialDrefs = value; }
        }


        private int _taskstatus;
        /// <summary>
        /// 任务状态，是否开始制作
        /// </summary>
        public int taskStatus
        {
            get { return _taskstatus; }
            set { _taskstatus = value; }
        }
        private string _broadcastCode="";
        /// <summary>
        /// 播出代码 目前为绑定的RCS播出代码
        /// </summary>
        public string broadcastCode
        {
            get { return _broadcastCode; }
            set { _broadcastCode = value; }
        }

        private int _isBroadcast=1;
        /// <summary>
        /// 是否播出 0：不播出，1：播出
        /// </summary>
        public int isBroadcast
        {
            get { return _isBroadcast; }
            set { _isBroadcast = value; }
        }
    }
}