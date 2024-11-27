using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class RegisterViewModel
    {
        [EmailAddress]
        public string Email { get; set; }
        [MaxLength(50)]
        public string Fname { get; set; }
        [MaxLength(50)]
        public string Lname { get; set; }
        [PasswordPropertyText]
        public string Password { get; set; }
        [Phone]
        public string Mobile { get; set; }
        [MaxLength(100)]
        public string Address { get; set; }
        public Guid SystemId { get; set; }
       

    }
}
