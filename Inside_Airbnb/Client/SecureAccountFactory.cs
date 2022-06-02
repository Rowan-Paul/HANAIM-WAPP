using System.Security.Claims;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;

namespace Inside_Airbnb.Client;

public class SecureAccountFactory : AccountClaimsPrincipalFactory<SecureUserAccount>
{
    public SecureAccountFactory(IAccessTokenProviderAccessor accessor)
        : base(accessor)
    {
    }

    public override async ValueTask<ClaimsPrincipal> CreateUserAsync(SecureUserAccount account,
        RemoteAuthenticationUserOptions options)
    {
        var initialUser = await base.CreateUserAsync(account, options);
        if (initialUser.Identity is {IsAuthenticated: true})
        {
            var userIdentity = (ClaimsIdentity) initialUser.Identity;
            foreach (var role in account.Roles) userIdentity.AddClaim(new Claim("appRole", role));
        }

        return initialUser;
    }
}