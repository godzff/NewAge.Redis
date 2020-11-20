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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Values"></param>
        /// <returns></returns>
        //[Fact]
        [Theory]
        [MemberData(nameof(StringProvideData))]
        public async Task Should_Set_String_True(string Key,string Values)
        {
            var strings = await redis.StringSetAsync(Key, Values);
            strings.ShouldBe(true);
        }
    }
}
