using Inside_Airbnb.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("Inside_Airbnb.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Inside_Airbnb.ServerAPI"));

builder.Services.AddMsalAuthentication<RemoteAuthenticationState, SecureUserAccount>(options =>
    {
        builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
        options.ProviderOptions.DefaultAccessTokenScopes.Add("api://980d04df-777c-4e22-9b94-0c0d32efd515/InsideAirbnb.API");
        options.UserOptions.RoleClaim = "appRole";
    })
    .AddAccountClaimsPrincipalFactory<RemoteAuthenticationState, SecureUserAccount,
        SecureAccountFactory>();

await builder.Build().RunAsync();
