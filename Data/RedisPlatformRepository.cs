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
            db.StringSet(plat.Id, serialPlat);
            db.SetAdd("PatformSet", serialPlat);
        }

        public IEnumerable<Platform?>? GetAllPlatforms()
        {
            var db = _redis.GetDatabase();
            var completeSet = db.SetMembers("PlatformSet");
          
            if(completeSet.Length > 0)
            {
                var obj = Array.ConvertAll(completeSet, val =>JsonSerializer.Deserialize<Platform>(val));
                return obj;
            }
        }

        public Platform? GetPlatformById(string id)
        {
            var db = _redis.GetDatabase();
            var plat = db.StringGet(id);

            if(!string.IsNullOrEmpty(plat))
            {
                return JsonSerializer.Deserialize<Platform>(plat);
            }

            return null;
        }
    }
}
