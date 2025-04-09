using Blog.Entity.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.FluentValidations
{
    public class ArticleValidator : AbstractValidator<Article>
    {
        public ArticleValidator()
        {
            RuleFor(x=>x.Title).NotEmpty().NotNull().MinimumLength(3).MaximumLength(150).WithName("Başlık");
            RuleFor(x => x.Description).NotEmpty().NotNull().MinimumLength(3).MaximumLength(250).WithName("Açıklama");
            RuleFor(x => x.Content).NotEmpty().NotNull().MinimumLength(10).MaximumLength(1500).WithName("İçerik");
        }
    }
}
