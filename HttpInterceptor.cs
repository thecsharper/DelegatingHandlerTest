using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace DelegatingHandlerTest
{
    public class HttpInterceptor(IOptions<TestOptions> testOptions) : DelegatingHandler
    {
        // From: https://timdeschryver.dev/blog/intercepting-http-requests-with-a-delegatinghandler
        // Example code - non functional
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var bearerToken = GetOptions(testOptions);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "token");

            var response = await base.SendAsync(request, cancellationToken);
            return response;
        }

        private bool GetOptions(IOptions<TestOptions> testOptions)
        {
            return testOptions.Value.Enabled;
        }
    }

    public sealed class TestOptions
    {
        public bool Enabled { get; set; }
    }
}
