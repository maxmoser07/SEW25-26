// See https://aka.ms/new-console-template for more information
using Model.Entities;

using (MyDbContext context = new MyDbContext())
{
    foreach (var d in context.Demos)
    {
        Console.WriteLine($"{d.Id}/{d.Value}");
    }
}