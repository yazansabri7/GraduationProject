using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DTO.DtoRequests
{
    public class TokenApiRequest
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}
