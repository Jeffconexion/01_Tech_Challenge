using LocalFriendzApi.Application.IServices;
using LocalFriendzApi.Application.Services;
using LocalFriendzApi.Core.Configuration;
using LocalFriendzApi.Core.IRepositories;
using LocalFriendzApi.Infrastructure.Data;
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
    }
}