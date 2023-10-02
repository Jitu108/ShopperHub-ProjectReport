using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Catalog.API.RedisConfig
{
    public static class RedisHelper
    {
        #region Cache Status Dictionary
        public static Dictionary<string, CacheDataStatus> CacheStatusDisctionary = new();

        public static void HoldCache(this IDistributedCache distributedCache)
        {
            foreach (var key in CacheStatusDisctionary.Keys)
            {
                CacheStatusDisctionary[key] = CacheDataStatus.OnHold;
            }
        }

        public static void ReleaseCache(this IDistributedCache distributedCache)
        {
            foreach (var key in CacheStatusDisctionary.Keys)
            {
                CacheStatusDisctionary[key] = CacheDataStatus.Refreshed;
            }
        }
        public static CacheDataStatus CacheStatus(this IDistributedCache distributedCache, string key)
        {
            if (CacheStatusDisctionary.ContainsKey(key))
            {
                return CacheStatusDisctionary[key];
            }

            return CacheDataStatus.NotExist;
        }
        #endregion

        #region Cache Operations
        public async static Task Add<T>(this IDistributedCache distributedCache, T data, string key)
        {
            var jsonData = JsonSerializer.Serialize(data);
            await distributedCache.SetStringAsync(key, jsonData, GetCacheOption());

            if (CacheStatusDisctionary.ContainsKey(key))
            {
                CacheStatusDisctionary[key] = CacheDataStatus.Ready;
            }
            else
            {
                CacheStatusDisctionary.Add(key, CacheDataStatus.Ready);
            }
        }

        public async static Task<T> Get<T>(this IDistributedCache distributedCache, string key)
        {
            var isReady = CacheStatusDisctionary[key];
            if (isReady == CacheDataStatus.Ready)
            {
                var jsonString = await distributedCache.GetStringAsync(key);

                var data = JsonSerializer.Deserialize<T>(jsonString);
                return data;
            }

            return default(T);
        }
        #endregion

        #region Private Members
        private static DistributedCacheEntryOptions GetCacheOption()
        {
            var distributedCacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = GetExpirationTime(),
                SlidingExpiration = null
            };

            return distributedCacheEntryOptions;
        }

        private static TimeSpan GetExpirationTime()
        {
            // 24 Hours in seconds
            var totalSeconds = DateTime.Now.AddDays(1).Subtract(DateTime.Now).TotalSeconds;
            var timeSpan = TimeSpan.FromSeconds(totalSeconds);
            return timeSpan;
        }
        #endregion

    }

    public enum CacheDataStatus
    {
        OnHold,
        Refreshed,
        NotExist,
        Ready
    }

    public static class RedisKeys
    {
        public const string AllProduct = "AllProduct";
        public const string ProductByBrand = "ProductByBrand_";
        public const string ProductByType = "ProductByType_";
        public const string ProductById = "ProductById_";
    }
}
