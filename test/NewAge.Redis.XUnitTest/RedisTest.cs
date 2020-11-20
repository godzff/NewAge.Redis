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
    public class RedisTest
    {
        IServiceCollection services = new ServiceCollection();
        IRedisOperation redis;
        public RedisTest()
        {
            services.AddRedisRepository(options =>
            {
                options.Connection = new string[] { "127.0.0.1:6379" };
            });
            redis = services.BuildServiceProvider().GetService<IRedisOperation>();
        }
        [Fact]
        public async Task Test1()
        {
            var strings = await redis.StringSetAsync("test","1111111");
            strings.ShouldBe(true);
        }
    }
}
