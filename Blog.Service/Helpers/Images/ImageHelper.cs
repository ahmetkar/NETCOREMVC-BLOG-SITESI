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
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;
using Google.Apis.Auth.OAuth2;
using System.Drawing;

namespace Blog.Service.Helpers.Images
{
    public class ImageHelper : IImageHelper
    {
   
        IConfiguration _configuration;

        private StorageClient _storageClient;
        private string _bucketName;
        public ImageHelper(IConfiguration configuration) {
       
            _configuration = configuration;

            string path = _configuration["GoogleCloudStorage:CredentialPath"]!;
            if (path == null)
            {
                throw new InvalidOperationException("Credential bulunamadı."); 
            }
           
            var json = File.ReadAllText(path);

            var serviceAccountCredential =
                CredentialFactory.FromJson<ServiceAccountCredential>(json);

            var credential =
                    serviceAccountCredential.ToGoogleCredential();

            this._storageClient = StorageClient.Create(credential);

            this._bucketName = _configuration["GoogleCloudStorage:BucketName"]
                ?? throw new InvalidOperationException(
                    "GoogleCloudStorage:BucketName bulunamadı.");

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

        public async void Delete(string url)
        {
            try {
            var parts = url.Split('/',StringSplitOptions.RemoveEmptyEntries);
            var objectName = string.Join('/',parts.Skip(3));    
            await _storageClient.DeleteObjectAsync(
                _bucketName,
                objectName
            );
            }catch(Exception ex)
            {
                Console.WriteLine("Error deleting image -> "+url+" -> "+ex.Message);
            }
        }

        public async Task<ImageUploadModel> Upload(string name, IFormFile imageFile,ImageType imageType, string folderName = null)
        {   

        if (imageFile == null || imageFile.Length == 0){
            throw new ArgumentException("Dosya boş.");
        }

        var type = imageType;
        var folder = "";

        if(type == ImageType.User) folder = "user_images";
        else if(type == ImageType.Article) folder = "article_images";
        else if(type == ImageType.Site) folder = "site_images";
        
        var normalizedName = NormalizeFileName(name);
        var extension = Path.GetExtension(imageFile.FileName);
        var fileName = $"{normalizedName}-{Guid.NewGuid():N}{extension}";
        var objectName = $"{folder.TrimEnd('/')}/{fileName}";

        await using var stream = imageFile.OpenReadStream();

        await _storageClient.UploadObjectAsync(_bucketName,objectName,imageFile.ContentType,stream);
          
        return new ImageUploadModel {FullName=GetPublicUrl(objectName)};
        }

        public string GetPublicUrl(string objectName)
        {
                return $"https://storage.googleapis.com/{_bucketName}/{objectName}";
        }

    }

    
}
