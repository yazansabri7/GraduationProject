using ChatSystem.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.BLL.Services
{
    public class FileService : IFileService
    {
        public async Task DeleteAsync(string folder, string fileName)
        {
           var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "images",
                folder,
                fileName
            );
            if (File.Exists(path))
                File.Delete(path);
        }

        public async Task<string?> UploadAsync(IFormFile file, string folder)
        {
            if (file != null && file.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

                var path = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "images",
                    folder
                );

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                var filePath = Path.Combine(path, fileName);

                using (var stream = File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }

                return fileName;
            }

            return null;
        }
    }
}
