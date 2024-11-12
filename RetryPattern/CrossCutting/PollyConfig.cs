using Polly;
using Polly.Extensions.Http;

namespace RetryPattern.CrossCutting
{
    public static class PollyConfig
    {
        public static IAsyncPolicy<HttpResponseMessage> WaitAndRetryPolicy()
        {
            var retryWaitPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(new[] {

                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(4),
                    TimeSpan.FromSeconds(6),

                }, (outcome, timespan, retrycount, context) =>
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Tentando pela {retrycount} vez!");
                    Console.ForegroundColor = ConsoleColor.White;
                });

            return retryWaitPolicy;
        }
    }
}
