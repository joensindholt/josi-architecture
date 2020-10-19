using System.Reflection;
using JosiArchitecture.Core.Shared;
using JosiArchitecture.Core.Shared.Cqs;
using JosiArchitecture.Data;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace JosiArchitecture.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen();

            // TODO: Use automatic discovery

            var connection = @"Data Source=data.db;Cache=Shared";
            services.AddDbContext<DataStore>(options => options.UseSqlite(connection));

            services.AddMediatR(GetApiAssembly(), GetCoreAssembly(), GetDataAssembly());
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));

            services.AddScoped<IQueryDataStore, DataStore>();
            services.AddScoped<DataStore, DataStore>();
            services.AddScoped<IUnitOfWork, DataStore>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private Assembly GetApiAssembly() => typeof(Startup).Assembly;

        private Assembly GetCoreAssembly() => typeof(IQueryDataStore).Assembly;

        private Assembly GetDataAssembly() => typeof(DataStore).Assembly;
    }
}