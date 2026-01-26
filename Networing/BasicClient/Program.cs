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

// Look for data BEFORE creating the file
    if (stream.CanRead)
    {
        // Check if there is actually data waiting
        // We create the file ONLY when we start receiving bytes
        string savePath = Path.Combine(Directory.GetCurrentDirectory(), "downloaded_" + requestedFile);
    
        using (FileStream fs = File.Create(savePath))
        {
            Console.WriteLine("Receiving data...");
            stream.CopyTo(fs);
        
            // If the file is still 0 bytes, the server found nothing
            if (fs.Length == 0) 
            {
                Console.WriteLine("Warning: Received 0 bytes. Check server logs.");
            }
            Console.WriteLine($"Success! Saved to: {savePath}");
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Connection Error: {ex.Message}");
}