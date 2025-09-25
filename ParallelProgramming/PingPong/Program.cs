namespace ParallelProgramming;
using System.Globalization;
class Program
{
    static void Main(string[] args)
    {
        object ping = new object();
        object pong = new object();
        
        new Thread(new ThreadStart(() => Spieler1())).Start();
        new Thread(new ThreadStart(() => Spieler2())).Start();
        Monitor
    }
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

    public void Spieler2()
    {
        while (true)
        {
            Monitor.Enter(pong);
            {
                Console.WriteLine("pong");
            }
        }
    }
}