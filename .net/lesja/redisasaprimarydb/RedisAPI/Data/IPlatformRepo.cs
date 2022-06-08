using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RedisAPI.Models;

namespace RedisAPI.Data
{
    public interface IPlatformRepo
    {
        void CreatePlatform(Platform platform);
        Platform? GetPlatformById(string id);
        IEnumerable<Platform> GetAllPlatforms();
    }
}