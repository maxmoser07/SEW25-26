namespace TrainSim;

public class Railway
{
    Semaphore[] semaphores = new Semaphore[1];
    
    public void BuildTrack(int trackLength, char rail)
    {
        Console.WriteLine("\n");
        for (int i = 0; i < trackLength; i++)
        {
            Console.Write(rail);
        }
    }
    public void RunTrain(string train, int position, char rail)
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