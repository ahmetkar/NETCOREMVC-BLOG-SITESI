using Blog.Entity.DTOs.Images;
using Blog.Entity.Entities;
using Blog.Entity.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Services.Abstractions
{
    public interface IImageService
    {
        Task SafeDeleteImageAsync(Guid? imageId);
        Task ForceDeleteImageAsync(Image? image = null, Guid? imageId = null);
        Task<Guid> ImageUpload(string Title, IFormFile imageFile, ImageType type, string userEmail);
        Task UndoDeleteImageAsync(Guid? imageId);
        Task<List<ImageViewModel>?> GetAllImagesAsync();
        Task<List<ImageViewModel>?> GetAllDeletedImagesAsync();
        Task SaveAsync();
    }
}
