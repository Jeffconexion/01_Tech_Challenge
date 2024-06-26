using LocalFriendzApi.Application.IServices;
using LocalFriendzApi.Application.Services;
using LocalFriendzApi.Core.Configuration;
using LocalFriendzApi.Core.IIntegration;
using LocalFriendzApi.Core.IRepositories;
using LocalFriendzApi.Core.Logging;
using LocalFriendzApi.Infrastructure.Data;
using LocalFriendzApi.Infrastructure.Integration;
using LocalFriendzApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LocalFriendzApi.Commom.Api
{
    public static class BuildExtension
    {
       
        public static void AddDataContexts(this WebApplicationBuilder builder)
        {

            var useInMemoryDatabase = builder.Configuration.GetValue<bool>("UseInMemoryDatabase");

                builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseInMemoryDatabase("DB_FIAP_ARQUITETO"));
         
        }

        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder
                .Services
                .AddScoped<IContactServices, ContactServices>();

            builder
                .Services
                .AddScoped<IContactRepository, ContactRepository>();
            builder
            .Services
                .AddScoped<IInfoDDDIntegration, InfoDDDIntegration>();
        }

        public static void AddDocumentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(x =>
            {
                x.CustomSchemaIds(n => n.FullName);
            });
        }

        public static void AddCrossOrigin(this WebApplicationBuilder builder)
        {
            // inserir implementação do cross
        }

        public static void AddLogging(this WebApplicationBuilder builder)
        {
            builder.Logging.ClearProviders();
            builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
            {
                LogLevel = LogLevel.Information
            }));
        }
    }
}