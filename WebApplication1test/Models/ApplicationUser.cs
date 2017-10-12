﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1test.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public bool IsScenarioManager { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsStudent { get; set; }
    }
}
