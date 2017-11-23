using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;

namespace WebAPITasks.Classes
{
    public static class ClassFunction
    {
        

        public static void WriteLocalLog(string conent)
        {
            try
            {
                if (!Directory.Exists(Environment.CurrentDirectory + "\\log"))
                {
                    Directory.CreateDirectory(Environment.CurrentDirectory + "\\log");
                }

                string fileName = DateTime.Now.ToString("yyyyMMdd") + "ErrorLog.txt";

                StreamWriter stream = File.AppendText(Environment.CurrentDirectory + "\\log\\" + fileName);
                stream.WriteLine(DateTime.Now + ":  " + conent);
                stream.Flush();
                stream.Close();

            }
            catch (System.Exception ex)
            {

            }



        }
    }
}