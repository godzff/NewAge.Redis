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
        /// SortedSet 新增
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public async Task<bool> SortedSetAddAsync<T>(string key, T value, double score)
        {
            if (value == null)
            {
                throw new MyException(nameof(value));
            }
            return await redisBase.DoSave(db => db.SortedSetAddAsync(RedisPrefixKey.SortedSetPrefixKey + key, value.ToJson(), score));
        }
        /// <summary>
        /// 获取SortedSet的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> SortedSetGetAsync<T>(string key, double score)
        {
            var result = await redisBase.DoSave(db => db.SortedSetRangeByScoreAsync(RedisPrefixKey.SortedSetPrefixKey + key, score));
            return result.ToStr().JsonTo<T>();
        }
        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<long> SortedSetLengthAsync(string key)
        {
            return await redisBase.DoSave(db => db.SortedSetLengthAsync(RedisPrefixKey.SortedSetPrefixKey + key));
        }
        /// <summary>
        /// 移除SortedSet
        /// </summary>
        public async Task<bool> SortedSetRemoveAsync<T>(string key, T value)
        {
            if (value == null)
            {
                throw new MyException(nameof(value));
            }
            return await redisBase.DoSave(db => db.SortedSetRemoveAsync(RedisPrefixKey.SortedSetPrefixKey + key, value.ToJson()));
        }
    }
}
