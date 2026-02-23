namespace ConsoleModel;

public class Post
{
    public int Id { get; set; }
    public string Text { get; set; }
    public int BlogId { get; set; }
    public Blog Blog { get; set; } = null!;
}