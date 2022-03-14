using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TodoApi.Domain.Exceptions;

namespace TodoApi.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;


        public ApiExceptionFilterAttribute()
        {
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(NotFoundException), HandleNotFoundException },
                { typeof(DbUpdateConcurrencyException), HandleDbUpdateConcurrencyException },
            };
        }

        /// <inheritdoc />
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.Exception is OperationCanceledException)
            {
                return Task.CompletedTask;
            }

            var loggerFactory = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory?.CreateLogger<ApiExceptionFilterAttribute>();

            var exception = context.Exception;
            var exceptionType = exception.GetType();

            if (_exceptionHandlers.ContainsKey(exceptionType))
            {
                logger.LogWarning(exception, exception.Message);
                _exceptionHandlers[exceptionType].Invoke(context);
            }
            else
            { 
                logger.LogError(exception, exception.Message);
                HandleUnknownException(context);
            }

            return Task.CompletedTask;
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            var details = new
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred while processing your request.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                TraceId = Activity.Current?.Id ?? context.HttpContext.TraceIdentifier
            };

            context.ExceptionHandled = true;
            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }

        private void HandleNotFoundException(ExceptionContext context)
        {
            if (context.Exception is not NotFoundException exception)
            {
                throw new ArgumentException("invalid type of exception");
            }

            var response = new
            {
                Status = StatusCodes.Status404NotFound,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "The specified resource was not found.",
                Detail = exception.Message,
                TraceId = Activity.Current?.Id ?? context.HttpContext.TraceIdentifier
            };

            context.ExceptionHandled = true;
            context.Result = new NotFoundObjectResult(response);
        }

        private void HandleDbUpdateConcurrencyException(ExceptionContext context)
        {
            if (context.Exception is not DbUpdateConcurrencyException exception) 
            {
                throw new ArgumentException("invalid type of exception");
            }

            var response = new
            {
                Status = StatusCodes.Status409Conflict,
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8",
                Title = "The specified resource was not updated because if concurrency conflict occured.",
                Detail = exception.Message,
                TraceId = Activity.Current?.Id ?? context.HttpContext.TraceIdentifier
            };

            context.ExceptionHandled = true;
            context.Result = new ConflictObjectResult(response);
        }
    }
}
