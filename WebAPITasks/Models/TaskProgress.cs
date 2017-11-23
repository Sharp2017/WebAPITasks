using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPITasks.Models
{
    public class taskProgress
    {
        private string _name;
        /// <summary>
        /// 活动名称
        /// </summary>
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        private int _status;
        /// <summary>
        /// 整个生产任务的状态，未开始0、进行中1、已终止2、已完成3 
        /// </summary>
        public int status
        {
            get { return _status; }
            set { _status = value; }
        }

        private string _extraDatas;
        /// <summary>
        /// 整个生产任务的附加数据包括如异常反馈等 
        /// </summary>
        public string extraDatas
        {
            get { return _extraDatas; }
            set { _extraDatas = value; }
        }

        private List<activitie> _activities;
        /// <summary>
        /// 生产任务中的多个活动节点 
        /// </summary>
        public List<activitie> activities
        {
            get { return _activities; }
            set { _activities = value; }
        }
    }
}