# DanskeHW
This is code for task that is get from Danske


#Code
Code has been review, whew bugs has been already spoted so Async log class mainloop was not finishing due to if statement that has been plaved in incorrect place. To raise code speed, new async method has been created to do all the writing in async manner. Exception catching has been done as well, in a manner that it would not crash logging.

New class and interface has been created to transfer all write tasks to it. AsyncLog class now does only async job, the FillToFile class files data into .log file, interface ILogDataToOutput adds flexibility, now there is opportunity to log file in different mannes e.g. Console.

During unit test other not so clear bugs has been noticed, the bug that has been created by myself. When exception is caught the log data that as generated it did not garbage it, so the log input that generates exception has been written over and over again. Small code changes are done to fix issue.

#Unit testing

Unit testing is done for class AsyncLog due to requerements that has been risen. Unit testing is done with Visual Studio unit testing project. 3 unit test are created to test StopWithoutFlush function, StopWithFlush function, Exception handling. For these test output files has been generated and review to double check if everything is fine with unit testing.

The test of creating new file after midnight has been tested manually by changing value of _currDate to yesterdays date. New files has been generated
