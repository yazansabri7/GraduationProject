using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DTO.DtoRequests
{
    public class UploadProfilePictureRequest
    {
        public IFormFile File { get; set; }
    }
}
