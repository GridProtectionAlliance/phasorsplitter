using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace StreamSplitter.WebUI
{
    public class HttpAuthenticator
    {
        #region [ Members ]

        // Nested Types
        public struct AuthenticationResult
        {
            private AuthenticationResult(long userID, string failureReason)
            {
                UserID = userID;
                FailureReason = failureReason;
            }

            public bool IsAuthenticated => !FailureReason.Any();
            public long UserID { get; }
            public string FailureReason { get; }

            internal static AuthenticationResult Succeed(long userID) => new(userID, string.Empty);

            internal static AuthenticationResult Fail(string reason) => new(0L, reason);
        }

        // Constants
        private const string AuthenticationType = "Basic";

        #endregion

        #region [ Constructors ]

        //public HttpAuthenticator(IAuthenticator authenticator) =>
        //    Authenticator = authenticator;

        #endregion

        #region [ Properties ]

        //private IAuthenticator Authenticator { get; }

        #endregion

        #region [ Methods ]

        public async Task<AuthenticationResult> AuthenticateAsync(HttpContext httpContext)
        {
            var authorizationHeader = httpContext.Request.Headers["Authorization"];

            if (authorizationHeader.Count == 0)
                return Fail("No authorization header");

            if (authorizationHeader.Count != 1)
                return Fail("Ambiguous authorization header");

            if (!AuthenticationHeaderValue.TryParse(authorizationHeader[0], out AuthenticationHeaderValue? headerValue))
                return Fail("Invalid format for authorization header");

            string scheme = headerValue.Scheme;
            string? token = headerValue.Parameter;

            if (scheme != AuthenticationType)
                return Fail($"Scheme not supported: {scheme}");

            if (token is null)
                return Fail($"Missing token from authorization header");

            string credentials = string.Empty;

            try
            {
                byte[] credentialData = Convert.FromBase64String(token);
                credentials = Encoding.UTF8.GetString(credentialData);
            }
            catch { /* Invalid credential string */ }

            int separatorIndex = credentials.IndexOf(':');

            if (separatorIndex < 0)
                return Fail("Invalid credential string");

            string username = credentials.Substring(0, separatorIndex);
            string password = credentials.Substring(separatorIndex + 1);
            long? id = null; // await Authenticator.AuthenticateAsync(username, password);

            if (id is null)
                return Fail("Invalid username or password");

            long userID = id.GetValueOrDefault();
            return Succeed(userID);
        }

        public async Task<ClaimsPrincipal> GetUserAsync(long userID)
        {
            //UserData userData = await Authenticator.GetUserDataAsync(userID);
            //string serializedData = JsonSerializer.Serialize(userData);

            //ClaimsIdentity claimsIdentity = new ClaimsIdentity(AuthenticationType);
            //claimsIdentity.AddClaim(new Claim(claimsIdentity.NameClaimType, userData.Username));
            //claimsIdentity.AddClaim(new Claim(ClaimTypes.GivenName, userData.DisplayName));
            //claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, userData.Role.ToString()));
            //claimsIdentity.AddClaim(new Claim(ClaimTypes.UserData, serializedData));

            //return new ClaimsPrincipal(claimsIdentity);
            return null;
        }

        private AuthenticationResult Succeed(long userID) =>
            AuthenticationResult.Succeed(userID);

        private AuthenticationResult Fail(string reason) =>
            AuthenticationResult.Fail(reason);

        #endregion
    }

    public static class AuthenticationExtensions
    {
        //public static UserData GetUserData(this ClaimsPrincipal user)
        //{
        //    Claim? claim = user.FindFirst(ClaimTypes.UserData)
        //        ?? throw new InvalidOperationException("Invalid user data: missing UserData claim");

        //    using JsonDocument document = JsonDocument.Parse(claim.Value);
        //    JsonElement root = document.RootElement;
        //    JsonElement usernameElement = root.GetProperty(nameof(UserData.Username));
        //    JsonElement displayNameElement = root.GetProperty(nameof(UserData.DisplayName));
        //    JsonElement organizationElement = root.GetProperty(nameof(UserData.Organization));
        //    JsonElement roleElement = root.GetProperty(nameof(UserData.Role));

        //    string username = usernameElement.GetString()
        //        ?? throw new InvalidOperationException("Invalid user data: missing username");

        //    string displayName = displayNameElement.GetString()
        //        ?? throw new InvalidOperationException("Invalid user data: missing display name");

        //    string organization = organizationElement.GetString()
        //        ?? throw new InvalidOperationException("Invalid user data: missing organization");

        //    Role role = (Role)roleElement.GetInt32();
        //    return new UserData(username, displayName, organization, role);
        //}
    }
}
