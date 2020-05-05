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
        /// 删除
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="hashField"需要删除的字段</param>
        /// <returns></returns>
        Task<bool> HashDeleteAsync(string key, string hashField);
        /// <summary>
        /// 删除多条
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="hashFields"需要删除的字段</param>
        /// <returns></returns>
        Task<long> HashDeleteAsync(string key, string[] hashFields);
        /// <summary>
        /// 验证是否存在指定列
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        Task<bool> HashExistsAsync(string key, string hashField);
        /// <summary>
        /// 获取指定的列的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        Task<string> HashGetAsync(string key, string hashField);
        /// <summary>
        /// 获取所有的数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<Dictionary<string, string>> HashGetAllAsync(string key);
        /// <summary>
        /// 获取多条数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashFields"></param>
        /// <returns></returns>
        Task<string[]> HashGetAsync(string key, string[] hashFields);
        /// <summary>
        /// 获取hash的长度
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<long> HashLengthAsync(string key);
        /// <summary>
        /// 存储hash值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashFields">存储的数据key-value结构</param>
        /// <returns></returns>
        Task HashSetAsync(string key, HashEntry[] hashFields);
        /// <summary>
        /// 储存单条hash值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField">字段名</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        Task<bool> HashSetAsync(string key, string hashField, string value);
        /// <summary>
        /// 返回所有值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string[]> HashValuesAsync(string key);
    }
}
