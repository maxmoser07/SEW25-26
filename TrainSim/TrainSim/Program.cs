using System;
using System.Threading;

namespace TrainSim
{
    class Program
    {
        static void Main(string[] args)
        {
            var display = new ConsoleDisplay();

            Railway railway1 = new Railway("7|=°>");
            Railway railway2 = new Railway("<o=o>");

            railway1.Attach(display);
            railway2.Attach(display);

            railway1.BuildTrack();

            Thread t1 = new Thread(railway1.RunTrain);
            Thread t2 = new Thread(railway2.RunTrain);

            t1.Start();
            Thread.Sleep(3000); // delay to show train queuing
            t2.Start();

            t1.Join();
            t2.Join();

            Console.SetCursorPosition(0, 6);
            Console.WriteLine("All trains finished!");
        }
    }
}