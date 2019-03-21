using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentProcessor
{
    public static class utility
    {
        public static void logFile(string documentProcessor, int TotalFilecount, int exception, int successfullyprocessedcnt, int existingerrorcnt)
        {
            if (!Directory.Exists(logpath))
                Directory.CreateDirectory(logpath);
            string logfilename = logpath + logfile;

            if (!File.Exists(logfilename))
                File.Create(logfilename);


            IEnumerable<string> log = new List<string>
            {
                DateTime.Now.ToString(), "Total Doc Count : "+TotalFilecount+
                ", Total File Processed :  "+successfullyprocessedcnt+", Execeptiong : "+exception+
                ", Existing Exception : "+existingerrorcnt+""
            };

            File.AppendAllLines(logfilename, log);
        }

        public static void logFile(string documentProcessor, Exception ex)
        {
            if (!Directory.Exists(logpath))
                Directory.CreateDirectory(logpath);
            string logfilename = logpath + logfile;

            if (!File.Exists(logfilename))
                File.Create(logfilename);

            IEnumerable<string> log = new List<string>
            {
                DateTime.Now.ToString(), "Exception Message: "+ex.Message+
                ", Stack Trace :  "+ex.StackTrace+", Source : "+ex.Source
            };

            File.AppendAllLines(logfilename, log);
        }

        /// <summary>
        /// Filelocation for final swept files. this path can be maintained from outside.
        /// </summary>
        public static string cmspath = @"c:\temp\CMS\Filelocation";
        /// <summary>
        /// log file for all logs. this file name can be maintained from outside also using config file or db
        /// </summary>
        public static string logpath = @"c:\temp\log\";
        public static string logfile = "Logfile.txt";

    }
}
