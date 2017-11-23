using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPITasks.Classes;
using WebAPITasks.DAL;
using WebAPITasks.Models;

namespace WebAPITasks.Controllers
{
    public class WorkTaskController : ApiController
    {
        // GET api/worktask
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/worktask/5
        //public TaskProgress Get(string id)
        //{
        //    //整个生产任务的状态，未开始0、进行中1、已终止2、已完成3 
        //    TaskProgress _TaskProgress = new TaskProgress();
        //    try
        //    {
        //        WorkTaskDataInfo _WorkTaskDataInfo = DBManager.GetWorkTaskByWorkItemID(id);
        //        if (_WorkTaskDataInfo != null)
        //        { 
        //            if (_WorkTaskDataInfo != null)
        //            {
        //                switch (_WorkTaskDataInfo.TaskStatus)
        //                {
        //                    case 0:
        //                        _TaskProgress.Name = "未开始";
        //                        _TaskProgress.Status = 0;
        //                        break;
        //                    case 1:
        //                        _TaskProgress.Name = "已完成";
        //                        _TaskProgress.Status = 1;
        //                        break;
        //                    case 2:
        //                        _TaskProgress.Name = "发送完成";
        //                        _TaskProgress.Status = 2;
        //                        break;
        //                    default:
        //                        _TaskProgress.Name = "未开始";
        //                        _TaskProgress.Status = 0;
        //                        break;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            _TaskProgress.Name = "未找到制作任务";
        //            _TaskProgress.Status = -1;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _TaskProgress.Name = "fail";
        //        ClassFunction.WriteLocalLog(ex.Message);
        //    }
        //    return _TaskProgress;
        //}

        [HttpGet]
        public taskProgress QueryWorkTask(string id)
        {
            //整个生产任务的状态，查询错误-100，未找到-1，未开始0、进行中1、已终止2、已完成3 
            taskProgress _TaskProgress = new taskProgress();
           
            try
            {
                workTaskDataInfo _WorkTaskDataInfo = DBManager.GetWorkTaskByWorkItemID(id);
                if (_WorkTaskDataInfo != null)
                {
                    switch (_WorkTaskDataInfo.taskStatus)
                    {
                        case 0:
                            _TaskProgress.name = "未开始";
                            _TaskProgress.status = 0;
                            break;
                        case 1:
                            _TaskProgress.name = "未发送";
                            _TaskProgress.status = 1;
                            break;
                        case 2:
                            _TaskProgress.name = "发送完成";
                            _TaskProgress.status = 2;
                            break;
                        case 3:
                            _TaskProgress.name = "制作中";
                            _TaskProgress.status = 3;
                            break;
                        default:
                            _TaskProgress.name = "未开始";
                            _TaskProgress.status = 0;
                            break;
                    }
                }
                else
                {
                    _TaskProgress.name = "未找到制作任务";
                    _TaskProgress.status = -1;
                }
            }
            catch (Exception ex)
            {
                _TaskProgress.name = "查询失败:" + ex.Message;
                _TaskProgress.status = -100;
                ClassFunction.WriteLocalLog(ex.Message);
            }
            return _TaskProgress;
        }

        [HttpPost]
        // POST api/worktask
        public resultInfo AddWorkTask([FromBody] workTaskDataInfo value)
        {
            resultInfo _ResultInfo = new resultInfo();
            _ResultInfo.message = "任务添加失败";
            try
            {
                if (DBManager.IsExistWorkTask(value.workItemID))
                {
                    _ResultInfo.code = 1;
                    _ResultInfo.message = "此任务已存在";
                }
                else
                {
                    if (DBManager.InsertIntoWorkTask(value))
                    {
                        _ResultInfo.code = 0;
                        _ResultInfo.message = "任务添加成功，任务ID：" + value.workItemID;
                    }
                    else
                    { 
                        _ResultInfo.code = -1;
                        _ResultInfo.message = "任务添加失败";
                    }
                }
            }
            catch (Exception ex)
            {
                _ResultInfo.code = -1;
                _ResultInfo.message = "添加失败：" + ex.Message;
                ClassFunction.WriteLocalLog(ex.Message);
            }
            return _ResultInfo;
        }

        //// PUT api/worktask/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/worktask/5
        //public void Delete(int id)
        //{
        //}
    }
}
