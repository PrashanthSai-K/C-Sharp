using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace task10.Models;

public class Book
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id{get; set;}
    public string? Name{get;set;}
    public string? Author{get;set;}
    public double Price{get;set;}
    public bool IsCompleted{get;set;}

}
