using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using SizeFintech.Domain.Interfaces.Notification;

namespace SizeFintech.API.Filters
{
    public class DomainNotificationFilter : IAsyncResultFilter
    {
        private readonly IDomainNotification _domainNotification;

        public DomainNotificationFilter(IDomainNotification domainNotification)
        {
            _domainNotification = domainNotification;
        }

        public async Task OnResultExecutionAsync(
            ResultExecutingContext context,
            ResultExecutionDelegate next)
        {
            var hasModelStateErrors = !context.ModelState.IsValid;
            var hasDomainNotifications = _domainNotification.HasNotifications;

            if (hasModelStateErrors || hasDomainNotifications)
            {
                string validations;

                if (hasModelStateErrors)
                {
                    // Retorna { campo: mensagem }
                    validations = JsonConvert.SerializeObject(
                        context.ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .ToDictionary(
                            kv => kv.Key,
                            kv => kv.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                        )
                    );
                }
                else
                {
                    validations = JsonConvert.SerializeObject(
                        _domainNotification.Notifications
                            .ToDictionary(k => k.Key, v => v.Value)
                    );
                }

                var problemDetails = new ProblemDetails
                {
                    Title = "Bad Request",
                    Status = StatusCodes.Status400BadRequest,
                    Instance = context.HttpContext.Request.Path.Value,
                    Detail = validations
                };

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.HttpContext.Response.ContentType = "application/problem+json";

                var json = System.Text.Json.JsonSerializer.Serialize(problemDetails);

                await context.HttpContext.Response.WriteAsync(json);

                return;
            }

            await next();
        }
    }
}
