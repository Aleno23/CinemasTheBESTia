﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CinemasTheBESTia.IdentityServer.Data.Context
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }

    }
}
