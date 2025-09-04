using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

class Program
{
    static void Main()
    {
        Stopwatch sw = new Stopwatch();
        int limit = 100000000;
        sw.Start();

        List<int> primes = GetPrimesParallel(limit, 6);

        sw.Stop();
        Console.WriteLine($"Number of primes between 1 and {limit}: {primes.Count}");
        Console.WriteLine(sw.ElapsedMilliseconds + " ms");
    }

    static List<int> GetPrimesParallel(int n, int numThreads)
    {
        bool[] isPrime = new bool[n + 1];
        for (int i = 2; i <= n; i++) isPrime[i] = true;

        int sqrtN = (int)Math.Sqrt(n);

        // Step 1: sieve up to sqrt(n) single-threaded
        for (int i = 2; i <= sqrtN; i++)
        {
            if (isPrime[i])
            {
                for (int j = i * i; j <= sqrtN; j += i)
                    isPrime[j] = false;
            }
        }

        // Step 2: split marking work into numThreads
        int chunkSize = n / numThreads;
        Thread[] threads = new Thread[numThreads];

        for (int t = 0; t < numThreads; t++)
        {
            int start = t * chunkSize + 1;
            int end = (t == numThreads - 1) ? n : (t + 1) * chunkSize;
            threads[t] = new Thread(() => MarkMultiples(isPrime, 2, sqrtN, start, end));
            threads[t].Start();
        }

        foreach (Thread t in threads)
            t.Join();

        // Step 3: collect results
        List<int> primes = new List<int>();
        for (int i = 2; i <= n; i++)
            if (isPrime[i]) primes.Add(i);

        return primes;
    }

    static void MarkMultiples(bool[] isPrime, int startPrime, int endPrime, int rangeStart, int rangeEnd)
    {
        for (int i = startPrime; i <= endPrime; i++)
        {
            if (isPrime[i])
            {
                int begin = Math.Max(i * i, ((rangeStart + i - 1) / i) * i); // first multiple in range
                for (int j = begin; j <= rangeEnd; j += i)
                    isPrime[j] = false;
            }
        }
    }
}
