using System.Diagnostics;

namespace carrace;

class Program
{
    static void Main(string[] args)
    {
        Stopwatch stopwatch = new Stopwatch();
        Car car1 = new Car("Car 1");
        Car car2 = new Car("Car 2");
        Car car3 = new Car("Car 3");
        Car car4 = new Car("Car 4");
        Car car5 = new Car("Car 5");

        Thread thread1 = new Thread(new ThreadStart(car1.Run));
        Thread thread2 = new Thread(new ThreadStart(car2.Run));
        Thread thread3 = new Thread(new ThreadStart(car3.Run));
        Thread thread4 = new Thread(new ThreadStart(car4.Run));
        Thread thread5 = new Thread(new ThreadStart(car5.Run));
        stopwatch.Start();
        thread1.Start();
        thread2.Start();
        thread3.Start();
        thread4.Start();
        thread5.Start();

        thread1.Join();
        thread2.Join();
        thread3.Join();
        thread4.Join();
        thread5.Join();
        stopwatch.Stop();
        Console.WriteLine($"Race is over! it took {stopwatch.ElapsedMilliseconds} ms");
        
        F1Car f1car1 = new F1Car("F1 Car 1");
        F1Car f1car2 = new F1Car("F1 Car 2");
        F1Car f1car3 = new F1Car("F1 Car 3");
        F1Car f1car4 = new F1Car("F1 Car 4");
        F1Car f1car5 = new F1Car("F1 Car 5");
        
        Thread f1thread1 = new Thread(new ThreadStart(f1car1.Run));
        Thread f1thread2 = new Thread(new ThreadStart(f1car2.Run));
        Thread f1thread3 = new Thread(new ThreadStart(f1car3.Run));
        Thread f1thread4 = new Thread(new ThreadStart(f1car4.Run));
        Thread f1thread5 = new Thread(new ThreadStart(f1car5.Run));
        
        stopwatch.Reset();
        stopwatch.Start();
        
        f1thread1.Start();
        f1thread2.Start();
        f1thread3.Start();
        f1thread4.Start();
        f1thread5.Start();
        
        f1thread1.Join();
        f1thread2.Join();
        f1thread3.Join();
        f1thread4.Join();
        f1thread5.Join();
        
        stopwatch.Stop();
        
        Console.WriteLine($"F1 Race is over! it took {stopwatch.ElapsedMilliseconds} ms");
    }
}