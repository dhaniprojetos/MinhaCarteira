using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MinhaCarteira.Cliente.Recursos.Refit.Handler
{
    public class AuthorizationMessageHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContext;

        public AuthorizationMessageHandler(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!_httpContext.HttpContext.Request.Cookies.ContainsKey("Bearer"))
                return await base.SendAsync(request, cancellationToken);

            request.Headers.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                _httpContext.HttpContext.Request.Cookies["Bearer"]);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
