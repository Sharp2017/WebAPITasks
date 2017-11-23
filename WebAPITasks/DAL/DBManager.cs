using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebAPITasks.Models;

namespace WebAPITasks.DAL
{
    public static class DBManager
    {
        private static string getTmpAppConnectionString()
        {
            string connStr = "";
            try
            {
                connStr = System.Configuration.ConfigurationManager.AppSettings.Get("primaryConnection").ToString();
                if (connStr == null)
                {
                    return "";
                }
                else
                {
                    return connStr;
                }
            }
            catch
            {
                return connStr;
            }

        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="pWorkTaskDataInfo"></param>
        /// <returns></returns>
        public static bool InsertIntoWorkTask(workTaskDataInfo pWorkTaskDataInfo)
        {

            using (SqlConnection conn = new SqlConnection(getTmpAppConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        conn.Open();
                        cmd.Connection = conn;
                        cmd.Transaction = conn.BeginTransaction();
                        cmd.CommandText = "insert into WorkTasks (WorkItemID, ProgramID, WorkCreateDate , AssetName, ColumnName,PlayDate,Tags,Description,Content,ChannelPath,ModifiedDate,ModifiedBy,CreationDate,CreatedBy,MaterialDrefs,TaskStatus,IsBroadcast,BroadcastCode) " +
                                          "values (@WorkItemID, @ProgramID, @WorkCreateDate, @AssetName, @ColumnName,@PlayDate,@Tags,@Description,@Content,@ChannelPath,@ModifiedDate,@ModifiedBy,@CreationDate,@CreatedBy,@MaterialDrefs,@TaskStatus,@IsBroadcast,@BroadcastCode)";
                        cmd.Parameters.AddWithValue("@WorkItemID", pWorkTaskDataInfo.workItemID);
                        cmd.Parameters.AddWithValue("@ProgramID", Guid.Empty.ToString());
                        cmd.Parameters.AddWithValue("@WorkCreateDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@AssetName", pWorkTaskDataInfo.assetName);
                        cmd.Parameters.AddWithValue("@ColumnName", pWorkTaskDataInfo.columnName);
                        cmd.Parameters.AddWithValue("@PlayDate", pWorkTaskDataInfo.playDate);
                        cmd.Parameters.AddWithValue("@Tags", pWorkTaskDataInfo.tags);
                        cmd.Parameters.AddWithValue("@Description", pWorkTaskDataInfo.description);
                        cmd.Parameters.AddWithValue("@Content", pWorkTaskDataInfo.content);
                        cmd.Parameters.AddWithValue("@ChannelPath", pWorkTaskDataInfo.channelPath);
                        cmd.Parameters.AddWithValue("@ModifiedDate", pWorkTaskDataInfo.modifiedDate);
                        cmd.Parameters.AddWithValue("@ModifiedBy", pWorkTaskDataInfo.modifiedBy);
                        cmd.Parameters.AddWithValue("@CreationDate", pWorkTaskDataInfo.creationDate);
                        cmd.Parameters.AddWithValue("@CreatedBy", pWorkTaskDataInfo.createdBy);
                        string strMaterialDrefs = "";
                        if (pWorkTaskDataInfo.materialDrefs != null && pWorkTaskDataInfo.materialDrefs.Count > 0)
                        {
                            foreach (string item in pWorkTaskDataInfo.materialDrefs)
                            {
                                strMaterialDrefs += item + ",";
                            }
                            strMaterialDrefs = strMaterialDrefs.Substring(0, strMaterialDrefs.Length - 1);
                        }
                        cmd.Parameters.AddWithValue("@MaterialDrefs", strMaterialDrefs);
                        cmd.Parameters.AddWithValue("@TaskStatus", 0);
                        cmd.Parameters.AddWithValue("@IsBroadcast", pWorkTaskDataInfo.isBroadcast);
                        cmd.Parameters.AddWithValue("@BroadcastCode", pWorkTaskDataInfo.broadcastCode);
                        cmd.ExecuteNonQuery();

                        cmd.Transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        if (cmd.Transaction != null)
                        {
                            cmd.Transaction.Rollback();
                        }
                        throw ex;
                    }
                }
            }
        }

        /// <summary>
        /// 任务是否存在
        /// </summary>
        /// <param name="pWorkItemID"></param>
        /// <returns></returns>
        public static bool IsExistWorkTask(string pWorkItemID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(getTmpAppConnectionString()))
                {

                    string sql = "select * from WorkTasks where WorkItemID='" + pWorkItemID + "' ";
                    SqlDataAdapter sd = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    sd.Fill(dt);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// 根据工作主键ID获取工作信息
        /// </summary>
        /// <param name="pWorkItemID">工作主键ID</param>
        /// <param name="TaskStatus">状态，1 表示进入制作流程 0表示没有进入制作流程，其他表示全部</param>
        /// <returns></returns>
        public static workTaskDataInfo GetWorkTaskByWorkItemID(string pWorkItemID, string TaskStatus)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(getTmpAppConnectionString()))
                {

                    string sql = "select * from WorkTasks where WorkItemID='" + pWorkItemID + "' ";

                    if (TaskStatus == "1")
                    {
                        sql += " And TaskStatus=1 ";
                    }
                    if (TaskStatus == "0")
                    {
                        sql += " And TaskStatus=0 ";
                    }

                    SqlDataAdapter sd = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    sd.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        workTaskDataInfo info = new workTaskDataInfo();
                        info.taskStatus = Convert.ToInt32(row["TaskStatus"]);
                        info.programID = row["ProgramID"].ToString();
                        return info;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 根据ID查询任务
        /// </summary>
        /// <param name="pWorkItemID"></param>
        /// <returns></returns>
        public static workTaskDataInfo GetWorkTaskByWorkItemID(string pWorkItemID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(getTmpAppConnectionString()))
                {

                    string sql = "select * from WorkTasks where WorkItemID='" + pWorkItemID + "' ";

                    SqlDataAdapter sd = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    sd.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        workTaskDataInfo info = new workTaskDataInfo();
                        info.taskStatus = Convert.ToInt32(row["TaskStatus"]);
                        info.programID = row["ProgramID"].ToString();
                        return info;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="LoginName"></param>
        /// <param name="Password"></param>
        /// <param name="Name"></param>
        /// <param name="Fingerprint"></param>
        /// <returns></returns>
        public static bool UpdateUserInfo(string LoginName, string Password, string Name)
        {
            using (SqlConnection conn = new SqlConnection(getTmpAppConnectionString()))
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    conn.Open();
                    cmd.Connection = conn;
                    string userID =  GetUserID(cmd, LoginName);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();
                    if (userID == null)
                    {
                        cmd.CommandText = "insert into Users (ID, Name, Code, Gender, LoginName, Password, Tag1,Tag2,Tag3,Certificate,ForceCard,JobStatus,ICNumber1,ICNumber2,ICNumber3,JobNumber,IDNumber,Fingerprint,Email1,Email2,Telephone1,Telephone2,CompanyAddr,HomeAddr,LastLoginDateTime,CreateUserName,CreateUserID,CreateDateTime,ModifyUserName,ModifyUserID,ModifyDateTime) " +
                                         "values (@ID,@Name, @Code, @Gender, @LoginName, @Password, @Tag1,@Tag2,@Tag3,@Certificate,@ForceCard,@JobStatus,@ICNumber1,@ICNumber2,@ICNumber3,@JobNumber,@IDNumber,@Fingerprint,@Email1,@Email2,@Telephone1,@Telephone2,@CompanyAddr,@HomeAddr,@LastLoginDateTime,@CreateUserName,@CreateUserID,@CreateDateTime,@ModifyUserName,@ModifyUserID,@ModifyDateTime)";

                        cmd.Parameters.AddWithValue("@ID", Guid.NewGuid());
                        cmd.Parameters.AddWithValue("@Name", Name);
                        cmd.Parameters.AddWithValue("@Code", "");
                        cmd.Parameters.AddWithValue("@Gender", 0);
                        cmd.Parameters.AddWithValue("@LoginName", LoginName);
                        cmd.Parameters.AddWithValue("@Password", Password);
                        cmd.Parameters.AddWithValue("@Tag1", "");
                        cmd.Parameters.AddWithValue("@Tag2", "");
                        cmd.Parameters.AddWithValue("@Tag3", "");
                        cmd.Parameters.AddWithValue("@Certificate", "");
                        cmd.Parameters.AddWithValue("@ForceCard", 0);
                        cmd.Parameters.AddWithValue("@JobStatus", "");
                        cmd.Parameters.AddWithValue("@ICNumber1", "");
                        cmd.Parameters.AddWithValue("@ICNumber2", "");
                        cmd.Parameters.AddWithValue("@ICNumber3", "");
                        cmd.Parameters.AddWithValue("@JobNumber", "");
                        cmd.Parameters.AddWithValue("@IDNumber", "");
                        cmd.Parameters.AddWithValue("@Fingerprint", "");
                        cmd.Parameters.AddWithValue("@Email1", "");
                        cmd.Parameters.AddWithValue("@Email2", "");
                        cmd.Parameters.AddWithValue("@Telephone1", "");
                        cmd.Parameters.AddWithValue("@Telephone2", "");
                        cmd.Parameters.AddWithValue("@CompanyAddr", "");
                        cmd.Parameters.AddWithValue("@HomeAddr", "");
                        cmd.Parameters.AddWithValue("@LastLoginDateTime", Convert.ToDateTime("1900-01-01 00:00:00.000"));
                        cmd.Parameters.AddWithValue("@CreateUserName", "大象融媒");
                        cmd.Parameters.AddWithValue("@CreateUserID", new Guid("00000000-0000-0000-0000-000000000001"));
                        cmd.Parameters.AddWithValue("@CreateDateTime", DateTime.Now);
                        cmd.Parameters.AddWithValue("@ModifyUserID", Guid.Empty);
                        cmd.Parameters.AddWithValue("@ModifyUserName", "");
                        cmd.Parameters.AddWithValue("@ModifyDateTime", Convert.ToDateTime("1900-01-01 00:00:00.000"));

                    }
                    else
                    {
                        cmd.CommandText = "update Users  set LoginName=@LoginName  ,Name=@Name  ,Password=@Password  where  ID=@ID";

                        cmd.Parameters.AddWithValue("@LoginName", LoginName);
                        cmd.Parameters.AddWithValue("@Name", Name);
                        cmd.Parameters.AddWithValue("@Password", Password); 
                        cmd.Parameters.AddWithValue("@ID", userID);

                    }
                    try
                    {
                        cmd.Transaction = conn.BeginTransaction();
                        cmd.ExecuteNonQuery();
                        cmd.Transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        if (cmd.Transaction != null)
                        {
                            cmd.Transaction.Rollback();
                        }
                        return false;
                    }

                }
                catch (System.Exception ex)
                {

                    return false;
                }

            }
        }

        /// <summary>
        /// 根据登录名查询用户ID
        /// </summary>
        /// <param name="command"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        private static string GetUserID(SqlCommand command, string loginName)
        {

            if (loginName == "") return "";
            else
            {
                string UserID = null;
                command.CommandText = "select ID from Users where LoginName=@LoginName";
                command.CommandType = CommandType.Text;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@LoginName", loginName);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;

                DataTable table = new DataTable("Result");
                try
                {
                    adapter.Fill(table);
                    if (table.Rows.Count == 1)
                    {
                        UserID = table.Rows[0]["ID"].ToString();
                    }
                    else
                    { UserID = null; }


                }
                catch (Exception exp)
                {
                    command.Connection.Close();
                    string ip = command.Connection.DataSource;
                    string db = command.Connection.Database;
                    string sql = command.CommandText;
                    throw new Exception("[SERVER][" + ip + "][DB][" + db + "][SQL][" + sql + "]" + exp.Message);
                }

                return UserID;
            }
        }

        /// <summary>
        /// 根据登录名查询用户ID
        /// </summary>
        /// <param name="command"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public static string GetUserID(string loginName)
        {

            if (loginName == "") return null;
            else
            {
                using (SqlConnection conn1 = new SqlConnection(getTmpAppConnectionString()))
                {
                    string UserID = null;
                    SqlDataAdapter sd = new SqlDataAdapter();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn1;
                    cmd.CommandText = "select ID from Users where LoginName=@LoginName";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@LoginName", loginName);
                    sd.SelectCommand = cmd;
                    DataTable dt = new DataTable();
                    try
                    {
                        sd.Fill(dt);
                        if (dt.Rows.Count == 1)
                        {
                            UserID = dt.Rows[0]["ID"].ToString();
                        }
                        else
                        { UserID = null; } 
                    }
                    catch (Exception e)
                    {
                        return null;

                    }
                    return UserID; 
                } 
            }
        }

        /// <summary>
        /// 根据ID删除用户
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static bool DeleteUserByID(string userID)
        {
            using (SqlConnection conn = new SqlConnection(getTmpAppConnectionString()))
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Transaction = conn.BeginTransaction();    

                    cmd.CommandText = "delete from users where id='" + userID + "'";
                    cmd.Parameters.Clear(); 
                    cmd.ExecuteNonQuery();  

                    cmd.CommandText = "delete from usersex where userid='" + userID + "'";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "delete from UserGroup2User where userid='" + userID + "'";
                    cmd.ExecuteNonQuery();
                    
                    cmd.Transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    cmd.Transaction.Rollback();
                    return false;
                }
            }
        }

    }
}