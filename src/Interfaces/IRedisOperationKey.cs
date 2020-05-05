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
        /// 移除key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="eKeyOperator"></param>
        Task<bool> KeyRemoveAsync(string key, EKeyOperator eKeyOperator = default);
        /// <summary>
        /// 移除key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="eKeyOperator"></param>
        Task<long> KeyRemoveAsync(List<string> key, EKeyOperator eKeyOperator = default);
        /// <summary>
        /// 判断key是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <param name="eKeyOperator"></param>
        Task<bool> KeyExistsAsync(string key, EKeyOperator eKeyOperator = default);
        /// <summary>
        /// 设置Key过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <param name="eKeyOperator"></param>
        /// <returns></returns>
        Task<bool> KeyExpireAsync(string key, TimeSpan? expiry = default, EKeyOperator eKeyOperator = default);
    }
}
