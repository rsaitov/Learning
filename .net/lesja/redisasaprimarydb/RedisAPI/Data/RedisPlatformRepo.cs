using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using RedisAPI.Models;
using StackExchange.Redis;

namespace RedisAPI.Data
{
    public class RedisPlatformRepo : IPlatformRepo
    {
        private readonly IConnectionMultiplexer _redis;
        private const string _hashPlatformKey = "hashplatform";

        public RedisPlatformRepo(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }
        public void CreatePlatform(Platform platform)
        {
            if (platform == null)
                throw new ArgumentNullException(nameof(platform));

            var db = _redis.GetDatabase();

            var serialPlatform = JsonSerializer.Serialize(platform);

            // using strings
            // db.StringSet(platform.Id, serialPlatform);

            // using sets
            // db.SetAdd("PlatformsSet", serialPlatform);

            db.HashSet(_hashPlatformKey, new HashEntry[] {
                new HashEntry(platform.Id, serialPlatform)
            });
        }

        public IEnumerable<Platform?>? GetAllPlatforms()
        {
            var db = _redis.GetDatabase();

            // using sets
            //var completeSet = db.SetMembers("PlatformsSet");

            var completeHash = db.HashGetAll(_hashPlatformKey);

            if (completeHash.Length > 0)
            {
                var obj = Array.ConvertAll(completeHash,
                    val => JsonSerializer.Deserialize<Platform>(val.Value)).ToList();

                return obj;
            }

            return null;
        }

        public Platform? GetPlatformById(string id)
        {
            var db = _redis.GetDatabase();

            // using strings
            // var serializedPlatform = db.StringGet(id);

            var serializedPlatform = db.HashGet(_hashPlatformKey, id);

            if (!string.IsNullOrEmpty(serializedPlatform))
            {
                return JsonSerializer.Deserialize<Platform>(serializedPlatform);
            }

            return null;
        }
    }
}