

using Quartz;
using System;
using System.IO;

namespace WebMvcSandbox.Quartz
{
    public class TestJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            using (StreamWriter streamWriter = new StreamWriter("TestJobLog.txt", true))
            {
                streamWriter.WriteLine(DateTime.Now.ToString());
            }

            System.Diagnostics.Debug.WriteLine("Test Job Fire");
        }

    }
}