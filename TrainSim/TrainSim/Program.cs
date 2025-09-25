namespace TrainSim;

class Program
{
    static void Main(string[] args)
    {
        
        Railway railway1 = new Railway("7|=°>");
        Railway railway2 = new Railway("<o=o>");

        railway1.BuildTrack();

        Thread t1 = new Thread(railway1.RunTrain);
        Thread t2 = new Thread(railway2.RunTrain);

        t1.Start();
        Thread.Sleep(1000); // 1 second gap
        t2.Start();

        t1.Join();
        t2.Join();



    }
}