using NewAge.Redis.Enums;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewAge.Redis.Interfaces
{
    /// <summary>
    /// redis的连接配置
    /// </summary>
    public partial interface IRedisOperation : IRedisDependency
    {
        /// <summary>
        /// 保存字符串
        /// </summary>
        Task<bool> StringSetAsync(string key, string value, TimeSpan? expiry = default);
        /// <summary>
        /// 保存对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        Task<bool> StringSetAsync<T>(string key, T value, TimeSpan? expiry = default);
        /// <summary>
        /// 保存集合对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        Task<bool> StringSetAsync<T>(string key, List<T> value, TimeSpan? expiry = default);
        /// <summary>
        /// 获取字符串
        /// </summary>
        Task<RedisValue> StringGetAsync(string key);
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        Task<T> StringGetAsync<T>(string key);
        /// <summary>
        /// 自增
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        Task<long> StringIncrementAsync(string key, long value = 1);
        /// <summary>
        /// 递减
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<long> StringDecrementAsync(string key, long value = 1);
    }
}
