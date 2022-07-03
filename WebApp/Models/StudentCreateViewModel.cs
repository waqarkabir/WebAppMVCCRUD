using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class StudentCreateViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [MaxLength(13), MinLength(13)]
        public string Cnic { get; set; }
    }
}
