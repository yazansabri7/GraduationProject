using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DTO.DtoRequests
{
    public class ResetPasswordRequest
    {
        public string NewPassword { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }

    }
}
