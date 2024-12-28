using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Model;

public partial class LoginModel
{
    [Key]
    public int IdUser { get; set; }
    [Required, EmailAddress, DataType(DataType.EmailAddress)]
    public string? Username { get; set; }
    [Required, MinLength(5)]
    public string? Password { get; set; }

}
