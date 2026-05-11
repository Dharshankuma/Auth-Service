using Authservice.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AuthService.Infrastructure.Middleware
{
    public class ExceptionHandelMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandelMiddleware> _logger;

        public ExceptionHandelMiddleware
            (RequestDelegate next, ILogger<ExceptionHandelMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                _logger.LogInformation("Request started  | Method : {Method} | Path {Path} | QueryString : {QueryString} | Timestamp : {Timestamp}",
                    context.Request.Method,
                    context.Request.Path,
                    context.Request.QueryString,
                    DateTime.UtcNow);

                await _next(context);

                _logger.LogInformation("Request Ended  | Method : {Method} | Path {Path} | QueryString : {QueryString} | Timestamp : {Timestamp}",
                    context.Request.Method,
                    context.Request.Path,
                    context.Request.QueryString,
                    DateTime.UtcNow);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An exception occurred while processing the request. | Method : {Method} | Path {Path} |  Timestamp : {Timestamp}",
                    context.Request.Method,
                    context.Request.Path,
                    DateTime.UtcNow);

                var exceptionDetails = GetExceptionDetails(ex);

                var problems = new ProblemDetails   //this class represent the machine readable error responses for HTTP APIs 
                {
                    Status = exceptionDetails.Status,
                    Type = exceptionDetails.Type,
                    Detail = exceptionDetails.Detail,
                    Title = exceptionDetails.Title,
                };

                if(exceptionDetails.errors is not null)

                context.Response.ContentType = "application/json";
                var json = JsonSerializer.Serialize(problems);
                await context.Response.WriteAsync(json);
            }
        }


        private static ExceptionDetails GetExceptionDetails(Exception ex)
        {
            return ex switch
            {
                ValidationException validationException => new ExceptionDetails(Status: StatusCodes.Status400BadRequest,
                "ValidationError",
                "Validation error",
                "One or more validation errors occured.",
                validationException.Errors),

                IncorrectPasswordException incorrectPasswordException => new ExceptionDetails(Status: StatusCodes.Status400BadRequest,
                Type: nameof(IncorrectPasswordException), Detail: incorrectPasswordException.Message,
                Title: "Incorrect Password", errors: incorrectPasswordException.Er),

                LoginAlreadyTakenException loginAlreadyTakenException => new ExceptionDetails(Status: StatusCodes.Status400BadRequest,
                Type: (nameof(LoginAlreadyTakenException)), Detail: loginAlreadyTakenException.Message,
                Title: "Login Already Taken", errors: null),

                NotFoundException notFoundException => new ExceptionDetails(Status: StatusCodes.Status404NotFound,
                Type: nameof(NotFoundException), Detail: notFoundException.Message,
                Title: "Resource Not Found", errors: null),

                UserAccountNotFoundOrGivenPasswordIsIncorrect userAccountNotFoundOrGivenPasswordIsIncorrect => new ExceptionDetails(Status: StatusCodes.Status400BadRequest,
                Type: nameof(UserAccountNotFoundOrGivenPasswordIsIncorrect), Detail: userAccountNotFoundOrGivenPasswordIsIncorrect.Message,
                Title: "User Account Not Found Or Given Password Is Incorrect", errors: null),

            };
        }



        private record ExceptionDetails(
            int Status,
            string Type, 
            string Detail,
            string Title,
            IEnumerable<object>? errors);
    }
}
