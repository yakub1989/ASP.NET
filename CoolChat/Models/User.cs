using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models;

public class User
{
    public int Id{get; set;}
    [Required]
    public string Username {get; set;}
    [Required]
    [DataType(DataType.Password)]
    public string Password {get; set;}
}