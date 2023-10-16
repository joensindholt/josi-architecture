using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
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
                    await context.Response.WriteAsJsonAsync(BuildProblemDetailsFromValidationException(ex));
                }
                catch (ArgumentException ex)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    await context.Response.WriteAsJsonAsync(ex.Message);
                }
            });
        }

        private static ProblemDetails BuildProblemDetailsFromValidationException(ValidationException ex)
        {
            var problemDetails = new ProblemDetails
            {
                Type = "bad-request",
                Title = "Your request parameters didn't validate",
            };

            problemDetails.Extensions.Add(
                "invalid-params",
                ex.Errors.Select(e => new { Name = e.PropertyName, Reason = e.ErrorMessage }));

            return problemDetails;
        }
    }
}
