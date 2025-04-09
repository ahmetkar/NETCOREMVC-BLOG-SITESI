using Blog.Entity.DTOs.Images;
using Blog.Entity.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blog.Service.Helpers.Images
{
    public class ImageHelper : IImageHelper
    {
        private readonly IWebHostEnvironment env;
        private readonly string wwwroot;
        private const string imgFolder = "images";
        private const string articleImageFolder = "article-images";
        private const string userImageFolder = "user-images";
        private const string siteImageFolder = "site-images";

        public ImageHelper(IWebHostEnvironment env) {
            this.env = env;
            wwwroot = env.WebRootPath;
        }

        private string NormalizeFileName(string text)
        {
            string[,] turkishChars =
            {
            {"Ç", "C"}, {"Ğ", "G"}, {"İ", "I"}, {"Ö", "O"}, {"Ş", "S"}, {"Ü", "U"},
            {"ç", "c"}, {"ğ", "g"}, {"ı", "i"}, {"ö", "o"}, {"ş", "s"}, {"ü", "u"}
        };

            for (int i = 0; i < turkishChars.GetLength(0); i++)
            {
                text = text.Replace(turkishChars[i, 0], turkishChars[i, 1]);
            }

            text = Regex.Replace(text, "[\\/:*?\"<>|]", " ").Trim();


            return text;
        }

        public void Delete(string imageName)
        {
           var fileToDelete = Path.Combine($"{wwwroot}/{imgFolder}", imageName);
            if (File.Exists(fileToDelete))
            {
                File.Delete(fileToDelete);
            }
        }

        public async Task<ImageUploadModel> Upload(string name, IFormFile imageFile,ImageType imageType, string folderName = null)
        {
            folderName ??= imageType == ImageType.User ? userImageFolder : imageType == ImageType.Article ? articleImageFolder : siteImageFolder;

            if (!Directory.Exists($"{wwwroot}/{imgFolder}/{folderName}")) {
                Directory.CreateDirectory($"{wwwroot}/{imgFolder}/{folderName}");
            }

            string oldFileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
            string fileExt = Path.GetExtension(imageFile.FileName);

            name = NormalizeFileName(name);

            DateTime dateTime = DateTime.Now;

            string newFileName = $"{name}_{dateTime.Millisecond}{fileExt}";
            string? path = Path.Combine($"{wwwroot}/{imgFolder}/{folderName}",newFileName);

            await using var stream = new FileStream(path,FileMode.Create,FileAccess.Write,FileShare.None,1024*1024,useAsync:false);
            await imageFile.CopyToAsync(stream);
            await stream.FlushAsync();

            string message = imageType == ImageType.User ? $"{newFileName} isimli kullanıcı resmi başarıyla eklendi." : imageType == ImageType.Article ? $"{newFileName} isimli blog resmi başarıyla eklendi." : $"{newFileName} adlı site resmi başarıyla eklendi.";

            return new ImageUploadModel()
            {
                FullName = $"{folderName}//{newFileName}"
            };

        }

    }
}
