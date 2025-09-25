namespace TrainSim;

class Program
{
    static void Main(string[] args)
    {
        Railway railway1 = new Railway("7|=°>");
        Railway railway2 = new Railway("<o=o>");

        railway1.BuildTrack();

        // Start first train
        Thread t1 = new Thread(railway1.RunTrain);

        // Start second train
        Thread t2 = new Thread(railway2.RunTrain);

        t1.Start();
        Thread.Sleep(3000); //delay between train starts
        t2.Start();

        // Wait for both to finish
        t1.Join();
        t2.Join();
    }
}