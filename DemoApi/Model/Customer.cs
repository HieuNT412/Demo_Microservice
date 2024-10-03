using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApi.Model
{
    public class Customer
    {
        [Key]
        [MaxLength(100)]
        public string Username {get; set;}
        [MaxLength(100)]
        [Required]
        public string Fullname {get; set;}
        [MaxLength(100)]
        [Required]
        public string Password {get; set;}
    }
}