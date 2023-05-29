using System.Text.Json;
using StackExchange.Redis;

namespace RedisAPI;

public class RedisPlatformService : IPlatformService
{
    private readonly IDatabase _redis;
     //private readonly IConnectionMultiplexer _redis;

     public RedisPlatformService(IConnectionMultiplexer redis)
     {

        _redis = redis.GetDatabase();

     }

    public void CreatePlatform(Platform plat)
    {
       
       if(plat ==  null )
            throw new ArgumentOutOfRangeException(nameof(plat));
        
        _redis.HashSet("HashPlatform",new HashEntry[]{
            new HashEntry(plat.Id,JsonSerializer.Serialize(plat))
        });
    }

    public IEnumerable<Platform?>? GetAllPlatform()
    {
        
      var completehashPlatForm=  _redis.HashGetAll("HashPlatform");
      if(completehashPlatForm.Length > 0 )
         return Array.ConvertAll(completehashPlatForm, val => JsonSerializer.Deserialize<Platform>(val.Value)).ToList();
      
      return default;
        
    }

    public Platform? GetPlatformById(string id)
    {
        
        var plat = _redis.HashGet("HashPlatform",id);

        if(!string.IsNullOrEmpty(plat))
            return JsonSerializer.Deserialize<Platform>(plat);
        return null;



    }

    // public void CreatePlatform(Platform plat)
    // {
    //     if(plat == null)
    //         throw new ArgumentOutOfRangeException(nameof(plat));

    //     _redis.StringSet(plat.Id, JsonSerializer.Serialize(plat));
    //     _redis.SetAdd("setPlatform",JsonSerializer.Serialize(plat));

    // }

    // public IEnumerable<Platform?>? GetAllPlatform()
    // {

    //    var completeSetPlatForm  = _redis.SetMembers("setPlatform");

    //    if(completeSetPlatForm.Length > 0)
    //     {
    //         var obj =  Array.ConvertAll(completeSetPlatForm, val => JsonSerializer.Deserialize<Platform>(val)).ToList();
    //         return obj;
    //     }
    //     return default;

    // }

    // public Platform? GetPlatformById(string id)
    // {

    //    var plat= _redis.StringGet(id);

    //     if(!string.IsNullOrEmpty(plat))
    //         return JsonSerializer.Deserialize<Platform>(plat);

    //     return default;
    // }










}