using System;
using LogTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestLogComponent
{
    [TestClass]
    public class AsyncLogUnitTesting
    {
        [TestMethod]
        public void StopWithoutFlush_StopAtPoint10_AssertThatItPointIs11AndNotEqual10()
        {
            ILog writerAsync = new AsyncLog(new FakeLogOutput());
            for (int i = 50; i >= 0; i--)
            {
                writerAsync.Write(i.ToString());
                if (i == 10)
                {
                    writerAsync.StopWithoutFlush();
                }
                System.Threading.Thread.Sleep(50);
            }
        }

        [TestMethod]
        public void StopWithFlush_StopAtPoint3_AssertThat0IsWritten()
        {
            ILog writerAsync = new AsyncLog(new FakeLogOutputForUnitTestWithFlush());
            for (int i = 30; i >= 0; i--)
            {
                writerAsync.Write(i.ToString());
                System.Threading.Thread.Sleep(50);
            }
            writerAsync.StopWithFlush();
        }

        [TestMethod]
        public void Write_ExceptionThrown_NoExceptionExpected()
        {
            try
            {
                ILog writerAsync = new AsyncLog(new FakeLogOutputForUnitTestThrowException());
                for (int i = 10; i >= 0; i--)
                {
                    writerAsync.Write(i.ToString());
                    System.Threading.Thread.Sleep(50);
                }
            }
            catch (Exception ex)
            {

                Assert.Fail("Expected no exception but got one : " + ex.Message);
            }
        }
    }

    #region FakeLogOuputForUnitTestStopWithoutFlush
    public class FakeLogOutput : ILogDataToOutput
    {

        System.IO.StreamWriter writer;

        public FakeLogOutput()
        {
            writer = new System.IO.StreamWriter("outputUnitTestStopWithoutFlush.txt");
        }


        public void WriteData(LogLine logLine)
        {
            writer.WriteLine(logLine.Text);
            writer.Flush();
            if(logLine.Text == "11")
            {
                Assert.Equals(logLine.Text, "11");
            }
            else
            {
                Assert.AreNotEqual(logLine.Text, "10");
            }
        
            
        }
    }
    #endregion

    #region FakeLogOutputForUnitTestWithFlush
    class FakeLogOutputForUnitTestWithFlush : ILogDataToOutput
    {
        System.IO.StreamWriter writer;

        public FakeLogOutputForUnitTestWithFlush()
        {
            writer = new System.IO.StreamWriter("outputUnitTestStopWithFlush.txt");
        }

        public void WriteData(LogLine logLine)
        {
            writer.WriteLine(logLine.Text);
            writer.Flush();
            if (logLine.Text == "0")
            {
                Assert.Equals(logLine.Text, "0");
            }

        }
        
    }
    #endregion

    #region FakeLogOutputForUnitTestThrowException
    class FakeLogOutputForUnitTestThrowException : ILogDataToOutput
    {

        System.IO.StreamWriter writer;

        public FakeLogOutputForUnitTestThrowException()
        {
            writer = new System.IO.StreamWriter("outputUnitTestExeption.txt");
        }

        public void WriteData(LogLine logLine)
        {
            if (logLine.Text == "2")
            {
                throw new Exception("Test exception");
            }
            writer.WriteLine(logLine.Text);
            writer.Flush();
            
        }
    }
    #endregion

}
