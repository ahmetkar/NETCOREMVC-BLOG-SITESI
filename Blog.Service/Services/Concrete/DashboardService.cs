using Blog.Data.UnitOfWorks;
using Blog.Entity.Entities;
using Blog.Service.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Services.Concrete
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWorkk unitOfWorkk;

        public DashboardService(IUnitOfWorkk unitOfWorkk)
        {
            this.unitOfWorkk = unitOfWorkk;
        }

        public async Task<List<int>> GetYearlyArticleCount()
        {
            var articles = await unitOfWorkk.GetRepository<Article>().GetAllAsync(x=> !x.IsDeleted);
            var startDate = DateTime.Now.Date;
            startDate = new DateTime(startDate.Year,1,1);

            List<int> datas = new();
            
            for (int i=1;i<=12;i++)
            {
                var startedDate = new DateTime(startDate.Year,i,1);
                var endedDate = startedDate.AddMonths(1);
                int data = articles.Where(x=>x.CreatedDate >= startedDate && x.CreatedDate<endedDate).Count();
                datas.Add(data);

            }

            return datas;
         }

        public async Task<int> GetTotalArticleCount()
        {
            var articleCount = await unitOfWorkk.GetRepository<Article>().GetAllAsync(x=>!x.IsDeleted);
            var count = articleCount.Count();
            return count;
        }

        public async Task<int> GetTotalViewCount()
        {
            var articleCount = await unitOfWorkk.GetRepository<Article>().GetAllAsync(x=>!x.IsDeleted);
            var count = 0;
            foreach(var i in articleCount)
            {
                count += i.ViewCount;
            }
            return count;
        }
    }
}
