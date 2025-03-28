#nullable enable
using System.Security.Claims;
using System.Threading.Tasks;
using BTCPayServer.Abstractions.Constants;
using BTCPayServer.Client;
using BTCPayServer.Security;
using BTCPayServer.Security.Greenfield;
using BTCPayServer.Services;
using Microsoft.AspNetCore.Authorization;

namespace BTCPayServer
{
    public record WalletCreationPermissions(bool CanCreateHotWallet, bool CanCreateColdWallet, bool CanRPCImport);
    public static class AuthorizationExtensions
    {
        public static async Task<bool> CanModifyStore(this IAuthorizationService authorizationService, ClaimsPrincipal user)
        {
            return (await authorizationService.AuthorizeAsync(user, null,
                                new PolicyRequirement(Policies.CanModifyStoreSettings))).Succeeded;
        }
        public static async Task<WalletCreationPermissions> CanUseHotWallet(
            this IAuthorizationService authorizationService,
            PoliciesSettings? policiesSettings,
            ClaimsPrincipal user)
        {
            if (user.Identity?.IsAuthenticated is not true)
                return new(false, false, false);
            var claimUser = user.Identity as ClaimsIdentity;
            if (claimUser is null)
                return new(false, false, false);

            bool isAdmin = false;
            if (claimUser.AuthenticationType == AuthenticationSchemes.Cookie)
                isAdmin = user.IsInRole(Roles.ServerAdmin);
            else if (claimUser.AuthenticationType == GreenfieldConstants.AuthenticationType)
                isAdmin = (await authorizationService.AuthorizeAsync(user, Policies.CanModifyServerSettings)).Succeeded;
            return isAdmin ? new(true, true, true) :
                   new(policiesSettings?.AllowHotWalletForAll is true,
                   policiesSettings?.AllowCreateColdWalletForAll is true,
                   policiesSettings?.AllowHotWalletRPCImportForAll is true);
        }
    }
}
