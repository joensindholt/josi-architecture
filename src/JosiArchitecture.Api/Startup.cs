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
            services.AddMvc();

            // TODO: Use automatic discovery

            var connection = @"Server=(localdb)\mssqllocaldb;Database=JosiArchitecture;Trusted_Connection=True;";
            services.AddDbContext<DataStore>(options => options.UseSqlServer(connection));

            services.AddMediatR(GetApiAssembly(), GetCoreAssembly(), GetDataAssembly());
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));

            services.AddScoped<IQueryDataStore, DataStore>();
            services.AddScoped<DataStore, DataStore>();
            services.AddScoped<IUnitOfWork, DataStore>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

        private Assembly GetApiAssembly() => typeof(Startup).Assembly;

        private Assembly GetCoreAssembly() => typeof(IQueryDataStore).Assembly;

        private Assembly GetDataAssembly() => typeof(DataStore).Assembly;
    }
}