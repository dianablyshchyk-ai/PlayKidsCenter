using Application.PlayRooms.Services.Abstract;
using Application.PlayRooms.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ConfigureApplicationServices
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IPlayRoomService, PlayRoomService>();
    }
}