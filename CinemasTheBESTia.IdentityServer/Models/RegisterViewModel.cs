using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemasTheBESTia.IdentityServer.Models
{
    public class RegisterViewModel
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
        
        public string ReturnUrl { get; set; }
    }
}
