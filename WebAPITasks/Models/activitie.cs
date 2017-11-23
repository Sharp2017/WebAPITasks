using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPITasks.Models
{
    public class activitie
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
        /// 单个活动的状态，未开始0、进行中1、已终止2、已完成3 
        /// </summary>
        public int status
        {
            get { return _status; }
            set { _status = value; }
        }

        private List<extraData> _extraDatas;
        /// <summary>
        /// 单个活动的附加数据如活动执行者，审批意见，计划执行者，异常反馈等 
        /// </summary>
        public List<extraData> ExtraDatas
        {
            get { return _extraDatas; }
            set { _extraDatas = value; }
        }

    }
}