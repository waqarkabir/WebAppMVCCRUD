using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cnic { get; set; }
    }
}
