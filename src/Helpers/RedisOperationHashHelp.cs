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
        /// 删除
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="hashField"需要删除的字段</param>
        /// <returns></returns>
        public async Task<bool> HashDeleteAsync(string key, string hashField)
            => await _redisBase.DoSave(db => db.HashDeleteAsync(RedisPrefixKey.HashPrefixKey + key, hashField));

        /// <summary>
        /// 删除多条
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="hashFields"需要删除的字段</param>
        /// <returns></returns>
        public async Task<long> HashDeleteAsync(string key, string[] hashFields)
        {
            if (hashFields == null || hashFields.Count() <= 0)
                throw new MyException("值不能为空");
            return await _redisBase.DoSave(db => db.HashDeleteAsync(RedisPrefixKey.HashPrefixKey + key, hashFields.ToRedisValueArray()));
        }

        /// <summary>
        /// 验证是否存在指定列
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public async Task<bool> HashExistsAsync(string key, string hashField) =>await _redisBase.DoSave(db => db.HashExistsAsync(RedisPrefixKey.HashPrefixKey + key, hashField));
        /// <summary>
        /// 获取指定的列的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public async Task<string> HashGetAsync(string key, string hashField)
        {
            var res = await _redisBase.DoSave(db => db.HashGetAsync(RedisPrefixKey.HashPrefixKey + key, hashField));
            return !res.IsNull ? res.ToStr() : default;
        }

        /// <summary>
        /// 获取所有的数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, string>> HashGetAllAsync(string key)
        {
            var res = await _redisBase.DoSave(db => db.HashGetAllAsync(RedisPrefixKey.HashPrefixKey + key));
            return res != null ? res.ToStringDictionary() : default;
        }
        /// <summary>
        /// 获取多条数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashFields"></param>
        /// <returns></returns>
        public async Task<string[]> HashGetAsync(string key, string[] hashFields)
        {
            if (hashFields == null || hashFields.Count() <= 0)
                throw new MyException("值不能为空");
            var res = await _redisBase.DoSave(db => db.HashGetAsync(RedisPrefixKey.HashPrefixKey + key, hashFields.ToRedisValueArray()));
            return res != null ? res.ToStringArray() : default;
        }
        /// <summary>
        /// 获取hash的长度
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<long> HashLengthAsync(string key) => _redisBase.DoSave(db => db.HashLengthAsync(RedisPrefixKey.HashPrefixKey + key));

        /// <summary>
        /// 存储hash值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashFields">存储的数据key-value结构</param>
        /// <returns></returns>
        public Task HashSetAsync(string key, HashEntry[] hashFields)
        {
            if (hashFields == null || hashFields.Count() <= 0)
                throw new MyException("值不能为空");
            return _redisBase.DoSave(db => db.HashSetAsync(RedisPrefixKey.HashPrefixKey + key, hashFields));
        }

        /// <summary>
        /// 储存单条hash值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField">字段名</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public Task<bool> HashSetAsync(string key, string hashField, string value)
        {
            return _redisBase.DoSave(db => db.HashSetAsync(RedisPrefixKey.HashPrefixKey + key, hashField, value));
        }

        /// <summary>
        /// 返回所有值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<string[]> HashValuesAsync(string key)
        {
            var res = await _redisBase.DoSave(db => db.HashValuesAsync(RedisPrefixKey.HashPrefixKey + key));
            return res != null ? res.ToStringArray() : default;
        }
    }
}
