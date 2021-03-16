using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using MarketplaceAPP.Data;
using MarketplaceAPP.Model;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace MarketplaceAPP.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime jsRuntime;
        private readonly IMarketplaceService _marketplaceService;

        public Customer cachedUser;

        public CustomAuthenticationStateProvider(IJSRuntime jsRuntime, IMarketplaceService marketplaceService)
        {
            this.jsRuntime = jsRuntime;
            _marketplaceService = marketplaceService;
        }

        public Customer GetCachedUser()
        {
            return cachedUser;    
        }

        
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            if (cachedUser == null)
            {
                string userAsJson = await jsRuntime.InvokeAsync<String>("sessionStorage.getItem", "currentUser");
                if (!string.IsNullOrEmpty(userAsJson))
                {
                    cachedUser = JsonSerializer.Deserialize<Customer>(userAsJson);

                    identity = SetupClaimsForUser(cachedUser);
                }
            }
            else
            {
                identity = SetupClaimsForUser(cachedUser);
            }
            
            ClaimsPrincipal cachedClaimsPrincipal = new ClaimsPrincipal(identity);
            return await Task.FromResult(new AuthenticationState(cachedClaimsPrincipal));
        }

        public  async Task ValidateLogin(string username, string password)
        {
            ClaimsIdentity identity = new ClaimsIdentity();
            try
            {
              Customer user =  await _marketplaceService.LoginUser(new Customer(username, password));
              identity = SetupClaimsForUser(user);
                string serialisedData = JsonSerializer.Serialize(user);
                await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serialisedData);
                cachedUser = user;
            } catch (Exception e) {
                throw e;
            }
            NotifyAuthenticationStateChanged(
                Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity))));
        Console.WriteLine("Reached"); 
        }
        
        private ClaimsIdentity SetupClaimsForUser(Customer user) {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.Username));

            ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth_type");
            return identity;
        }
        
    }
}