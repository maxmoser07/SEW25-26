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
        WaitForSignal();
        Race();
        TakingPitstop();
        Race();
    }

    private void WaitForSignal()
    {
        Console.WriteLine($"{Racer} is waiting for the signal to start");
        Thread.Sleep(200);
    }
    private void TakingPitstop()
    {
        Console.WriteLine($"{Racer} is taking a pitstop");
        Thread.Sleep(500);
    }
    private void Race()
    {
        Console.WriteLine($"{Racer} is racing");
        Thread.Sleep(1500);
    }
}