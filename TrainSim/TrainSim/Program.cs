using System;
using System.Threading;

namespace TrainSim
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create trains
            Railway railway1 = new Railway("7|=°>");
            Railway railway2 = new Railway("<o=o>");

            // Build track once
            railway1.BuildTrack();

            // Start trains in separate threads
            Thread t1 = new Thread(railway1.RunTrain);
            Thread t2 = new Thread(railway2.RunTrain);

            t1.Start();
            Thread.Sleep(3000); // delay to show train queuing
            t2.Start();

            // Wait for both trains to finish
            t1.Join();
            t2.Join();

            Console.SetCursorPosition(0, 6);
            Console.WriteLine("All trains finished!");
        }
    }
}