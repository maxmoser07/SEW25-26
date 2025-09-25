namespace TrainSim;

public class Railway
{
    Semaphore[] semaphores = new Semaphore[1];
    private char Rail;
    public string Train { get; set; }
    private int TrackLength;
    
    

    // constructor
    public Railway(string train)
    {
        Rail = '#';
        Train = train;
        TrackLength = 80;
    }
    public void BuildTrack()
    {
        BuildTrack(TrackLength, Rail);
    }
    public void RunTrain()
    {
        RunTrain(Train, TrackLength, Rail);
    }
    private void BuildTrack(int trackLength, char rail)
    {
        Console.WriteLine("\n");
        for (int i = 1; i < trackLength/10; i++)
        {
            Console.Write($"\t/   ");
        }
        Console.WriteLine();
        for (int i = 1; i < trackLength/10; i++)
        {
            Console.Write($"\t|({i})");
        }

        Console.WriteLine();
        for (int i = 0; i < trackLength; i++)
        {
            Console.Write(rail);
        }
    }
    public void RunTrain(string train, int tracklength, char rail)
    {
        for (int i = 1; i < tracklength-train.Length; i++)
        {
            Console.SetCursorPosition(i-1, 4);
            Console.Write(rail);
            Console.SetCursorPosition(i, 4);
            Console.Write(train);
            Thread.Sleep(100);
        }
        Console.SetCursorPosition(tracklength-train.Length-1, 4);
        for (int i = 0; i < train.Length; i++)
            Console.Write(rail);
    }
}