using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helper_api_dotnet_o6_investimento.Domain.Interfaces
{
    public interface IGerenciamentoCacheRepository
    {
        void Salvar(string key, object value, TimeSpan absoluteExpirationRelativeToNow);
        T Obter<T>(string key);
        void Remover(string key);
    }
}
