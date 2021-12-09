using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FinalProject.Extensions
{
    public static class Extension
    {
        public static bool IsImage(this IFormFile file)
        {
            if (file.ContentType.Contains("image/"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsMaxLength(this IFormFile file, int kb)
        {
            return file.Length / 1024 > kb;
        }
        public static async Task<string> SaveImageAsync(this IFormFile file, string folder)
        {
            string fileName = Guid.NewGuid().ToString() + file.FileName;
            string fullPath = Path.Combine(folder, fileName);
            using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return fileName;
        }
    }
}
