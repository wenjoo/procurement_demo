using Microsoft.AspNetCore.Http;

namespace ProcureDemo.Services
{
    public class StorageService
    {
        private readonly IWebHostEnvironment _env;
        public StorageService(IWebHostEnvironment env) => _env = env;

        public async Task<string> SaveInvoiceAsync(IFormFile file)
        {
            var uploadsDir = Path.Combine(_env.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadsDir);
            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            var fullPath = Path.Combine(uploadsDir, fileName);
            using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);
            return $"/uploads/{fileName}"; // web path
        }
    }
}
