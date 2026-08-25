using Blog.Entity.DTOs.Article;
using Blog.Entity.DTOs.Comment;
using Blog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Services.Abstractions
{
    public interface ICachingService
    {
        public Task<T?> GetCached<T>(string key);
        public Task<bool> SetCached<T>(string key,T obj,int number);

        public Task<bool> RemoveCached(string key);

        public Task<bool> RemoveAllKeysFromCacheList(string tagkey);

        Task<bool> RemoveKeysFromCacheList(string tagkey,string[] keys,int number);

        public Task<bool> RemoveKeyFromCacheList(string tagkey,string key,int number);

        public Task<bool> AddKeyToCacheList(string key,string tagkey,int number);

        public Task<bool> SaveCacheList(string tagkey,List<string> list,int number);

    }
}