using AutoMapper;
using Blog.Data.UnitOfWorks;
using Blog.Service.Services.Abstractions;
using Enyim.Caching;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blog.Service.Services.Concrete
{
    public  class CachingService : ICachingService
    {
        private readonly IMemcachedClient _cache;

        public CachingService(IMemcachedClient cache)
        {
            _cache = cache;
        }

        public async Task<T?> GetCached<T>(string key)
        {
            var cached = await _cache.GetValueAsync<T>(key);

            if(cached!=null) return cached;

            return default;
        }

        public async Task<bool> SetCached<T>(string key, T obj, int number)
        {   
            bool set = await _cache.SetAsync(key,obj,TimeSpan.FromMinutes(number));
            return set;
        }

        public async Task<bool> RemoveCached(string key)
        {
            bool remove = await _cache.RemoveAsync(key);
            return remove;         
        }



        public async Task<bool> RemoveAllKeysFromCacheList(string tagkey)
        {
            var list = await GetCached<List<string>>(tagkey);
            if(list==null) return false;
            
            foreach (var key in list)
            {
                bool del = await RemoveCached(key);
                if(del==false) return false;
            }
            bool deltags= await RemoveCached(tagkey);

            return deltags;
        }



        public async Task<bool> RemoveKeyFromCacheList(string tagkey,string key,int number)
        {
            var list = await GetCached<List<string>>(tagkey);
            if(list==null) return false;
            await RemoveCached(key);
            list.Remove(key);
            return await SaveCacheList(tagkey,list,number);
        }


        public async Task<bool> RemoveKeysFromCacheList(string tagkey,string[] keys,int number)
        {
            var list = await GetCached<List<string>>(tagkey);
            if(list==null) return false;
            foreach(string k in keys){
                await RemoveCached(k);
                list.Remove(k);
            }
            return await SaveCacheList(tagkey,list,number);
        }


        public async Task<bool> AddKeyToCacheList(string key,string tagkey,int number)
        {
            var list = await GetCached<List<string>>(tagkey);
            if (list == null)
            {
                list = new List<string>();
            }

            if (!list.Contains(key))
            {
                list.Add(key);
            }
            return await SaveCacheList(tagkey,list,number);
        }

        public async Task<bool> SaveCacheList(string tagkey,List<string> list,int number)
        {   
            bool set = await _cache.SetAsync(tagkey,list,TimeSpan.FromMinutes(number));
            return set;
        }

    }

}
