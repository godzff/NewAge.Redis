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
        /// 新增
        /// </summary>
        /// <param name="value"></param>
        Task<bool> SetAddAsync<T>(string value);
        /// <summary>
        /// 移除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        Task<bool> SetRemoveAsync<T>(string value);
        /// <summary>
        /// 取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        Task<string[]> SetGetAsync<T>();
        /// <summary>
        /// 取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        Task<string[]> SetGetAsync(string key);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        Task<bool> SetAddAsync(string key, string value);
        /// <summary>
        /// 移除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        Task<bool> SetRemoveAsync(string key, string value);
    }
}
