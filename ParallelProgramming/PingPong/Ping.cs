namespace ParallelProgramming;

public class Ping
{
    public void Spieler1()
    {
        while (true)
        {
            Monitor.Enter(ping);
            {
                Console.WriteLine("ping");
            }
        }
    }
}