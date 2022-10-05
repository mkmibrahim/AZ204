using CityWeather.Data;
using CityWeather.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CityWeather
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private readonly string _policy = "CorsPolicy";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddCors(options =>
            {
                options.AddPolicy(name: _policy,
                                    policy =>
                                    {
                                        //policy.WithOrigins("http://localhost");
                                        //policy.WithOrigins("https://localhost:8080/", "http://localhost:8080/")
                                        policy.AllowAnyOrigin()
                                                .AllowAnyHeader()
                                                .AllowAnyMethod();
                                    });
            });
            services.Configure<OpenWeatherConfigurationClass>
                (this.Configuration.GetSection("OpenWeather"));
            services.AddScoped<IWeatherInfoRetriever,WeatherInfoRetriever>();

            services.Configure<DatabaseConfigurationClass>
               (this.Configuration.GetSection("ConnectionStrings"));
            services.AddDbContext<AuthDbContext>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(c =>
                {
                    c.SerializeAsV2 = true;
                });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<AuthDbContext>();
                //context.Database.EnsureCreated();
                context.Database.Migrate();
            }
        }
    }
}
