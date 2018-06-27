namespace LogTest
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;


    /// <summary>
    /// This class is used only to process task in async maner 
    /// </summary>
    public class AsyncLog : ILog
    {

        #region Private variables
        private Thread _runThread;
        private List<LogLine> _lines = new List<LogLine>();
        private ILogDataToOutput logDataTo;
        private bool _QuitWithFlush = false;
        private bool _exit;
        #endregion

        #region Constructor
        public AsyncLog(ILogDataToOutput logDataTo)
        {
            this.logDataTo = logDataTo;
            this._runThread = new Thread(this.MainLoop);
            this._runThread.Start();
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Loop in new thread not in main program one
        /// </summary>
        private void MainLoop()
        {
            while (!this._exit)
            {
                try
                {
                    if (this._lines.Count > 0)
                    {
                        FillAsync(this._lines);
                    }

                    //While loop never ends (bug) if statment not in place
                    if (this._QuitWithFlush == true && this._lines.Count == 0)
                        this._exit = true;

                    Thread.Sleep(50);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unexpected exception occured");
                    Console.WriteLine(ex.Message);
                }
               
            }
            
        }


        /// <summary>
        /// Write async lines
        /// </summary>
        /// <param name="tempLines"></param>
        /// <returns></returns>
        private async Task FillAsync(List<LogLine> tempLines)
        {

            List<LogLine> _handled = new List<LogLine>();
            foreach (LogLine logLine in tempLines)
            {

                if (!this._exit || this._QuitWithFlush)
                {
                    _handled.Add(logLine);
                    logDataTo.WriteData(logLine);
                    
                }
            }

            for (int y = 0; y < _handled.Count; y++)
            {
                this._lines.Remove(_handled[y]);
            }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Stop without finishing the write
        /// </summary>
        public void StopWithoutFlush()
        {
            this._exit = true;
            
        }

        public void StopWithFlush()
        {
            this._QuitWithFlush = true;
        }

        public void Write(string text)
        {
            this._lines.Add(new LogLine() { Text = text, Timestamp = DateTime.Now });
        }
        #endregion
    }
} 