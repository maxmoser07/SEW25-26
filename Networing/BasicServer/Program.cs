using System.Net;
using System.Net.Sockets;

string homeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
string sharedFolder = Path.Combine(homeDir, "TcpShared");

if (!Directory.Exists(sharedFolder)) Directory.CreateDirectory(sharedFolder);

TcpListener lsnr = new TcpListener(IPAddress.Any, 2025);
lsnr.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
lsnr.Start();
Console.WriteLine($"Server started. Monitoring: {sharedFolder}");

while (true)
{
    using TcpClient client = lsnr.AcceptTcpClient();
    using NetworkStream stream = client.GetStream();
    
    StreamReader sr = new StreamReader(stream);
    string? requestedFile = sr.ReadLine(); 

    if (!string.IsNullOrEmpty(requestedFile))
    {
        string fullPath = Path.Combine(Directory.GetCurrentDirectory(), requestedFile);

        if (File.Exists(fullPath) && fullPath.EndsWith(".html"))
        {
            Console.WriteLine($"Serving HTML: {fullPath}");
            
            StreamWriter sw = new StreamWriter(stream);
            
            sw.WriteLine("HTTP/1.1 200 OK");
            sw.WriteLine("Content-Type: text/html; charset=utf-8");
            sw.WriteLine(""); // This empty line is REQUIRED to separate headers from content
            sw.Flush(); 
            
            using FileStream fs = File.OpenRead(fullPath);
            fs.CopyTo(stream);
            stream.Flush();
        }
    }
    
}