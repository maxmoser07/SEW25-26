namespace TrainSim;

class Program
{
    static void Main(string[] args)
    {
        char rail = '#';
        string train = "7|=°>";
        int trackLength = 80;
        
        Railway railway1 = new Railway();
        
        railway1.BuildTrack(trackLength, rail);
        railway1.RunTrain(train, trackLength, rail);
        
    }

}