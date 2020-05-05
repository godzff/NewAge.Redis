﻿using NewAge.Redis.Enums;
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
        /// 订阅消息
        /// </summary>
        /// <param name="chanel">订阅的名称</param>
        /// <param name="handler">需要处理的事件</param>
        Task SubscribeAsync(RedisChannel chanel, Action<RedisChannel, RedisValue> handler, CommandFlags flags = CommandFlags.None);
        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="channel">被订阅的name</param>
        /// <param name="message">需要传递的参数</param>
        Task<long> PublishAsync(RedisChannel channel, RedisValue message, CommandFlags flags = CommandFlags.None);
        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="chanel">订阅的名称</param>
        /// <param name="handler">需要处理的事件</param>
        Task UnsubscribeAsync(RedisChannel chanel, Action<RedisChannel, RedisValue> handler = null, CommandFlags flags = CommandFlags.None);
        /// <summary>
        /// 取消所有的订阅
        /// </summary>
        Task UnsubscribeAllAsync(CommandFlags flags = CommandFlags.None);
    }
}
