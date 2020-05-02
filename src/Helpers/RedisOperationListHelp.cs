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
        /// 存储list 集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public async Task ListSetAsync<T>(string key, List<T> value)
        {
            if (value != null && value.Count > 0)
            {
                foreach (var single in value)
                {
                    await redisBase.DoSave(db => db.ListRightPushAsync(RedisPrefixKey.ListPrefixKey + key, single.ToJson()));
                }
            }
        }
        /// <summary>
        /// 取list 集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        public async Task<List<T>> ListGetAsync<T>(string key)
        {
            var vList = await redisBase.DoSave(db => db.ListRangeAsync(RedisPrefixKey.ListPrefixKey + key));
            List<T> result = new List<T>();
            foreach (var item in vList)
            {
                result.Add(item.ToStr().JsonTo<T>());//反序列化
            }
            return result;
        }
        /// <summary>
        /// 取list 集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        public async Task<List<string>> ListGetAsync(string key)
        {
            var vList = await redisBase.DoSave(db => db.ListRangeAsync(RedisPrefixKey.ListPrefixKey + key));
            return vList.ToStringArray().ToList();
        }
        /// <summary>
        /// 删除list集合的某一项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">value值</param>
        public async Task<long> ListRemoveAsync<T>(string key, T value, long count = 0)
        {
            if (value == null)
                throw new MyException("值不能为空");
            return await redisBase.DoSave(db => db.ListRemoveAsync(RedisPrefixKey.ListPrefixKey + key, value.ToJson(), count));
        }


        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<long> ListLengthAsync(string key)
        {
            return await redisBase.DoSave(db => db.ListLengthAsync(RedisPrefixKey.ListPrefixKey + key));
        }

        /// <summary>
        /// 删除并返回存储在key上的列表的第一个元素。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<string> ListLeftPopAsync(string key)
        {
            return await redisBase.DoSave(db => db.ListLeftPopAsync(RedisPrefixKey.ListPrefixKey + key));
        }
        /// <summary>
        /// 往最后推送一个数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task ListRightPushAsync(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new MyException("值不能为空");
            await redisBase.DoSave(db => db.ListRightPushAsync(RedisPrefixKey.ListPrefixKey + key, value));
        }
        /// <summary>
        /// 删除并返回存储在key上的列表的第一个元素。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> ListLeftPopAsync<T>(string key)
        {
            return (await redisBase.DoSave(db => db.ListLeftPopAsync(RedisPrefixKey.ListPrefixKey + key))).ToStr().JsonTo<T>();
        }
        /// <summary>
        /// 往最后推送一个数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<long> ListRightPushAsync<T>(string key, T value)
        {
            if (value == null)
                throw new MyException("值不能为空");
            return await redisBase.DoSave(db => db.ListRightPushAsync(RedisPrefixKey.ListPrefixKey + key, value.ToJson()));
        }
        /// <summary>
        /// 往末尾推送多条数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<long> ListRightPushAsync<T>(string key, List<T> value)
        {
            if (value == null || value.Count <= 0)
                throw new MyException("值不能为空");
            List<RedisValue> redisValues = new List<RedisValue>();
            value.ForEach(item =>
            {
                redisValues.Add(item.ToJson());
            });
            return await redisBase.DoSave(db => db.ListRightPushAsync(RedisPrefixKey.ListPrefixKey + key, redisValues.ToArray()));
        }
        /// <summary>
        /// 往末尾推送多条数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<long> ListRightPushAsync(string key, string[] value)
        {
            if (value == null || value.Count() <= 0)
                throw new MyException("值不能为空");
            return await redisBase.DoSave(db => db.ListRightPushAsync(RedisPrefixKey.ListPrefixKey + key, value.ToRedisValueArray()));
        }
    }
}
