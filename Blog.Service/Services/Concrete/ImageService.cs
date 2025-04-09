using AutoMapper;
using Blog.Data.UnitOfWorks;
using Blog.Entity.DTOs.Images;
using Blog.Entity.Entities;
using Blog.Entity.Enums;
using Blog.Service.Extensions;
using Blog.Service.Helpers.Images;
using Blog.Service.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Services.Concrete
{
    public class ImageService : IImageService
    {
        private readonly IImageHelper imageHelper;
        private readonly IUnitOfWorkk unitOfWorkk;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ClaimsPrincipal user;

        public ImageService(IImageHelper imageHelper,IUnitOfWorkk unitOfWorkk,IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.imageHelper = imageHelper;
            this.unitOfWorkk = unitOfWorkk;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            user = httpContextAccessor.HttpContext.User;
        }


        public async Task<List<ImageViewModel>?> GetAllImagesAsync()
        {
            var images = await unitOfWorkk.GetRepository<Image>().GetAllAsync(x=>!x.IsDeleted,x=>x.Articles);
            if (images != null)
            {
                var map = mapper.Map<List<ImageViewModel>>(images);
                return map;
            }

            return null;

        }

        public async Task<List<ImageViewModel>?> GetAllDeletedImagesAsync()
        {
            var images = await unitOfWorkk.GetRepository<Image>().GetAllAsync(x => x.IsDeleted, x => x.Articles);
            if (images != null)
            {
                var map = mapper.Map<List<ImageViewModel>>(images);
                return map;
            }

            return null;

        }

        public async Task SafeDeleteImageAsync(Guid? imageId)
        {
            var userEmail = user.GetLoggedInEmail();
            if (imageId.HasValue)
            {
                var image = await unitOfWorkk.GetRepository<Image>().GetByGuidAsync(imageId.Value);
                image.IsDeleted = true;
                image.DeleteDate = DateTime.Now;
                image.DeletedBy = userEmail;
                await unitOfWorkk.GetRepository<Image>().UpdateAsync(image);
            }
        }

        public async Task ForceDeleteImageAsync(Image? image = null,Guid? imageId = null)
        {
            if (image != null)
            {
                imageHelper.Delete(image.FileName);
                await unitOfWorkk.GetRepository<Image>().DeleteAsync(image);
            }

            if (imageId != null)
            {
                var delete = await unitOfWorkk.GetRepository<Image>().GetByGuidAsync(imageId.Value);
                imageHelper.Delete(image.FileName);
                await unitOfWorkk.GetRepository<Image>().DeleteAsync(delete);
            }

            
        }

        public async Task SaveAsync()
        {
            await unitOfWorkk.SaveAsync();
        }

        public async Task UndoDeleteImageAsync(Guid? imageId)
        {
            if (imageId.HasValue)
            {
                var image = await unitOfWorkk.GetRepository<Image>().GetByGuidAsync(imageId.Value);
                image.IsDeleted = false;
                image.DeleteDate = null;
                image.DeletedBy = null;
                await unitOfWorkk.GetRepository<Image>().UpdateAsync(image);
            }
        }

        public async Task<Guid> ImageUpload(string Title, IFormFile imageFile, ImageType type, string userEmail)
        {
            var imageUpload = await imageHelper.Upload(Title, imageFile, type);
            Image image = new Image(imageUpload.FullName, imageFile.ContentType, userEmail);
            await unitOfWorkk.GetRepository<Image>().AddAsync(image);
            return image.Id;
        }
    }
}
