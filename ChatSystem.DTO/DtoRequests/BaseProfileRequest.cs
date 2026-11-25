using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ChatSystem.DTO.DtoRequests
{
    public class BaseProfileRequest
    {
        [StringLength(100, MinimumLength = 2)]
        public string? FullName { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [StringLength(100)]
        public string? City { get; set; }
    }
}
