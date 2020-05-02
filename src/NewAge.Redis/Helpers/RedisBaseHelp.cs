using NewAge.Infra.Exceptions;
using NewAge.Infra.Extensions;
using NewAge.Redis.Interfaces;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace NewAge.Redis.Helpers
{
    public class RedisBaseHelp : IRedisBase
    {
        private readonly IRedisConnection redisConnection;
        public RedisBaseHelp(IRedisConnection _redisConnection)
        {
            redisConnection = _redisConnection;
        }
        public IDatabase RedisDataBase
        {
            get
            {
                return redisConnection.RedisConnection.GetDatabase();
            }
        }
        public ConnectionMultiplexer RedisConnection
        {
            get
            {
                return redisConnection.RedisConnection;
            }
        }

        /// <summary>
        /// 执行缓存库保存
        /// </summary>
        /// <typeparam name="TResult">返回结果</typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        public TResult DoSave<TResult>(Func<IDatabase, TResult> action)
        {
            return action(RedisDataBase);
        }
        /// <summary>
        /// 执行缓存库保存 无返回值
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public void DoSave(Action<IDatabase> action)
        {
            action(RedisDataBase);
        }

        public RedisKey[] ConvertRedisKeys(List<string> val)
        {
            return val.Select(k => (RedisKey)k).ToArray();
        }

        /// <summary>
        /// 生成EndPoint
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public EndPoint ParseEndPoints(string host, int port)
        {
            if (IPAddress.TryParse(host, out IPAddress ip)) return new IPEndPoint(ip, port);
            return new DnsEndPoint(host, port);
        }
        /// <summary>
        /// 生成EndPoint
        /// </summary>
        /// <param name="hostAndPort"></param>
        /// <returns></returns>
        public EndPoint ParseEndPoints(string hostAndPort)
        {
            if (hostAndPort.IndexOf(":") != -1)
            {
                var obj = hostAndPort.Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                var host = obj[0];
                var port = obj[1].ToInt();
                return ParseEndPoints(host, port);
            }
            else
            {
                throw new MyException("hostAndPort error");
            }
        }
    }
}
