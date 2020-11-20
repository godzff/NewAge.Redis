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
        /// 锁
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public async Task<bool> LockAsync(string key, string value, TimeSpan expiry)
        {
            return await _redisBase.DoSave((database) =>
            {
                return database.LockTakeAsync(RedisPrefixKey.LockPrefixKey + key, value, expiry);
            });
        }

        /// <summary>
        /// 释放锁
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public async Task<bool> LockReleaseAsync(string key, string value)
        {
            return await _redisBase.DoSave((database) =>
            {
                return database.LockReleaseAsync(RedisPrefixKey.LockPrefixKey + key, value);
            });
        }
    }
}
