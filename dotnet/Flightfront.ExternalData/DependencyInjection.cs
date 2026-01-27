using Flightfront.Core.Interfaces;
using Flightfront.ExternalData.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;

namespace Flightfront.ExternalData;

public static class DependencyInjection
{
    public static IServiceCollection AddExternalDataServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddHttpClient<ICheckWxService, CheckWxService>(client =>
        {
            var config = configuration.GetSection("CheckWxApi");

            if (string.IsNullOrEmpty(config["ApiKey"]))
                throw new InvalidOperationException("CheckWx API key is missing.");

            client.BaseAddress = new Uri(config["BaseUrl"]!);
            client.DefaultRequestHeaders.Add("X-API-Key", config["ApiKey"]);
        });

        return services;



    }

}
