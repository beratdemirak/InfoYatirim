namespace InfoYatirim.Consumer
{
    using Microsoft.Extensions.Caching.Memory;
    using System.Collections.Generic;

    public class CacheService
    {
        private readonly IMemoryCache _cache;
        private readonly string _cacheKey = "Data";

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void AddData(Data Data)
        {
            var dataList = _cache.GetOrCreate(_cacheKey, entry => new List<Data>());
            dataList.Add(Data);
            _cache.Set(_cacheKey, dataList);
        }

        public List<Data> GetData()
        {
            return _cache.GetOrCreate(_cacheKey, entry => new List<Data>());
        }
    }
}
