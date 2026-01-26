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
    string? requestedFile = sr.ReadLine();

    if (!string.IsNullOrEmpty(requestedFile))
    {
        // 2. Path.GetFileName strips out any "../" path traversal attempts
        string fileNameOnly = Path.GetFileName(requestedFile);
        string fullPath = Path.Combine(sharedFolder, fileNameOnly);

        // Linux is case-sensitive! Image.png != image.png
        if (File.Exists(fullPath))
        {
            Console.WriteLine($"Sending: {fullPath}");
            using FileStream fs = File.OpenRead(fullPath);
            fs.CopyTo(stream);
            stream.Flush();
        }
        else
        {
            Console.WriteLine($"File not found (Check casing): {fullPath}");
        }
    }
}