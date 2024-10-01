using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    // Constructor: RequestDelegate ve ILogger parametreleri alır
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next; // Sonraki middleware veya endpoint'i temsil eden RequestDelegate
        _logger = logger; // ILogger, hata günlüğü (log) için kullanılır
    }

    // InvokeAsync metodu: HTTP isteğini işleyen metot
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            // Sonraki middleware veya endpoint'i çağırır
            await _next(httpContext);
        }
        catch (Exception e)
        {
            // Hata durumunda HandleExceptionAsync metodu çağrılır
            await HandleExceptionAsync(httpContext, e);
        }
    }

    // HandleExceptionAsync metodu: Hata durumunda çalışacak metot
    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode statusCode;
        string result;

        // Farklı türde hatalar için farklı işlemler yapılır
        switch (exception)
        {
            case FluentValidation.ValidationException validationException:
                // FluentValidation kütüphanesi tarafından oluşturulan hatalar için BadRequest (400) döner
                statusCode = HttpStatusCode.BadRequest;
                result = JsonConvert.SerializeObject(new { error = validationException.Errors });
                break;
            case Autofac.Core.DependencyResolutionException dependencyResolutionException:
                // Autofac DI kütüphanesi tarafından oluşturulan hatalar için InternalServerError (500) döner
                statusCode = HttpStatusCode.InternalServerError;
                result = JsonConvert.SerializeObject(new { error = dependencyResolutionException.Message });
                break;
            default:
                // Diğer tüm hatalar için InternalServerError (500) döner
                statusCode = HttpStatusCode.InternalServerError;
                result = JsonConvert.SerializeObject(new { error = "Beklenmedik bir hata oluştu." });
                break;
        }

        // Hatanın loglanması
        _logger.LogError(exception, exception.Message);

        // HTTP yanıtının hazırlanması
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        // JSON formatında hata mesajının HTTP yanıtına yazılması
        return context.Response.WriteAsync(result);
    }
}
