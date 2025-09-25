namespace TrainSim;

class Program
{
    static void Main(string[] args)
    {
        Railway railway1 = new Railway('#', "7|=°>");
        
        railway1.BuildTrack();
        railway1.RunTrain();
    }

}