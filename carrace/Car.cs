using System;
using System.Threading;

namespace carrace;

public class Car
{
    public string Racer { get; set; }

    public Car(string racer)
    {
        Racer = racer;
    }

    public void Run()
    {
        WaitForStart();
        Race();
        TakePitstop();
        Race();
        Finish();
    }

    protected void WaitForStart()
    {
        Console.WriteLine($"{Racer} is waiting for the start signal...");
        Program.StartSemaphore.WaitOne();
        Console.WriteLine($"{Racer} STARTS!");
    }

    private void TakePitstop()
    {
        Console.WriteLine($"{Racer} tries to enter pitstop...");
        Program.PitstopSemaphore.WaitOne();
        Console.WriteLine($"{Racer} is taking a pitstop...");
        Thread.Sleep(500);
        Program.PitstopSemaphore.Release();
    }

    private void Race()
    {
        Console.WriteLine($"{Racer} is racing...");
        Thread.Sleep(1500);
    }

    private void Finish()
    {
        Console.WriteLine($"{Racer} finished the race!");
    }
}