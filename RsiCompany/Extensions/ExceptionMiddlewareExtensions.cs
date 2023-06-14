using Contracts;
using Entities.ErrorModel;
using Entities.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace RsiCompany.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        // Configures an exception handler for the WebApplication using the provided logger
        public static void ConfigureExceptionHandler(this WebApplication app, ILoggerManager logger)
        {
            app.UseExceptionHandler(appError =>
            {
                // Handles the exception asynchronously when an exception occurs in the application
                appError.Run(async context =>
                {
                    // Sets the response status code to 500 (Internal Server Error)
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    // Sets the response content type to "application/json"
                    context.Response.ContentType = "application/json";

                    // Retrieves the exception information from the context features
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            NotFoundException => StatusCodes.Status404NotFound,_ => StatusCodes.Status500InternalServerError
                        };
                        // Logs the error message using the provided logger
                        logger.LogError($"Something went wrong: {contextFeature.Error}");

                        // Writes an error response in JSON format to the response body
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message
                        }.ToString());
                    }
                });
            });
        }

    }
}
