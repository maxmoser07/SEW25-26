using System.Linq;
namespace AsyncBlazer.Services;

public class SieveService
{
    public int Sieve(int n)
    {
        bool[] isPrime = new bool[n + 1];
        for (int i = 2; i <= n; i++)
            isPrime[i] = true;

        for (int i = 2; i * i <= n; i++)
        {
            if (isPrime[i])
            {
                for (int j = i * i; j <= n; j += i)
                    isPrime[j] = false;
            }
        }

        return isPrime.Count(x => x);
    }
}