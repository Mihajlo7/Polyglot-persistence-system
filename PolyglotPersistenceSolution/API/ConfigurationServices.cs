using API.Services;
using SQLDataAccess;
using SQLDataAccess.impl;

namespace API
{
    public static class ConfigurationServices
    {
        public static IServiceCollection ConfigureHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            var baseUrl = configuration["GeneratorAPI:Url"];
            var headerKey = configuration["GeneratorAPI:HeaderKey"];
            var headerValue = configuration["GeneratorAPI:HeaderValue"];

            services.AddHttpClient("GeneratorAPI", config =>
            {
                config.BaseAddress = new Uri(baseUrl);
                config.DefaultRequestHeaders.Add(headerKey, headerValue);

                config.Timeout=new TimeSpan(0,5,0);
            });

            services.AddScoped<IHttpClientFactoryService, HttpClientFactoryService>();

            return services;
        }

        public static IServiceCollection ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped<IConsumerRepository, ConsumerRepository>();

            return services;
        }
    }
}
