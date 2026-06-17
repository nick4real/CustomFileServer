using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace CFS.Domain.Entities;

public class Product
{
    [Required]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public IDictionary<string, string> Parameters { get; set; }
}
