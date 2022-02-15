using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace MinhaCarteira.Cliente.Recursos.Refit
{
    public class AuthorizationMessageHandler : DelegatingHandler
    {
        private readonly IAuthTokenStore authTokenStore;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await Task.FromResult("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImhlbXBtYXgiLCJyb2xlIjoiQWRtaW4iLCJuYmYiOjE2NDQ5NTA1NjMsImV4cCI6MTY0NDk1NDE2MywiaWF0IjoxNjQ0OTUwNTYzfQ.jEogrlEFNVKslcUsrheXtkIGVs_mUK-e8EIyN0ox_DA");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
