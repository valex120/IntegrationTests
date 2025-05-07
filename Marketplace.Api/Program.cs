using Marketplace.DataAccess;
using Marketplace.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<ProductsService>()
                            .AddScoped<IProductsRepository, ProductsRepository>()
                            .AddDbContext<MarketplaceContext>(optionsBuilder => optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("MarketPlace")), ServiceLifetime.Scoped)
                            .AddSingleton(TimeProvider.System)
                            .AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
