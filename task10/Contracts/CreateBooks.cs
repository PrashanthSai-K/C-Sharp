using System;
using System.ComponentModel.DataAnnotations;

namespace task10.Contarcts;

public class CreateBooks
{
    [Required]
    [StringLength(100)]
    public string? Name { get; set; }
    [Required]
    [StringLength(100)]
    public string? Author { get; set; }
    [Required]
    public double Price { get; set; }
    [Required]
    public bool IsCompleted { get; set; }
}
