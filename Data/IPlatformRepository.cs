using RedisAPI.Models;

namespace RedisAPI.Data
{
    public interface IPlatformRepository
    {
        void CreatePlatform(Platform plat);
        Platform? GetPlatformById(string id);
        IEnumerable<Platform?>? GetAllPlatforms();
    }
}