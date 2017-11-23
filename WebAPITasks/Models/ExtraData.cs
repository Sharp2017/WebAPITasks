using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPITasks.Models
{
    public class extraData
    {
        private string _name;
        /// <summary>
        /// 附加数据名
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// 附加数据标题
        /// </summary>
        private string _caption;

        public string caption
        {
            get { return _caption; }
            set { _caption = value; }
        }

        private string _value;
        /// <summary>
        /// 附加数据值 
        /// </summary>
        public string value
        {
            get { return this._value; }
            set { this._value = value; }
        }
    }
}