using System;
using System.Collections.Generic;
using StackExchange.Redis;
using System.IO;
using System.Runtime.Serialization;

namespace CachingSolutionsSamples
{
    class RedisCache<T> : ICache<T>
        where T : class
    {
        private readonly ConnectionMultiplexer _redisConnection;
        private readonly string _prefix = "Cache_" + typeof(T);

        readonly DataContractSerializer _serializer = new DataContractSerializer(
            typeof(IEnumerable<T>));

        public RedisCache(string hostName)
        {
            _redisConnection = ConnectionMultiplexer.Connect(hostName);
            
        }

        public IEnumerable<T> Get(string forUser)
        {
            var db = _redisConnection.GetDatabase();
            byte[] s = db.StringGet(_prefix + forUser);
            if (s == null)
                return null;

            return (IEnumerable<T>) _serializer
                .ReadObject(new MemoryStream(s));

        }

        public void Set(string forUser, IEnumerable<T> categories)
        {
            var db = _redisConnection.GetDatabase();
            var key = _prefix + forUser;

            if (categories == null)
            {
                db.StringSet(key, RedisValue.Null);
            }
            else
            {
                var stream = new MemoryStream();
                _serializer.WriteObject(stream, categories);
                db.StringSet(key, stream.ToArray(), TimeSpan.FromSeconds(1));
            }
        }
    }
}
