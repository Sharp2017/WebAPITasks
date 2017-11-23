using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPITasks.Models
{
    public class resultInfo
    {
        private int _code=-1;

        /// <summary>
        /// 
        /// </summary>
        public int code
        {
            get { return _code; }
            set { _code = value; }
        }

        private string _message;
        /// <summary>
        /// Code非0时返回的错误详情
        /// </summary>
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }
        private resultInfo _result;

        public resultInfo result
        {
            get { return _result; }
            set { _result = value; }
        }
    }
}