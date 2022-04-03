using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PlannerApp
{
    public class JwtAuthemticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _storage;

        public JwtAuthemticationStateProvider(ILocalStorageService storage)
        {
            _storage = storage;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (await _storage.ContainKeyAsync("access_token"))
            {
                //User logged in
                var tokenAsSting = await _storage.GetItemAsStringAsync("access_token");
                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.ReadJwtToken(tokenAsSting);

                var identity = new ClaimsIdentity(token.Claims, "Bearer");
                var user = new ClaimsPrincipal(identity);
                var authState = new AuthenticationState(user);

                NotifyAuthenticationStateChanged(Task.FromResult(authState));

                return authState;
            }

            return new AuthenticationState(new ClaimsPrincipal()); //Empty claims => user not logged in
        }
    }
}