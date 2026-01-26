// See https://aka.ms/new-console-template for more information

using System.Net.Sockets;

TcpClient client = new TcpClient("localhost", 2025);

Stream stream = client.GetStream();

StreamReader reader = new StreamReader(stream);
StreamWriter writer = new StreamWriter(stream);

writer.AutoFlush = true;
writer.WriteLine(Console.ReadLine());

string response = reader.ReadLine();
Console.WriteLine($"Response from server: {response}");