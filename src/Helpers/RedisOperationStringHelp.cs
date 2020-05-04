using Microsoft.Extensions.Options;
using NewAge.Infra.Exceptions;
using NewAge.Infra.Extensions;
using NewAge.Infra.Helpers;
using NewAge.Redis.Const;
using NewAge.Redis.Interfaces;
using NewAge.Redis.Options;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAge.Redis.Helpers
{
    public partial class RedisOperationHelp
    {
        /// <summary>
        /// 保存字符串
        /// </summary>
        public async Task<bool> StringSetAsync(string key, string value, TimeSpan? expiry = default)
        {
            if (value.IsNullOrEmpty())
                throw new MyException(nameof(value));
            return await redisBase.DoSave(db => db.StringSetAsync(RedisPrefixKey.StringPrefixKey + key, value, expiry));
        }
        /// <summary>
        /// 保存对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public async Task<bool> StringSetAsync<T>(string key, T value, TimeSpan? expiry = default(TimeSpan?))
        {
            if (value == null)
            {
                throw new MyException(nameof(value));
            }
            return await redisBase.DoSave(db => db.StringSetAsync(RedisPrefixKey.StringPrefixKey + key, value.ToJson(), expiry));
        }

        /// <summary>
        /// 保存集合对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public async Task<bool> StringSetAsync<T>(string key, List<T> value, TimeSpan? expiry = default)
        {
            if (value == null || value.Count() <= 0)
            {
                throw new MyException(nameof(value));
            }
            key = RedisPrefixKey.StringPrefixKey + key;
            List<T> li = new List<T>();
            foreach (var item in value)
            {
                li.Add(item);
            }
            return await redisBase.DoSave(db => db.StringSetAsync(key, li.ToJson(), expiry));
        }

        /// <summary>
        /// 获取字符串
        /// </summary>
        public async Task<RedisValue> StringGetAsync(string key)
        {
            return await redisBase.DoSave(db => db.StringGetAsync(RedisPrefixKey.StringPrefixKey + key));
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public async Task<T> StringGetAsync<T>(string key)
        {
            key = RedisPrefixKey.StringPrefixKey + key;
            var value = await redisBase.DoSave(db => db.StringGetAsync(key));
            if (value.ToString() == null)
            {
                return default;
            }
            return value.ToStr().JsonTo<T>();
        }
        /// <summary>
        /// 自增
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public Task<long> StringIncrementAsync(string key, long value = 1)
        {
            key = RedisPrefixKey.StringPrefixKey + key;
            return redisBase.DoSave(db => db.StringIncrementAsync(key, value));
        }

        /// <summary>
        /// 递减
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<long> StringDecrementAsync(string key, long value = 1)
        {
            key = RedisPrefixKey.StringPrefixKey + key;
            return redisBase.DoSave(db => db.StringDecrementAsync(key, value));
        }
    }
}
