using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace LogTest
{
    using System.IO;
    using System.Text;
    /// <summary>
    /// Class responsible to write into .log file
    /// </summary>
    public  class LogToFile : ILogDataToOutput
    {

        #region Private variables
        private DateTime _curDate = DateTime.Now;
        private StreamWriter _writer;
        #endregion

        #region Constructor
        public LogToFile()
        {
            if (!Directory.Exists(@"C:\LogTest"))
                Directory.CreateDirectory(@"C:\LogTest");
            CreateNewFile();
        }
        #endregion


        #region Public methods
        /// <summary>
        /// Write Data to Log file
        /// </summary>
        /// <param name="logLine"></param>
        public void WriteData(LogLine logLine)
        {
            StringBuilder stringBuilder = new StringBuilder();
            //If midnight make new file
            if ((DateTime.Now - _curDate).Days != 0)
            {
                _curDate = DateTime.Now;

                CreateNewFile();

                stringBuilder.Append(Environment.NewLine);

                this._writer.Write(stringBuilder.ToString());


            }

            //Builds string to be written
            stringBuilder.Append(logLine.Timestamp.ToString("yyyy-MM-dd HH:mm:ss:fff"));
            stringBuilder.Append("\t");
            stringBuilder.Append(logLine.LineText());
            stringBuilder.Append("\t");
            stringBuilder.Append(Environment.NewLine);

            this._writer.Write(stringBuilder.ToString());

        }
        #endregion


        #region Private methods
        /// <summary>
        /// Create a new log file
        /// </summary>
        private void CreateNewFile()
        {
            this._writer = File.AppendText(@"C:\LogTest\Log" + DateTime.Now.ToString("yyyyMMdd HHmmss fff") + ".log");

            this._writer.Write("Timestamp".PadRight(25, ' ') + "\t" + "Data".PadRight(15, ' ') + "\t" + Environment.NewLine);

            this._writer.AutoFlush = true;
        }
        #endregion
    }
}

