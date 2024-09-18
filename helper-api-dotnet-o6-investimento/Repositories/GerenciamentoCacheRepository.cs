using helper_api_dotnet_o6_investimento.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace helper_api_dotnet_o6_investimento.Repositories
{
    public class GerenciamentoCacheRepository : IGerenciamentoCacheRepository
    {
        private readonly IMemoryCache _memoryCache;

        public GerenciamentoCacheRepository(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void Salvar(string key, object value, TimeSpan absoluteExpirationRelativeToNow)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow
            };

            _memoryCache.Set(key, value, cacheEntryOptions);
        }

        public T? Obter<T>(string key)
        {
            _memoryCache.TryGetValue(key, out T value);
            return value;
        }

        public void Remover(string key)
        {
            _memoryCache.Remove(key);
        }
    }
}
