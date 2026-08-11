using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DAL.Models
{
    public class ApplicationUser : IdentityUser
    {
        
        public string FullName { get; set; }
        public string? ProfilePicture { get; set; }
        public string? City { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now; 
        public DateTime?  DateOfBirth { get; set; }
        public string Gender { get; set; }
        




    }
}
