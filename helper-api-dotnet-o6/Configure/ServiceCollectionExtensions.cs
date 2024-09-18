using helper_api_dotnet_o6_investimento.Domain.Interfaces;
using helper_api_dotnet_o6_investimento.Repositories;
using helper_api_dotnet_o6_investimento.Services;

namespace helper_api_dotnet_o6.Configure
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddScoped<IInvestimentoService, InvestimentoService>();
            services.AddScoped<IBancoCentralApiRepository, BancoCentralApiRepository>();
            services.AddScoped<IGerenciamentoCacheRepository, GerenciamentoCacheRepository>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowInvestimento",
                  builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });
        }
    }
}
