using System.Net;
using System.Net.Sockets;

// 1. Define a safe root directory in your Home folder
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
    // Inside your Server loop
    string? requestedFile = sr.ReadLine();
    if (!string.IsNullOrEmpty(requestedFile))
    {
        // Use Path.GetFullPath to see exactly where the server is looking
        string fullPath = Path.Combine(Directory.GetCurrentDirectory(), requestedFile);
    
        if (File.Exists(fullPath))
        {
            Console.WriteLine($"[SUCCESS] Sending {fullPath} ({new FileInfo(fullPath).Length} bytes)");
            using FileStream fs = File.OpenRead(fullPath);
            fs.CopyTo(stream);
            stream.Flush(); // Ensure every byte is pushed out
        }
        else
        {
            // THIS IS THE IMPORTANT PART:
            Console.WriteLine($"[ERROR] File NOT found at: {fullPath}");
            Console.WriteLine("Check your casing! Linux is case-sensitive.");
        }
    }
    
}