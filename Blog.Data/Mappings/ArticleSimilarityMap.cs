using Blog.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Mappings
{
    public class ArticleSimilarityMap : IEntityTypeConfiguration<ArticleSimilarity>
    {
        public void Configure(EntityTypeBuilder<ArticleSimilarity> builder)
        {
            builder.HasKey(ar => new { ar.BaseArticleId
                , ar.TargetArticleId });

            builder.HasOne(ar => ar.BaseArticle)
                .WithMany(a => a.BaseArticleSimilarities)
                .HasForeignKey(ar => ar.BaseArticleId)
                .OnDelete(DeleteBehavior.Restrict);  

            builder
                .HasOne(ar => ar.TargetArticle)
                .WithMany(a=>a.TargetArticleSimilarities)
                .HasForeignKey(ar => ar.TargetArticleId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
