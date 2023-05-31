using Microsoft.AspNetCore.Http.Json;
using Poker.Domain;
using Poker.HandsApi.JsonConverters;

namespace Poker.HandsApi.Extensions;

public static class JsonExtensions
{

    public static IServiceCollection AddJsonOptions(this IServiceCollection services)
    {
        return services.Configure<JsonOptions>(options => 
        {
            options.SerializerOptions.Converters.Add(new EnumStringConverter<Card>());
        });
    }

}