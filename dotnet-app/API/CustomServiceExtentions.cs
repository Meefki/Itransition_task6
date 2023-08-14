using Application.Repositories;
using Application.UseCases;
using Infrastructure.Repository;

public static class CustomServiceExtentions
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddTransient<ITagRepository, TagRepository>();
        services.AddTransient<IMessageRepository, MessageRepository>();

        services.AddTransient<IMessageService, MessageService>();

        return services;
    }
}