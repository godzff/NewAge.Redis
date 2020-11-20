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
        #region string set
        /// <summary>
        /// 保存string类型的数据
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Values"></param>
        /// <returns></returns>
        [Theory]
        [MemberData(nameof(StringTrueProvideData))]
        public async Task Should_Set_String_True<T>(string Key, T Values)
        {
            var Res = await redis.StringSetAsync(Key, Values);
            Res.ShouldBe(true);
        }
        /// <summary>
        /// 保存string类型的数据
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Values"></param>
        /// <returns></returns>
        [Theory]
        [MemberData(nameof(StringFalseProvideData))]
        public async Task Should_Set_String_False<T>(string Key, T Values)
        {
            bool Res = true;
            try
            {
                Res = await redis.StringSetAsync(Key, Values);
            }
            catch
            {
                Res = false;
            }
            Res.ShouldBe(false);
        }
        #endregion
    }
}
