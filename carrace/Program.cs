using System;
using System.Diagnostics;
using System.Threading;

namespace carrace;

class Program
{
    // Start semaphore — cars wait until race starts
    public static Semaphore StartSemaphore = new Semaphore(0, 100);
    
    public static Semaphore PitstopSemaphore = new Semaphore(5, 5);

    static void Main(string[] args)
    {
        Stopwatch stopwatch = new Stopwatch();

        Car[] cars =
        {
            new Car("Kainz"),
            new Car("Car 2"),
            new Car("Car 3"),
            new Car("Car 4"),
            new Car("Car 5")
        };

        Thread[] threads = cars
            .Select(car => new Thread(car.Run))
            .ToArray();

        stopwatch.Start();

        // Start all threads (they will wait on semaphore)
        foreach (var t in threads) t.Start();

        // Let all cars start at once
        Console.WriteLine(">>> Start signal given!");
        StartSemaphore.Release(cars.Length);

        // Wait for all cars
        foreach (var t in threads) t.Join();

        stopwatch.Stop();
        Console.WriteLine($"Race is over! It took {stopwatch.ElapsedMilliseconds} ms\n");
        
        // F1 Race

        F1Car[] f1cars =
        {
            new F1Car("F1 Car 1"),
            new F1Car("F1 Car 2"),
            new F1Car("F1 Car 3"),
            new F1Car("F1 Car 4"),
            new F1Car("F1 Car 5")
        };

        Thread[] f1threads = f1cars
            .Select(car => new Thread(car.Run))
            .ToArray();

        stopwatch.Reset();
        stopwatch.Start();

        foreach (var t in f1threads) t.Start();

        Console.WriteLine(">>> F1 Start signal given!");
        StartSemaphore.Release(f1cars.Length);

        foreach (var t in f1threads) t.Join();

        stopwatch.Stop();
        Console.WriteLine($"F1 Race is over! It took {stopwatch.ElapsedMilliseconds} ms");
    }
}
