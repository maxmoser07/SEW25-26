namespace carrace;

public class F1Car : Car
{
    public F1Car(string racer) : base(racer)
    {
    }
    
    public new void Run()
    {
        Start();
        
    }
    private void Start()
    {
        Console.WriteLine($"{Racer} starting");
        Thread.Sleep(1000);
    }

    private void End()
    {
        Console.WriteLine($"{Racer} finished the race");
    }
}