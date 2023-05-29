namespace RedisAPI;

public interface IPlatformService
{

   void CreatePlatform<T>(T obj);

  T GetPlatformById<T>(string id );

  IEnumerable<T> GetAllPlatform<T>();



}