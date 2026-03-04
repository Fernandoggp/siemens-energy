using System.Net;

namespace Project.Domain.Common
{
    public class Result
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public object Data { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }

        protected Result(bool success, HttpStatusCode statusCode, string message = null, object data = null)
        {
            Success = success;
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }

        public static Result Ok(object data = null, HttpStatusCode statusCode = HttpStatusCode.OK)
            => new Result(true, statusCode, null, data);

        public static Result Fail(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
            => new Result(false, statusCode, message);
    }
}