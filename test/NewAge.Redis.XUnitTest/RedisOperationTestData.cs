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
        /// string ������ɹ�������
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> StringTrueProvideData() {
            yield return new object[] { "t_long", 11111 };
            yield return new object[] { "t_string", "hahaha" };
            yield return new object[] { "t_object", new { id = 1 } };
        }
        /// <summary>
        /// string �����ʧ�ܵ�����
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> StringFalseProvideData()
        {
            yield return new object[] { "f_long", null };
            yield return new object[] { "f_string", null };
            yield return new object[] { "f_object", null };
        }
    }
}
