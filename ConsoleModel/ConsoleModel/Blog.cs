namespace ConsoleModel;

public class Blog
{
    public int Id { get; set; }
    public string Topic { get; set; }
    public List<Post> Posts { get; } = new List<Post>();
}