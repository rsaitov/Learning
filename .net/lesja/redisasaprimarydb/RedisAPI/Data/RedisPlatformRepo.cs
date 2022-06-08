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

        public RedisPlatformRepo(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }
        public void CreatePlatform(Platform platform)
        {
            if (platform == null)
                throw new ArgumentNullException(nameof(platform));

            var db = _redis.GetDatabase(0);

            var serialPlatform = JsonSerializer.Serialize(platform);

            db.StringSet(platform.Id, serialPlatform);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            throw new NotImplementedException();
        }

        public Platform? GetPlatformById(string id)
        {
            var db = _redis.GetDatabase(0);

            var serializedPlatform = db.StringGet(id);

            if (!string.IsNullOrEmpty(serializedPlatform))
            {
                return JsonSerializer.Deserialize<Platform>(serializedPlatform);
            }

            return null;
        }
    }
}