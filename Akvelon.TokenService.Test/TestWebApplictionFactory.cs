using Akvelon.TokenService.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using Akvelon.TokenService.Web;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Akvelon.TokenService.Test
{
    public class TestWebApplicationFactory<TStartup> : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkSqlServer()
                    .BuildServiceProvider();

                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<TokenDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<TokenDbContext>(options =>
                {
                    options.UseSqlServer("Server=localhost\\SQLEXPRESS01;Database=AkvelonTest;Trusted_Connection=True")
                        .UseInternalServiceProvider(serviceProvider);
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var appDataBase = scopedServices.GetRequiredService<TokenDbContext>();

                    appDataBase.Database.EnsureCreated();
                }
            });
        }
    }
}