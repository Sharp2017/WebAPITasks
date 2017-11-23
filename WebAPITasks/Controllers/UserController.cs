using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using WebAPITasks.Classes;
using WebAPITasks.DAL;
using WebAPITasks.Models;

namespace WebAPITasks.Controllers
{
    public class UserController : ApiController
    {
        //// GET api/user
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/user/5
        //public string Get(int id)
        //{
        //    return "value";
        //}


        [HttpPost]
        // POST api/user
        public resultInfo UpdateUser([FromBody]userInfoUpdate value)
        {
            resultInfo _ResultInfo = new resultInfo();
            StringBuilder msg = new StringBuilder();
            try
            {
                if (value != null && value.updateUserInfo != null && value.updateUserInfo.Length>0)
                {
                    foreach (userInfo item in value.updateUserInfo)
                    {

                        if (item.CRUD == "D")//删除用户
                        {
                            string userId = DBManager.GetUserID(item.userID);
                            if (userId == null || userId.Length <= 0)
                            {
                               // _ResultInfo.message = "未找到用户：" + item.userID;
                                msg.AppendLine("未找到用户：" + item.userID);
                                _ResultInfo.code = -1;
                                break;
                            }
                            else
                            {
                                if (DBManager.DeleteUserByID(userId))
                                {
                                   // _ResultInfo.message = "用户：" + item.userID + " 删除成功！";
                                    msg.AppendLine("用户：" + item.userID + " 删除成功！");
                                    _ResultInfo.code = 0;

                                    LogDataBaseManager.SendUserLogByUDP(Guid.Empty.ToString(),
                                        value.updateUserID,
                                        value.updateUserName,
                                        value.updateIP,
                                        "",
                                        value.updateUserName + "删除用户：" + item.userID + "成功！",
                                        Guid.Empty.ToString(),
                                        LogDatabaseDll.LogDatabaseWS.E_Operation.Delete,
                                        LogDatabaseDll.LogDatabaseWS.E_System.XStudio);
                                }
                                else
                                {
                                    //_ResultInfo.message = "用户：" + item.userID + " 删除失败！";
                                    msg.AppendLine("用户：" + item.userID + " 删除失败！");
                                    _ResultInfo.code = -1;
                                    break;

                                }
                            }
                        }
                        else if (item.CRUD == "U" || item.CRUD == "C")
                        {
                            string pass = com.cdv.nova.util.PasswordHelper.decodePassword(item.salt, item.userID, item.password);
                            if (DBManager.UpdateUserInfo(item.userID, pass, item.userName))
                            {
                                //_ResultInfo.message = "用户：" + item.userID + " 更新成功！";
                                msg.AppendLine("用户：" + item.userID + " 更新成功！");
                                _ResultInfo.code = 0;
                                LogDataBaseManager.SendUserLogByUDP(Guid.Empty.ToString(),
                                       value.updateUserID,
                                       value.updateUserName,
                                       value.updateIP,
                                       "",
                                       value.updateUserName + "更新用户：" + item.userID + " 成功！",
                                       Guid.Empty.ToString(),
                                       LogDatabaseDll.LogDatabaseWS.E_Operation.Delete,
                                       LogDatabaseDll.LogDatabaseWS.E_System.XStudio);
                            }
                            else
                            {
                                //_ResultInfo.message = "用户：" + item.userID + " 更新失败！";
                                msg.AppendLine("用户：" + item.userID + " 更新失败！");
                                _ResultInfo.code = -1;
                                break;
                            }
                        }
                    }

                }
                else
                {
                   // _ResultInfo.message = "需要更新的用户信息不能为空！";
                    msg.AppendLine("需要更新的用户信息不能为空！");
                    _ResultInfo.code = -1;
                }
            }
            catch (Exception ex)
            {
                _ResultInfo.code = -1;
               // _ResultInfo.message = "用户信息更新失败：" + ex.Message;
                msg.AppendLine("用户信息更新失败：" + ex.Message);
                ClassFunction.WriteLocalLog(ex.Message);
            }
            _ResultInfo.message = msg.ToString();
            return _ResultInfo;
        }

        //// PUT api/user/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/user/5
        //public void Delete(int id)
        //{
        //}
    }
}
