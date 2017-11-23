using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace WebAPITasks.DAL
{
    public class LogDataBaseManager
    {

        static string userID1 = "";
        static string loginName1 = "";
        static string userFullName1 = "";
        static string logContent1 = "";
        static string programTitle1 = "";
        static string programID1 = "";
        static LogDatabaseDll.LogDatabaseWS.E_Operation oper1;
        static LogDatabaseDll.LogDatabaseWS.E_System sys1;
        public static string myIP = "";
        public static string myHostName = "";

        public enum LogType
        {
            All = 0,
            Search = 1,
            New = 2,
            Modify = 3,
            Delete = 4,
            Add = 5,
            Remove = 6,
            Clear = 7,
            Import = 8,
            Export = 9,
            Upload = 10,
            Download = 11,
            Audit = 12,
            TechnologyReview = 13,
            ReviewHearing = 14,
            Synchronization = 15,
            Downmix = 16,
            Switch = 17,
            Login = 18,
            LogOut = 19
        }

        private static string getlogServiceURLString()
        {
            string LogServiceStr = "";
            try
            {
                LogServiceStr = System.Configuration.ConfigurationManager.AppSettings.Get("LogService").ToString();
                if (LogServiceStr == null)
                {
                    return "";
                }
                else
                {
                    return LogServiceStr;
                }
            }
            catch
            {
                return LogServiceStr;
            }

        }
        public  static void IniXStudioLog()
        { 
            try
            {
                LogDatabaseDll.ClientAgency.LogDatabaseClient.LogInfo.SetUrl(getlogServiceURLString()); 
                 
            }
            catch (System.Exception ex)
            {

            }

        }


        public static void SendUserLogByUDP(string userID, string loginName, string userFullName,string pMyIP, string programTitle, string logContent, string programID, LogDatabaseDll.LogDatabaseWS.E_Operation oper, LogDatabaseDll.LogDatabaseWS.E_System sys)
        {


            string message = "Xstudio;" + userFullName + ";" + myIP + ";" + (int)oper + ";" + programTitle;
            try
            {
                userID1 = userID;
                loginName1 = loginName;
                userFullName1 = userFullName;
                programTitle1 = programTitle;
                logContent1 = logContent;
                programID1 = programID.Trim().Length == 0 ? Guid.Empty.ToString() : programID.Trim();
                myIP = pMyIP;
                oper1 = oper;
                sys1 = sys; 

                System.Threading.Thread th = new System.Threading.Thread(new ThreadStart(writeLog));
                th.Start();
            }
            catch
            {

            }
              

        }


        private static void writeLog()
        {
            try
            {
                bool b = LogDatabaseDll.ClientAgency.LogDatabaseClient.LogInfo.WriteLog(myIP, myHostName, new Guid(userID1), logContent1, programTitle1, new Guid(programID1), LogDatabaseDll.LogDatabaseWS.E_Type.Normal, userFullName1, loginName1, oper1, sys1);



            }
            catch { }
        }

    }
}