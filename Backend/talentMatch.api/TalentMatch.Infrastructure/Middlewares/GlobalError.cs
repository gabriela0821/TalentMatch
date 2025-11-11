using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Text;
using TalentMatch.Core.Enums;
using TalentMatch.Core.Wrappers;
using TalentMatch.Infrastructure.Exceptions;
using ValidationException = TalentMatch.Infrastructure.Exceptions.ValidationException;

namespace TalentMatch.Infrastructure.Middlewares
{
    public class GlobalError
    {
        private string _requestBody;

        private readonly DateTime _beginTime;

        private readonly Stopwatch _sw;

        private readonly RequestDelegate _next;

        public GlobalError(RequestDelegate next)
        {
            _requestBody = string.Empty;
            _beginTime = DateTime.Now;
            _sw = Stopwatch.StartNew();
            _next = next ?? throw new ArgumentNullException("next");
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                _requestBody = await GetDataRequest(context);
                await _next(context).ConfigureAwait(continueOnCapturedContext: false);
            }
            catch (Exception error)
            {
                HttpResponse response = context.Response;
                Response<string> responseModel = new Response<string>
                {
                    Succeeded = false,
                    Message = error?.Message
                };
                await TypeOfErrorAsync(error, response, responseModel);
                await HandleExceptionAsync(_requestBody, context, error, response, responseModel, _sw, _beginTime);
            }
        }

        private static async Task HandleExceptionAsync(string _requestBody, HttpContext context, Exception ex, HttpResponse response, Response<string> responseModel, Stopwatch _sw, DateTime _beginTime)
        {
            string text = Activity.Current?.Id ?? context.TraceIdentifier;
            responseModel.CodeError = "Code error: " + text;
            responseModel.Message = "Error: " + responseModel.Message;
            string result = JsonConvert.SerializeObject(responseModel, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            if (response.StatusCode != 404 && response.StatusCode != 500)
            {
                response.StatusCode = 500;
            }

            await response.WriteAsync(result).ConfigureAwait(continueOnCapturedContext: false);
        }

        private static async Task TypeOfErrorAsync(Exception error, HttpResponse response, Response<string> responseModel)
        {
            response.ContentType = "application/json";
            response.StatusCode = (int)GetErrorCode(error.GetType());
            if (!(error is InfrastructureException))
            {
                if (!(error is InfrastructureException))
                {
                    if (!(error is ValidationException ex))
                    {
                        if (error is KeyNotFoundException)
                        {
                            response.StatusCode = 404;
                        }
                    }
                    else
                    {
                        response.StatusCode = 404;
                        responseModel.Errors = ex.Errors;
                    }
                }
                else
                {
                    response.StatusCode = 404;
                }
            }
            else
            {
                response.StatusCode = 404;
            }

            await Task.FromResult(response);
        }

        private static HttpStatusCode GetErrorCode(Type exceptionType)
        {
            if (Enum.TryParse<TypeExceptions>(exceptionType.Name, out var result))
            {
                return result switch
                {
                    TypeExceptions.NullReferenceException => HttpStatusCode.LengthRequired,
                    TypeExceptions.FileNotFoundException => HttpStatusCode.NotFound,
                    TypeExceptions.OverflowException => HttpStatusCode.RequestedRangeNotSatisfiable,
                    TypeExceptions.OutOfMemoryException => HttpStatusCode.ExpectationFailed,
                    TypeExceptions.InvalidCastException => HttpStatusCode.PreconditionFailed,
                    TypeExceptions.ObjectDisposedException => HttpStatusCode.Gone,
                    TypeExceptions.UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                    TypeExceptions.NotImplementedException => HttpStatusCode.NotImplemented,
                    TypeExceptions.NotSupportedException => HttpStatusCode.NotAcceptable,
                    TypeExceptions.InvalidOperationException => HttpStatusCode.MethodNotAllowed,
                    TypeExceptions.TimeoutException => HttpStatusCode.RequestTimeout,
                    TypeExceptions.ArgumentException => HttpStatusCode.BadRequest,
                    TypeExceptions.StackOverflowException => HttpStatusCode.RequestedRangeNotSatisfiable,
                    TypeExceptions.FormatException => HttpStatusCode.UnsupportedMediaType,
                    TypeExceptions.IOException => HttpStatusCode.NotFound,
                    TypeExceptions.IndexOutOfRangeException => HttpStatusCode.ExpectationFailed,
                    _ => HttpStatusCode.InternalServerError,
                };
            }

            return HttpStatusCode.InternalServerError;
        }

        private static async Task<string> GetDataRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            request.EnableBuffering();
            byte[] buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer);
            string @string = Encoding.UTF8.GetString(buffer);
            request.Body.Position = 0L;
            return string.IsNullOrEmpty(@string) ? string.Empty : @string;
        }
    }
}