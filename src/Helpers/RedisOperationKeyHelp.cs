using Microsoft.Extensions.Options;
using NewAge.Infra.Exceptions;
using NewAge.Infra.Extensions;
using NewAge.Infra.Helpers;
using NewAge.Redis.Const;
using NewAge.Redis.Enums;
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
        /// 移除key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="eKeyOperator"></param>
        public async Task<bool> KeyRemoveAsync(string key, EKeyOperator eKeyOperator = default)
        {
            key = eKeyOperator switch
            {
                EKeyOperator.String => RedisPrefixKey.StringPrefixKey + key,
                EKeyOperator.List => RedisPrefixKey.ListPrefixKey + key,
                EKeyOperator.Set => RedisPrefixKey.SetPrefixKey + key,
                EKeyOperator.Hash => RedisPrefixKey.HashPrefixKey + key,
                EKeyOperator.SortedSet => RedisPrefixKey.SortedSetPrefixKey + key,
                _ => RedisPrefixKey.StringPrefixKey + key,
            };
            return await _redisBase.DoSave(db => db.KeyDeleteAsync(key));
        }
        /// <summary>
        /// 移除key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="eKeyOperator"></param>
        public async Task<long> KeyRemoveAsync(List<string> key, EKeyOperator eKeyOperator = default)
        {
            if (key == null || key.Count() <= 0)
            {
                throw new MyException(nameof(key));
            }

            List<string> removeList = new List<string>();
            key.ForEach(item =>
            {
                switch (eKeyOperator)
                {
                    case EKeyOperator.String:
                        removeList.Add(RedisPrefixKey.StringPrefixKey + item);
                        break;
                    case EKeyOperator.List:
                        removeList.Add(RedisPrefixKey.ListPrefixKey + item);
                        break;
                    case EKeyOperator.Set:
                        removeList.Add(RedisPrefixKey.SetPrefixKey + item);
                        break;
                    case EKeyOperator.Hash:
                        removeList.Add(RedisPrefixKey.HashPrefixKey + item);
                        break;
                    case EKeyOperator.SortedSet:
                        removeList.Add(RedisPrefixKey.SortedSetPrefixKey + item);
                        break;
                    default:
                        removeList.Add(RedisPrefixKey.StringPrefixKey + item);
                        break;
                }
            });
            return await _redisBase.DoSave(db => db.KeyDeleteAsync(_redisBase.ConvertRedisKeys(removeList)));
        }
        /// <summary>
        /// 判断key是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <param name="eKeyOperator"></param>
        public async Task<bool> KeyExistsAsync(string key, EKeyOperator eKeyOperator = default)
        {
            key = eKeyOperator switch
            {
                EKeyOperator.String => RedisPrefixKey.StringPrefixKey + key,
                EKeyOperator.List => RedisPrefixKey.ListPrefixKey + key,
                EKeyOperator.Set => RedisPrefixKey.SetPrefixKey + key,
                EKeyOperator.Hash => RedisPrefixKey.HashPrefixKey + key,
                EKeyOperator.SortedSet => RedisPrefixKey.SortedSetPrefixKey + key,
                _ => RedisPrefixKey.StringPrefixKey + key,
            };
            return await _redisBase.DoSave(db => db.KeyExistsAsync(key));
        }
        /// <summary>
        /// 设置Key过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <param name="eKeyOperator"></param>
        /// <returns></returns>
        public async Task<bool> KeyExpireAsync(string key, TimeSpan? expiry = default, EKeyOperator eKeyOperator = default)
        {
            key = eKeyOperator switch
            {
                EKeyOperator.String => RedisPrefixKey.StringPrefixKey + key,
                EKeyOperator.List => RedisPrefixKey.ListPrefixKey + key,
                EKeyOperator.Set => RedisPrefixKey.SetPrefixKey + key,
                EKeyOperator.Hash => RedisPrefixKey.HashPrefixKey + key,
                EKeyOperator.SortedSet => RedisPrefixKey.SortedSetPrefixKey + key,
                _ => RedisPrefixKey.StringPrefixKey + key,
            };
            return await _redisBase.DoSave(db => db.KeyExpireAsync(key, expiry));
        }
    }
}
