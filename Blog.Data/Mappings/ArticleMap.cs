using Blog.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Mappings
{
    public class ArticleMap : IEntityTypeConfiguration<Article>
    {
        
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Article> builder)
        {

            /*
            builder.HasData(
                new Article
            {
                Id =Guid.Parse("{12dd1d68-af34-46a5-ae46-7842d39aed20}"),
                Title = "Deneme makale 1",
                Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                ViewCount = 1,
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries..",
                 CategoryID = Guid.Parse("{58252b86-ec84-4c93-b3df-e9b1bf39d3bf}"),
                ImageId = Guid.Parse("{17a74024-059f-4747-9849-1ea1613869cc}"),
                CreatedBy = "Admin test",
                CreatedDate = new DateTime(2023, 5, 15, 7, 0, 0),
                IsDeleted = false,
                UserId = Guid.Parse("{070f54e2-bf16-4d50-bd52-a7f9cd87c479}")

                },
            new Article {
                Id = Guid.Parse("{7eb33293-d3fe-4abe-9924-41a64f0138d7}"),
                Title = "Deneme makale 2",
                Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                ViewCount = 1,
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries..",
                CategoryID = Guid.Parse("{80ac9637-35b9-42d1-afb7-297242e1e7c5}"),
                ImageId = Guid.Parse("{21c11c2f-b0dd-43f8-bf95-1d0c2940fa2d}"),
                CreatedBy = "Admin test",
                CreatedDate = new DateTime(2023, 5, 15, 7, 0, 0),
                IsDeleted = false,
                UserId = Guid.Parse("{258794ef-c8f5-4ab5-8818-bcfd3218036a}")
            }
            );*/
            
        }
    }
}
