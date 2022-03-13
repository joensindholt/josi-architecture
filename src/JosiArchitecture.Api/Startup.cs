using System.Reflection;
using JosiArchitecture.Core.Shared;
using JosiArchitecture.Core.Shared.Cqs;
using JosiArchitecture.Core.Shared.Persistence;
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

            services.AddDbContext<DataStore>(options => options.UseInMemoryDatabase("JosiArchitecture"));

            services.AddCqs();

            services.AddScoped<IQueryDataStore, DataStore>();
            services.AddScoped<ICommandDataStore, DataStore>();
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

        
    }
}