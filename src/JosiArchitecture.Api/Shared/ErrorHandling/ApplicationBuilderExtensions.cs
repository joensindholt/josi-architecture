using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;

namespace JosiArchitecture.Api.Shared.ErrorHandling
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseApplicationErrorHandling(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                try
                {
                    await next(context);
                }
                catch (ValidationException ex)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    await context.Response.WriteAsJsonAsync(ex.Errors);
                }
                catch (ArgumentException ex)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    await context.Response.WriteAsJsonAsync(ex.Message);
                }
            });
        }
    }
}