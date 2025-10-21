using System;
using System.ComponentModel.DataAnnotations;


namespace SupportWebApp.Models;

public class SupportMessage
{
    public string id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public string name { get; set; }

    [Required]
    public string email { get; set; }

    [Required]
    public string category { get; set; }

    [Required]
    public string subject { get; set; }

    [Required]
    public string message { get; set; }

    public DateTime createdAt { get; set; } = DateTime.UtcNow;
}