using System.Text.Json;
using Exceptions;
using FluentValidation;

namespace Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        object response;

        switch (exception)
        {
            case BaseException ex:
                context.Response.StatusCode = ex.StatusCode;

                response = new
                {
                    success = false,
                    message = ex.Message
                };
                break;

            case ValidationException ex:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                response = new
                {
                    success = false,
                    message = "Validation failed.",
                    errors = ex.Errors.Select(x => new
                    {
                        field = x.PropertyName,
                        error = x.ErrorMessage
                    })
                };
                break;

            default:
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                response = new
                {
                    success = false,
                    message = "Internal server error."
                };
                break;
        }

        var json = JsonSerializer.Serialize(response);

        await context.Response.WriteAsync(json);
    }
}