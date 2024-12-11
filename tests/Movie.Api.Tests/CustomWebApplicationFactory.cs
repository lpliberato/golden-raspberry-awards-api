using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace Movie.Api.Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseSetting("https_port", "5001");
            builder.UseSetting("http_port", "5000");


            builder.ConfigureKestrel(options =>
            {
                options.ListenLocalhost(5000);
                options.ListenLocalhost(5001, listenOptions => listenOptions.UseHttps());
            });

            builder.ConfigureAppConfiguration(config =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
            });

            builder.ConfigureServices(services =>
            {
                // Configurar serviços específicos para os testes, se necessário
            });
        }
    }

}
