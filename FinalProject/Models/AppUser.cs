using Microsoft.AspNetCore.Identity;
using System;

namespace FinalProject.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsDeactive { get; set; }
        public DateTime? DeactivationTime { get; set; }
    }
}
