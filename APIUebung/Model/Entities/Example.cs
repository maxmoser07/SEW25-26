using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

[Table("Examples")]
public class Example
{
    public int Id { get; set; }
    public int Value1  { get; set; }
    public int Value2  { get; set; }
}