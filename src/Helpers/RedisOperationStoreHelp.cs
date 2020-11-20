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
    //TODO Store集合方法目前还是同步夹杂异步,需要改进,暂时无法使用,只能保证编译不报错
    public partial class RedisOperationHelp
    {
        /// <summary>
        /// 保存一个集合 （事务）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public bool StoreAll<T>(List<T> list)
        {
            if (list != null && list.Count >= 0)
            {
                //获取实体的信息
                var type = typeof(T);
                //获取类名
                var name = type.Name;
                string key = RedisPrefixKey.StorePrefixKey + name.ToLower() + ":";
                //获取id的属性
                System.Reflection.PropertyInfo propertyInfo = type.GetProperty("Id");
                var tran = _redisBase.DoSave(db => db.CreateTransaction());
                foreach (var item in list)
                {
                    //获取id的值
                    var id = propertyInfo.GetValue(item, null);
                    tran.SetAddAsync(RedisPrefixKey.StorePrefixKey + type.Name, id.ToStr());
                    tran.StringSetAsync(key + id.ToStr(), item.ToJson());
                }
                return tran.Execute();
            }
            return false;
        }
        /// <summary>
        /// 保存单个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public bool Store<T>(T info)
        {
            if (info == null)
            {
                return false;
            }
            //获取实体的信息
            var type = typeof(T);
            //获取类名
            var name = type.Name;
            string key = RedisPrefixKey.StorePrefixKey + name.ToLower() + ":";
            //获取id的属性
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty("Id");
            //获取id的值
            var id = propertyInfo.GetValue(info, null);
            //开启事务
            var tran = _redisBase.DoSave(db => db.CreateTransaction());
            tran.SetAddAsync(RedisPrefixKey.StorePrefixKey + type.Name, id.ToStr());
            tran.StringSetAsync(key + id.ToStr(), info.ToJson());
            return tran.Execute();
        }

        /// <summary>
        /// 删除所有的
        /// </summary>
        public async Task<bool> DeleteAllAsync<T>()
        {
            //获取实体的信息
            var type = typeof(T);
            //获取类名
            var name = type.Name;
            string key = RedisPrefixKey.StorePrefixKey + name.ToLower() + ":";

            var tran = _redisBase.DoSave(db => db.CreateTransaction());
            //获取需要删除的id
            var ids =await SetGetAsync<T>();
            await tran.KeyDeleteAsync(RedisPrefixKey.StorePrefixKey + type.Name);
            foreach (var item in ids)
            {
                await tran.KeyDeleteAsync(key + item.ToStr());
            }
            return tran.Execute();
        }
        /// <summary>
        /// 移除 单个的集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        public bool DeleteById<T>(string id)
        {
            //获取实体的信息
            var type = typeof(T);
            //获取类名
            var name = type.Name;
            string key = RedisPrefixKey.StorePrefixKey + name.ToLower() + ":";
            var tran = _redisBase.DoSave(db => db.CreateTransaction());
            tran.SetRemoveAsync(RedisPrefixKey.StorePrefixKey + type.Name, id);
            tran.KeyDeleteAsync(key + id.ToStr());
            return tran.Execute();
        }

        /// <summary>
        /// 移除 多个的集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        public bool DeleteByIds<T>(List<string> ids)
        {
            if (ids != null && ids.Count > 0)
            {
                //获取实体的信息
                var type = typeof(T);
                //获取类名
                var name = type.Name;
                string key = RedisPrefixKey.StorePrefixKey + name.ToLower() + ":";
                var tran = _redisBase.DoSave(db => db.CreateTransaction());
                foreach (var item in ids)
                {
                    tran.SetRemoveAsync(RedisPrefixKey.StorePrefixKey + type.Name, item);
                    tran.KeyDeleteAsync(key + item.ToStr());
                }
                return tran.Execute();
            }
            return false;

        }
        /// <summary>
        /// 获取所有的集合数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<List<T>> GetAllAsync<T>()
        {
            //获取实体的信息
            var type = typeof(T);
            //获取类名
            var name = type.Name;
            string key = RedisPrefixKey.StorePrefixKey + name.ToLower() + ":";

            List<T> li = new List<T>();
            //获取id的集合
            var ids =await SetGetAsync<T>();
            if (ids != null && ids.Length > 0)
            {
                foreach (var item in ids)
                {
                    var res = _redisBase.DoSave(db => db.StringGet(key + item));
                    if (!res.IsNullOrEmpty)
                    {
                        li.Add(res.ToStr().JsonTo<T>());
                    }
                }
            }
            return li;
        }

        /// <summary>
        /// 获取单个的
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById<T>(int id)
        {
            //获取实体的信息
            var type = typeof(T);
            //获取类名
            var name = type.Name;
            string key = RedisPrefixKey.StorePrefixKey + name.ToLower() + ":";
            var res = _redisBase.DoSave(db => db.StringGet(key + id.ToString()));
            if (!res.IsNullOrEmpty)
            {
                return res.ToStr().JsonTo<T>() ;
            }
            return default;
        }

        /// <summary>
        /// 获取多个的
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<T> GetByIds<T>(List<int> ids)
        {
            //获取实体的信息
            var type = typeof(T);
            //获取类名
            var name = type.Name;
            string key = RedisPrefixKey.StorePrefixKey + name.ToLower() + ":";
            List<T> li = new List<T>();
            foreach (var item in ids)
            {
                var res = _redisBase.DoSave(db => db.StringGet(key + item.ToStr()));
                if (!res.IsNullOrEmpty)
                {
                    li.Add(res.ToStr().JsonTo<T>());
                }
            }
            return li;
        }
    }
}
