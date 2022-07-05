
using Baas.Core.Interfaces;
using Baas.Core.Services;
using Baas.Infrastructure;
using Baas.Infrastructure.Filters;
using Microsoft.OpenApi.Models;

namespace Middelware
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


            services.AddControllers(options => {
                options.Filters.Add<GlobalExceptionFilter>();
            });
            
            services.AddTransient<IVaultServices, VaultServices>();
            services.AddTransient<IIpfsService, IpfsService>();
            services.AddTransient<ICurpService, CurpService>();
            services.AddTransient<IAccountService, AccountService>();
            
            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo {
                    Version = "v1",
                    Title = "Dicio Alliance Middelware",
                    Description = "Middelware Backend, Blockchain & IPFS",
                    TermsOfService = new Uri("https://dicio.com"),
                });
                // var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
                // options.IncludeXmlComments(xmlPath);
            });
        }
                
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder => builder
                                      .AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader()
                        );


            app.UseSwagger();
            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("./swagger/v1/swagger.json", "Dicio Alliance Middelware");
                options.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
