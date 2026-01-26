// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Net.Sockets;

int anz = 5;
TcpListener lsnr = new TcpListener(IPAddress.Loopback,2025);

lsnr.Start();

Console.WriteLine("Listening on Port 2025");

for (int i = 0; i < anz; i++)
{
    new Thread(() => StatelessFileServer()).Start();
}

void StatelessFileServer()
{
    while (true)
    {
        Socket soc = lsnr.AcceptSocket();
        Console.WriteLine($"Connection from {soc.RemoteEndPoint}");

        Stream s = new NetworkStream(soc);
        StreamReader sr = new StreamReader(s);
        StreamWriter sw = new StreamWriter(s);
        sw.AutoFlush = true;
        
        string input = sr.ReadLine();
        
        string content = File.ReadAllText(input);
        
        sw.Write(content);
        soc.Close();
        
    }
}

void StatelessEchoServer()
{
    while (true)
    {
        Socket soc = lsnr.AcceptSocket();
        Console.WriteLine($"Connection from {soc.RemoteEndPoint}");

        Stream s = new NetworkStream(soc);
        StreamReader sr = new StreamReader(s);
        StreamWriter sw = new StreamWriter(s);
        sw.AutoFlush = true;
        
        string input = sr.ReadLine();
        sw.Write(input.ToUpper());
        sw.Flush();
        soc.Close();
        
    }
}


Console.ReadKey();