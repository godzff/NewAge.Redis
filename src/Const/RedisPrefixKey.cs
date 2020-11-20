using System;
using System.Collections.Generic;
using System.Text;

namespace NewAge.Redis.Const
{
    /// <summary>
    /// redis的key的前缀
    /// </summary>
    public class RedisPrefixKey
    {
        /// <summary>
        /// string类型的前缀 (默认string:)
        /// </summary>
        public const string StringPrefixKey  = "str:";
        /// <summary>
        /// List 类型的前缀  (默认list:)
        /// </summary>
        public const string ListPrefixKey = "lst:";
        /// <summary>
        /// Set类型的前缀  (set:)
        /// </summary>
        public const string SetPrefixKey = "uset:";
        /// <summary>
        /// Hash 类型的前缀  (默认hash:)
        /// </summary>
        public const string HashPrefixKey = "hash:";
        /// <summary>
        /// 有序集合的前缀  (默认sortedset:)
        /// </summary>
        public const string SortedSetPrefixKey = "sset:";
        /// <summary>
        /// 集合(事务)的前缀
        /// </summary>
        public const string StorePrefixKey = "urn:";
        /// <summary>
        /// 分布式锁的前缀
        /// </summary>
        public const string LockPrefixKey = "lock:";
    }
}
