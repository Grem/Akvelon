using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Akvelon.TokenService.Web.Filters
{
    /// <summary>
    /// Фильтр валидации токена
    /// </summary>
    public class TokenValidationAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var token = context.HttpContext.Request.Path.Value.Replace("/", string.Empty);

            if (!Validate(token))
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.HttpContext.Response.WriteAsync("Invalid token specified!");
                    
                return; // Прерываем обработку запроса, если токен не валидный
            }
            
            await next();
        }
        
        /// <summary>
        /// Проверка корректности данных
        /// </summary>
        /// <param name="token">Токен</param>
        private static bool Validate(string token)
        {
            if (!string.IsNullOrEmpty(token) && token.Length == 6) return true;

            return false;
        }
    }
}