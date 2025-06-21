using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using eCommerceWebAPI.Interface;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace eCommerceWebAPI.Handlers
{
    // BasicAuthenticationHandler class: Handles Basic Authentication for incoming requests.
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IAdminRepository _adminRepo;

        // Constructor: Initializes the authentication handler with required dependencies.
        public BasicAuthenticationHandler(IAdminRepository adminRepo, 
            IOptionsMonitor<AuthenticationSchemeOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock) 
            : base(options, logger, encoder, clock)
        {
            _adminRepo = adminRepo;
        }

        // Handles the authentication logic for incoming requests using Basic Authentication.
        // Parses the Authorization header, decodes credentials, and validates them against the admin repository.
        // Returns a success result with claims if valid, otherwise fails authentication.
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            await Task.Yield();
            string? username = null;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader?.Parameter)).Split(':');
                username = credentials?.FirstOrDefault();
                var password = credentials?.LastOrDefault();

                if (!_adminRepo.ValidateCredentials(username, password))
                    throw new ArgumentException("Invalid Credentials");
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail($"Authentication Failed: {ex.Message}");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
