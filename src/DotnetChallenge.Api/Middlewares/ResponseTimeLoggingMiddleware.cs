using System.Diagnostics;

namespace DotnetChallenge.Api.Middlewares
{
    public class ResponseTimeLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseTimeLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            await _next(context);

            stopwatch.Stop();
            LogResponseTime(context, stopwatch.ElapsedMilliseconds);
        }

        private void LogResponseTime(HttpContext context, long elapsedMilliseconds)
        {
            string logsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");

            if (!Directory.Exists(logsDirectory))
            {
                Directory.CreateDirectory(logsDirectory);
            }

            string logFileName = $"{DateTime.Now:yyyyMMdd}_response_time.log";
            string logFilePath = Path.Combine(logsDirectory, logFileName);

            string logMessage = $"{DateTime.Now} - {context.Request.Method} {context.Request.Path} - Tiempo de respuesta: {elapsedMilliseconds} ms";
            File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
        }
    }
}
