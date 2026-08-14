using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ChatSystem.DAL.Models
{
    public enum Gender
    {
        Male=1,
        Female=2,
    }
    public class ApplicationUser : IdentityUser
    {
        
        public string FullName { get; set; }
        public string? ProfilePicture { get; set; }
        public string? City { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now; 
        public DateTime?  DateOfBirth { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Gender Gender { get; set; }
    




    }
}
