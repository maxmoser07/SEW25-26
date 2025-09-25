using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

class Program
{
    static void Main()
    {
        Stopwatch sw = new Stopwatch();
        uint limit = 1000000000;
        sw.Start();

        List<uint> primes = GetPrimesParallel(limit, 8); //Thread amount

        sw.Stop();
        Console.WriteLine($"Number of primes between 1 and {limit}: {primes.Count}");
        Console.WriteLine(sw.ElapsedMilliseconds + " ms");
    }

    static List<uint> GetPrimesParallel(uint n, uint numThreads)
    {
        bool[] isPrime = new bool[n + 1];
        for (uint i = 2; i <= n; i++) isPrime[i] = true;

        uint sqrtN = (uint)Math.Sqrt(n);

        // Step 1: sieve up to sqrt(n) single-threaded
        for (uint i = 2; i <= sqrtN; i++)
        {
            if (isPrime[i])
            {
                for (uint j = i * i; j <= sqrtN; j += i)
                    isPrime[j] = false;
            }
        }

        // Step 2: split marking work into numThreads
        uint chunkSize = n / numThreads;
        Thread[] threads = new Thread[numThreads];

        for (uint t = 0; t < numThreads; t++)
        {
            uint start = t * chunkSize + 1;
            uint end = (t == numThreads - 1) ? n : (t + 1) * chunkSize;
            threads[t] = new Thread(() => MarkMultiples(isPrime, 2, sqrtN, start, end));
            threads[t].Start();
        }

        foreach (Thread t in threads)
            t.Join();

        // Step 3: collect results
        List<uint> primes = new List<uint>();
        for (uint i = 2; i <= n; i++)
            if (isPrime[i]) primes.Add(i);

        return primes;
    }

    static void MarkMultiples(bool[] isPrime, uint startPrime, uint endPrime, uint rangeStart, uint rangeEnd)
    {
        for (uint i = startPrime; i <= endPrime; i++)
        {
            if (isPrime[i])
            {
                uint begin = Math.Max(i * i, ((rangeStart + i - 1) / i) * i); // first multiple in range
                for (uint j = begin; j <= rangeEnd; j += i)
                    isPrime[j] = false;
            }
        }
    }
}
