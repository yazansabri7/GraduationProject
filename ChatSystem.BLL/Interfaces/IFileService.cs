using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.BLL.Interfaces
{
    public interface IFileService
    {
        Task<string?> UploadAsync(IFormFile file , string folder);
        Task DeleteAsync(string folder , string fileName);
    }
}
