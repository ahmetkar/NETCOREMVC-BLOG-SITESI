using Blog.Data.UnitOfWorks;
using Blog.Entity.Entities;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Blog.Web.Filters.ArticleVisitors
{
    public class ArticleVisitorFilter : IAsyncActionFilter
    {
        private readonly IUnitOfWorkk unitOfWorkk;

        public ArticleVisitorFilter(IUnitOfWorkk unitOfWorkk)
        {
            this.unitOfWorkk = unitOfWorkk;
        }

        //public bool Disable { get; set; }
        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //if (Disable) return next();
            List<Visitor> visitors = unitOfWorkk.GetRepository<Visitor>().GetAllAsync().Result;

            string getIp = context.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            string getUserAgent = context.HttpContext.Request.Headers["User-Agent"];
            Visitor visitor = new() { IpAdress = getIp, UserAgent = getUserAgent };
            
            if(visitors.Any(x=>x.IpAdress == visitor.IpAdress))
            {
                return next();
            }else
            {
                 unitOfWorkk.GetRepository<Visitor>().AddAsync(visitor);
                 unitOfWorkk.Save();
            }
            return next();

            
        }
    }
}
