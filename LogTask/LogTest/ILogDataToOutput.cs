using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTest
{
    public interface ILogDataToOutput
    {

        /// <summary>
        /// Write data to desireable log
        /// </summary>
        /// <param name="logLine"></param>
        void WriteData(LogLine logLine);
    }
}
