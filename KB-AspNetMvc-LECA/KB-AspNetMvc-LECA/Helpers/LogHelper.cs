using System;
using System.Diagnostics;
using System.IO;
using System.Web;
namespace KB_AspNetMvc_LECA.Helpers
{
    /// <summary>
    /// The class in which the logging operations are performed.
    /// </summary>
    public static class LogHelper
    {
        /// <summary>
        /// Location where log files are stored.
        /// </summary>
        private const string LogMapPath = "/Content/Log";
        /// <summary>
        /// The path on the server to the location where the log files are stored.
        /// </summary>
        private static string ServerLogMapPath { get => HttpContext.Current.Server.MapPath(LogMapPath); }
        /// <summary>
        /// Folder check operation. (The folder where the log files are stored)
        /// </summary>
        private static void DirectoryControl()
        {
            try
            {
                if (!Directory.Exists(ServerLogMapPath))
                    Directory.CreateDirectory(ServerLogMapPath);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// Default log types.
        /// </summary>
        public enum LogTypes
        {
            Information,
            Warning,
            Error,
            Success_Audit,
            Failure_Audit
        }
        /// <summary>
        /// It checks whether there is a log file for the current day, if there is no log file, it creates a new log file.
        /// </summary>
        /// <returns>Returns the path of the log file on the server.</returns>
        private static string LogFileControl()
        {
            try
            {
                string date = DateTime.Now.ToString("dd-MM-yyyy");
                string fileName = $"{date}.log";
                string filePath = Path.Combine(ServerLogMapPath, fileName);
                bool fileCheck = File.Exists(filePath);
                if (fileCheck == false)
                {
                    using (FileStream fs = File.Create(filePath))
                    {
                        fs.Dispose();
                        fs.Close();
                    }
                }
                return filePath;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return "#!";
            }
        }
        /// <summary>
        /// Adds a record to the log file.
        /// </summary>
        /// <param name="type">Type of log record.</param>
        /// <param name="data">Log record to be added.</param>
        public static void AddLog(LogTypes type, string data)
        {
            try
            {
                DirectoryControl();
                string filePath = LogFileControl();
                if (filePath != "#!")
                {
                    StreamWriter sw = File.AppendText(filePath);
                    string log = $"{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")} | {type.ToString()} | {data}";
                    sw.WriteLine(log);
                    sw.Dispose();
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// It records the information about the error that occurred in the log file.
        /// </summary>
        /// <param name="exception">The error to be logged.</param>
        public static void AddLog(Exception exception)
        {
            try
            {
                DirectoryControl();
                string filePath = LogFileControl();
                StreamWriter sw = File.AppendText(filePath);
                string log = $"{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")} | {LogTypes.Error.ToString()}";
                if (exception.TargetSite != null)
                    if (exception.TargetSite.DeclaringType != null)
                        if (exception.TargetSite.DeclaringType.Name != null)
                            log += " | Controller: " + exception.TargetSite.DeclaringType.Name;

                if (exception.TargetSite != null)
                    if (exception.TargetSite.Name != null)
                        log += " | Action: " + exception.TargetSite.Name;

                log += " | Exception: " + exception.Message;

                sw.WriteLine(log);
                sw.Dispose();
                sw.Close();
                try
                {
                    Debug.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + " | Error: " + exception.Message);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}