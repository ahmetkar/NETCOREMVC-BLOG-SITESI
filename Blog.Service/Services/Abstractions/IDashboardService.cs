using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Services.Abstractions
{
    public interface IDashboardService
    {
        Task<List<int>> GetYearlyArticleCount();
        Task<int> GetTotalArticleCount();
        Task<int> GetTotalViewCount();
    }
}
