using RedisAPI.Models;
using System.Text.Json;
using StackExchange.Redis;

namespace RedisAPI.Data
{
    public class RedisPlatformRepository : IPlatformRepository
    {
        private readonly IConnectionMultiplexer _redis;
        public RedisPlatformRepository(IConnectionMultiplexer redis)
        {
            _redis = redis;   
        }
        public void CreatePlatform(Platform plat)
        {
            if(plat == null)
            {
                throw new ArgumentOutOfRangeException(nameof(plat));
            }

            var db = _redis.GetDatabase();
            var serialPlat = JsonSerializer.Serialize(plat);
            
            // db.StringSet(plat.Id, serialPlat);
            // db.SetAdd("PatformSet", serialPlat);

            db.HashSet($"hashplatform", new HashEntry[]{new HashEntry(plat.Id, serialPlat)});
        }

        public IEnumerable<Platform?>? GetAllPlatforms()
        {
            var db = _redis.GetDatabase();
            // var completeSet = db.SetMembers("PlatformSet");
          
            var completeHash = db.HashGetAll("hashplatform");

            if(completeHash.Length > 0)
            {
                var obj = Array.ConvertAll(completeHash, val => JsonSerializer.Deserialize<Platform>(val.Value)).ToList();
                return obj;
            }

            return null;
        }

        public Platform? GetPlatformById(string id)
        {
            var db = _redis.GetDatabase();
            // var plat = db.StringGet(id);
            var plat = db.HashGet("hashplatform", id);

            if(!string.IsNullOrEmpty(plat))
            {
                return JsonSerializer.Deserialize<Platform>(plat);
            }

            return null;
        }
    }
}