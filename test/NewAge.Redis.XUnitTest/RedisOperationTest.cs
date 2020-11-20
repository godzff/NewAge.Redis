using Microsoft.Extensions.DependencyInjection;
using NewAge.Redis.Extensions;
using NewAge.Redis.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Shouldly;

namespace NewAge.Redis.XUnitTest
{
    public partial class RedisOperationTest
    {
        private readonly IServiceCollection services = new ServiceCollection();
        private readonly IRedisOperation redis;
        public RedisOperationTest()
        {
            services.AddRedisRepository(options =>
            {
                options.Connection = new string[] { "127.0.0.1:6379" };
            });
            redis = services.BuildServiceProvider().GetService<IRedisOperation>();
        }
    }
}
