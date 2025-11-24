namespace carrace;

public class F1Car : Car
{
    public F1Car(string racer) : base(racer) {}

    public new void Run()
    {
        WaitForStart();
        Thread.Sleep(1000);
        Console.WriteLine($"{Racer} finished the race");
    }
}