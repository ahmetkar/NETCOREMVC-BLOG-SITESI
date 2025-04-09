using Blog.Data.Context;
using Blog.Data.Repositories.Abstractions;
using Blog.Data.Repositories.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.UnitOfWorks
{
    public class UnitOfWorkk : IUnitOfWorkk
    {
        private readonly AppDbContext dbContext;
        public UnitOfWorkk(AppDbContext appDbContext) {

            this.dbContext = appDbContext;
        }

        public async ValueTask DisposeAsync()
        {
            await dbContext.DisposeAsync();
        }

        public int Save()
        {
            return dbContext.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        IRepository<T> IUnitOfWorkk.GetRepository<T>()
        {
            return new Repository<T>(dbContext);
        }
    }
}
