using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPITasks.Models
{
    public class extraDatas
    {
        private string _name;
        /// <summary>
        /// 属性名
        /// </summary>
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _caption;
        /// <summary>
        /// 属性说明
        /// </summary>
        public string caption
        {
            get { return _caption; }
            set { _caption = value; }
        }
        private string _value;
        /// <summary>
        /// 属性值
        /// </summary>
        public string value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
    /// <summary>
    /// 用户信息，用户用户的更新
    /// </summary>
    public class userInfo
    {

        private string _userID;
        /// <summary>
        /// 工号
        /// </summary>
        public string userID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        private string _userName;

        public string userName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        private string _password;

        public string password
        {
            get { return _password; }
            set { _password = value; }
        }


        private string _salt;
        /// <summary>
        /// 加密参数
        /// </summary>
        public string salt
        {
            get { return _salt; }
            set { _salt = value; }
        }

        private string _status;

        public string status
        {
            get { return _status; }
            set { _status = value; }
        }

        private string _CRUD;

        public string CRUD
        {
            get { return _CRUD; }
            set { _CRUD = value; }
        }
        private extraDatas[] _extraDatas;

        public extraDatas[] extraDatas
        {
            get { return _extraDatas; }
            set { _extraDatas = value; }
        }
    }
}