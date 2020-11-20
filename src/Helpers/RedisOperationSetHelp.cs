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
        /// 新增
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public async Task<bool> SetAddAsync<T>(string value)
        {
            if (value.IsNullOrEmpty())
                throw new MyException(nameof(value));
            //反射实体的信息
            var type = typeof(T);
            string key = RedisPrefixKey.SetPrefixKey + type.Name;
            return await _redisBase.DoSave(db => db.SetAddAsync(key, value));
        }
        /// <summary>
        /// 移除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public async Task<bool> SetRemoveAsync<T>(string value)
        {
            if (value.IsNullOrEmpty())
                throw new MyException(nameof(value));
            //反射实体的信息
            var type = typeof(T);
            string key = RedisPrefixKey.SetPrefixKey + type.Name;
            return await _redisBase.DoSave(db => db.SetRemoveAsync(key, value));
        }
        /// <summary>
        /// 取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public async Task<string[]> SetGetAsync<T>()
        {
            //反射实体的信息
            var type = typeof(T);
            string key = RedisPrefixKey.SetPrefixKey + type.Name;
            return (await _redisBase.DoSave(db => db.SetMembersAsync(key))).ToStringArray();
        }
        /// <summary>
        /// 取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public async Task<string[]> SetGetAsync(string key)
        {
            return (await _redisBase.DoSave(db => db.SetMembersAsync(RedisPrefixKey.SetPrefixKey + key))).ToStringArray();
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public async Task<bool> SetAddAsync(string key, string value)
        {
            if (value.IsNullOrEmpty())
                throw new MyException(nameof(value));
            return await _redisBase.DoSave(db => db.SetAddAsync(RedisPrefixKey.SetPrefixKey + key, value));
        }
        /// <summary>
        /// 移除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public async Task<bool> SetRemoveAsync(string key, string value)
        {
            if (value.IsNullOrEmpty())
                throw new MyException(nameof(value));
            return await _redisBase.DoSave(db => db.SetRemoveAsync(RedisPrefixKey.SetPrefixKey + key, value));
        }
    }
}
