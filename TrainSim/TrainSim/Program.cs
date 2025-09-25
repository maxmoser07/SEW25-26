namespace TrainSim;

class Program
{
    static void Main(string[] args)
    {
        Railway railway = new Railway();

        char rail = '=';
        string train = "7|=°>";
        int trackLength = 100;
        
        BuildTrack(trackLength, rail);
        RunTrain(train, 80, rail);
    }
    static void BuildTrack(int trackLength, char rail)
    {
        Console.WriteLine("\n");
        for (int i = 0; i < trackLength; i++)
        {
            Console.Write(rail);
        }
    }
    static void RunTrain(string train, int position, char rail)
    {
        for (int i = 1; i < position; i++)
        {
            Console.SetCursorPosition(i-1, 2);
            Console.Write(rail);
            Console.SetCursorPosition(i, 2);
            Console.Write(train);
            Thread.Sleep(100);
        }
    }
}