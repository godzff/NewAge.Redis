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
        /// 订阅消息
        /// </summary>
        /// <param name="chanel">订阅的名称</param>
        /// <param name="handler">需要处理的事件</param>
        public async Task SubscribeAsync(RedisChannel chanel, Action<RedisChannel, RedisValue> handler, CommandFlags flags = CommandFlags.None)
        {
            var subscriber = _redisBase.RedisConnection.GetSubscriber();
            await subscriber.SubscribeAsync(chanel, handler, flags);
        }
        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="channel">被订阅的name</param>
        /// <param name="message">需要传递的参数</param>
        public async Task<long> PublishAsync(RedisChannel channel, RedisValue message, CommandFlags flags = CommandFlags.None)
        {
            var subscriber = _redisBase.RedisConnection.GetSubscriber();
            return await subscriber.PublishAsync(channel, message, flags);
        }
        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="chanel">订阅的名称</param>
        /// <param name="handler">需要处理的事件</param>
        public async Task UnsubscribeAsync(RedisChannel chanel, Action<RedisChannel, RedisValue> handler = null, CommandFlags flags = CommandFlags.None)
        {
            var subscriber = _redisBase.RedisConnection.GetSubscriber();
            await subscriber.UnsubscribeAsync(chanel, handler, flags);
        }
        /// <summary>
        /// 取消所有的订阅
        /// </summary>
        public async Task UnsubscribeAllAsync(CommandFlags flags = CommandFlags.None)
        {
            var subscriber = _redisBase.RedisConnection.GetSubscriber();
            await subscriber.UnsubscribeAllAsync(flags);
        }
    }
}
