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
        /// SortedSet 新增
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        Task<bool> SortedSetAddAsync<T>(string key, T value, double score);
        /// <summary>
        /// 获取SortedSet的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> SortedSetGetAsync<T>(string key, double score);
        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<long> SortedSetLengthAsync(string key);
        /// <summary>
        /// 移除SortedSet
        /// </summary>
        Task<bool> SortedSetRemoveAsync<T>(string key, T value);
    }
}
