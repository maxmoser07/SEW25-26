using System.Net.Sockets;

try
{
    using TcpClient client = new TcpClient("127.0.0.1", 2025);
    using NetworkStream stream = client.GetStream();

    StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };
    Console.Write("Enter exact filename (Case Sensitive): ");
    string? requestedFile = Console.ReadLine();
    
    if (string.IsNullOrEmpty(requestedFile)) return;

    writer.WriteLine(requestedFile);

    // Save to the current directory where the app is running
    string savePath = Path.Combine(Directory.GetCurrentDirectory(), "downloaded_" + requestedFile);
    
    using (FileStream fs = File.Create(savePath))
    {
        Console.WriteLine("Downloading from Fedora Server...");
        stream.CopyTo(fs);
    }

    Console.WriteLine($"Success! Saved to: {savePath}");
}
catch (Exception ex)
{
    Console.WriteLine($"Connection Error: {ex.Message}");
}