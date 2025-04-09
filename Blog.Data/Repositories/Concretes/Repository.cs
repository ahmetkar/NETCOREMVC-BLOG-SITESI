using Blog.Core.Entities;
using Blog.Data.Context;
using Blog.Data.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Blog.Data.Repositories.Concretes
{
    public class Repository<T>: IRepository<T> where T : class,IEntityBase,new()
    {
        private readonly AppDbContext dbContext;

        public Repository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        private DbSet<T> Table { get => dbContext.Set<T>(); }

        public async Task AddAsync(T entity)
        {
            await Table.AddAsync(entity);
        }



        public async Task<List<T>> GetAllAndSelectAsync(Expression<Func<T, T>> selector = null,
            Expression <Func<T, bool>> predicate = null, 
            params Expression<Func<T, object>>[] includeProperties)
        {

            IQueryable<T> query = Table;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (includeProperties.Any())
            {
                foreach (var item in includeProperties)
                {
                    query = query.Include(item);
                }
            }

            if (selector != null)
            {
                query = query.Select(selector);
            }

            return await query.ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T,bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties ) {

            IQueryable<T> query = Table;
            if(predicate != null)
            {
                query = query.Where(predicate);
            }

            if (includeProperties.Any())
            {
                foreach (var item in includeProperties)
                {
                    query = query.Include(item);
                }
            }


            return await query.ToListAsync();
        }

        public async Task<List<T>> GetAllWithLimitAsync(int limit,Expression<Func<T, bool>> predicate = null,bool desc = true, Expression<Func<T, object>> orderBypredicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Table;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (includeProperties.Any())
            {
                foreach (var item in includeProperties)
                {
                    query = query.Include(item);
                }
            }
            if(desc) query = query.OrderByDescending(orderBypredicate);
            else query = query.OrderBy(orderBypredicate);

            query = query.Take(limit);

            return await query.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Table;
            query = query.Where(predicate);

            if (includeProperties.Any())
            {
                foreach (var item in includeProperties)
                {
                    query = query.Include(item);
                }
            }

            return await query.SingleAsync();


        }

        public async Task<T> GetByGuidAsync(Guid id)
        {
           
            return await Table.FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            await Task.Run(() =>
            {
                Table.Update(entity);
            });
            return entity;
        }


        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await Table.AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Table;

            if (includeProperties.Any())
            {
                foreach (var item in includeProperties)
                {
                    query = query.Include(item);
                }
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            int count = await query.CountAsync();
            if (count == 0 || count == 1) { return count; }
            return -1;
        }

        public async Task DeleteAsync(T entity)
        {
            await Task.Run(() =>
            {
                Table.Remove(entity);
            });
        }

    }
    }
