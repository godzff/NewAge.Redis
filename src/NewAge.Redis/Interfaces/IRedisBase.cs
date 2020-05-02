using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace NewAge.Redis.Interfaces
{
    /// <summary>
    /// 缓存的基类 
    /// </summary>
    public interface IRedisBase : IRedisDependency
    {
        /// <summary>
        /// 访问存储库
        /// </summary>
        IDatabase RedisDataBase { get; }
        /// <summary>
        /// 实例连接
        /// </summary>
        ConnectionMultiplexer RedisConnection { get; }
        /// <summary>
        /// 保存
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        TResult DoSave<TResult>(Func<IDatabase, TResult> action);
        /// <summary>
        /// 保存 无返回值的
        /// </summary>
        /// <param name="action"></param>
        void DoSave(Action<IDatabase> action);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        RedisKey[] ConvertRedisKeys(List<string> val);

        EndPoint ParseEndPoints(string hostAndPort);

    }
}
