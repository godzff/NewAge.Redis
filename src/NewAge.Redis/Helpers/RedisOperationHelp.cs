using Microsoft.Extensions.Options;
using NewAge.Redis.Interfaces;
using NewAge.Redis.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewAge.Redis.Helpers
{
    public partial class RedisOperationHelp
    {
        private readonly IRedisBase redisBase;
        /// <summary>
        /// 实例化连接
        /// </summary>
        public RedisOperationHelp(IRedisBase _redisBase)
        {
            redisBase = _redisBase;
        }
    }
}
