using Deviot.Common;
using Microsoft.AspNetCore.Mvc;
using Project.Domain.Exceptions;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.Json;

namespace Project.Api.Bases
{
    [ApiController]
    [ExcludeFromCodeCoverage]
    public abstract class CustomControllerBase : ControllerBase
    {
        protected readonly INotifier _notifier;
        protected readonly ILogger _logger;

        private const string CONTENT_TYPE = "application/json";
        private const string OK_MESSAGE = "A requisição foi executada com sucesso";
        private const string INTERNAL_SERVER_ERROR_MESSAGE = "A requisição não foi executada com sucesso, erro não identificado";

        protected CustomControllerBase(INotifier notifier,
                                       ILogger logger)
        {
            _notifier = notifier;
            _logger = logger;
        }

        private static string Serialize(CustomActionResult customActionResult)
        {
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            return JsonSerializer.Serialize(customActionResult, options);
        }

        private static ContentResult GenerateContentResult(HttpStatusCode httpStatusCode,
                                                           IEnumerable<string> messages,
                                                           object data,
                                                           object meta)
        {
            var success = httpStatusCode == HttpStatusCode.OK ? true : false;
            var json = Serialize(new CustomActionResult(success, messages, data, meta));

            return new ContentResult
            {
                StatusCode = (int)httpStatusCode,
                Content = json,
                ContentType = CONTENT_TYPE
            };
        }

        protected ActionResult CustomResponse(object value = null, object meta = null, HttpStatusCode httpStatusCode = HttpStatusCode.OK, List<string> messages = null, bool success = true)
        {
            messages ??= new List<string>();

            if (_notifier.HasNotifications)
            {
                var notifies = _notifier.GetNotifications();

                if (notifies.Any(x => x.Type == HttpStatusCode.Unauthorized))
                {
                    httpStatusCode = HttpStatusCode.Unauthorized;
                    messages.Add(notifies.First(x => x.Type == HttpStatusCode.Unauthorized).Message);
                }
                else if (notifies.Any(x => x.Type == HttpStatusCode.InternalServerError))
                {
                    httpStatusCode = HttpStatusCode.InternalServerError;
                    messages.Add(notifies.First(x => x.Type == HttpStatusCode.InternalServerError).Message);
                }
                else if (notifies.Any(x => x.Type == HttpStatusCode.NotFound))
                {
                    httpStatusCode = HttpStatusCode.NotFound;
                    messages.Add(notifies.First(x => x.Type == HttpStatusCode.NotFound).Message);
                }
                else if (notifies.Any(x => x.Type == HttpStatusCode.Forbidden))
                {
                    httpStatusCode = HttpStatusCode.Forbidden;
                    messages.Add(notifies.First(x => x.Type == HttpStatusCode.Forbidden).Message);
                }
                else if (notifies.Any(x => x.Type == HttpStatusCode.BadRequest))
                {
                    httpStatusCode = HttpStatusCode.BadRequest;
                    foreach (var notify in notifies.Where(x => x.Type == HttpStatusCode.BadRequest))
                        messages.Add(notify.Message);
                }
                else if (notifies.Any(x => x.Type == HttpStatusCode.NoContent))
                {
                    httpStatusCode = HttpStatusCode.NoContent;
                    messages.Add(notifies.First(x => x.Type == HttpStatusCode.NoContent).Message);
                }
                else if (notifies.Any(x => x.Type == HttpStatusCode.Created))
                {
                    httpStatusCode = HttpStatusCode.Created;
                    messages.Add(notifies.First(x => x.Type == HttpStatusCode.Created).Message);
                }
                else if (notifies.Any(x => x.Type == HttpStatusCode.OK))
                {
                    httpStatusCode = HttpStatusCode.OK;
                    messages.Add(notifies.First(x => x.Type == HttpStatusCode.OK).Message);
                }
                else
                {
                    httpStatusCode = HttpStatusCode.InternalServerError;
                    messages.Add(INTERNAL_SERVER_ERROR_MESSAGE);
                }
                _notifier.Clear();
            }
            else if (success)
            {
                messages.Add(OK_MESSAGE);
            }

            var response = new
            {
                Success = success,
                Messages = messages,
                Data = value,
                Meta = meta
            };

            return new ObjectResult(response) { StatusCode = (int)httpStatusCode };
        }

        protected ActionResult ReturnActionResultForValidationError(ObjectValidationException exception)
        {
            return GenerateContentResult(HttpStatusCode.BadRequest, new List<string>(1) { exception.Message }, null, null);
        }

        protected ActionResult ReturnActionResultForGenericError(Exception exception)
        {
            var errors = Utils.GetAllExceptionMessages(exception);
            foreach (var error in errors)
                _logger.LogError(error);

            return GenerateContentResult(HttpStatusCode.InternalServerError, new List<string>(1) { INTERNAL_SERVER_ERROR_MESSAGE }, null, null);
        }
    }

    public class CustomActionResult
    {
        public IEnumerable<string> Messages
        {
            get;
            private set;
        }

        public object Data
        {
            get;
            private set;
        }
        public object Meta
        {
            get;
            private set;
        }
        public object Success
        {
            get;
            private set;
        }

        public CustomActionResult()
        {
        }

        public CustomActionResult(bool success, string message, object data)
        {
            Success = success;
            Messages = new List<string>(1)
            {
                message
            };
            Data = data;
        }

        public CustomActionResult(bool success, IEnumerable<string> message, object data)
        {
            Success = success;
            Messages = message;
            Data = data;
        }

        public CustomActionResult(bool success, IEnumerable<string> message, object data, object meta)
        {
            Success = success;
            Messages = message;
            Data = data;
            Meta = meta;
        }
    }
}
