using JosiArchitecture.Jobs.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
// builder.Services.AddHostedService<Worker>();

builder.Services.AddSingleton<IRecipeService, RecipeService>();
builder.AddHttpContext();

IHost host = builder.Build();
host.Run();
