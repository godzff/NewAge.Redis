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
        /// string 操作的数据
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> StringProvideData() {
            yield return new object[] { "test1", 11111 };
            yield return new object[] { "test2", 22222 };
            yield return new object[] { "test3", 33333 };
        }
    }
}
