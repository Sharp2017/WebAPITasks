using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPITasks.Models
{
    public class userInfoUpdate
    {
        private string _updateUserID;
        /// <summary>
        /// 更新操作用户的ID
        /// </summary>
        public string updateUserID
        {
            get { return _updateUserID; }
            set { _updateUserID = value; }
        }

        private string _updateUserName;
        /// <summary>
        /// 更新操作用户的名字
        /// </summary>
        public string updateUserName
        {
            get { return _updateUserName; }
            set { _updateUserName = value; }
        }
        private string _updateIP;
        /// <summary>
        /// 更新用户的ＩＰ地址
        /// </summary>
        public string updateIP
        {
            get { return _updateIP; }
            set { _updateIP = value; }
        }

        private userInfo[] _updateUserInfo;
        /// <summary>
        /// 更新的信息
        /// </summary>
        public userInfo[] updateUserInfo
        {
            get { return _updateUserInfo; }
            set { _updateUserInfo = value; }
        }
    }
}