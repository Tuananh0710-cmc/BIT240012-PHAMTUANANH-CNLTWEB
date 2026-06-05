using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace BookManagement.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Log trước khi xử lý request
            var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var method = context.Request.Method;
            var path = context.Request.Path.ToString();

            Console.WriteLine(
                $"[{time}] Method: {method} - Path: {path}");

            // Chặn URL không hợp lệ
            if (path == "/Book/Detail/0" ||
                path == "/Book/Detail/-1")
            {
                context.Response.StatusCode = 400;

                await context.Response.WriteAsync(
                    "Book id khong hop le");

                return;
            }

            // Chuyển request cho middleware tiếp theo
            await _next(context);

            // Log sau khi xử lý request
            Console.WriteLine(
                $"Status Code: {context.Response.StatusCode}");
        }
    }
}