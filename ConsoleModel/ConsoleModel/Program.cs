using ConsoleModel;

Blog sew =new Blog {Id = 1, Topic = "Sew"};
Post p = new Post {Id = 1, BlogId = 1, Text = "Suppa"};

p.Blog = sew;
Console.WriteLine(p.Blog.Topic);

Console.WriteLine(sew.Posts[0].Text);
sew.Posts.Add(p);