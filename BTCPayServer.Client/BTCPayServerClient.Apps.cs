using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BTCPayServer.Client.Models;

namespace BTCPayServer.Client;

public partial class BTCPayServerClient
{
    public virtual async Task<PointOfSaleAppData> CreatePointOfSaleApp(string storeId,
        PointOfSaleAppRequest request, CancellationToken token = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        return await SendHttpRequest<PointOfSaleAppData>($"api/v1/stores/{storeId}/apps/pos", request, HttpMethod.Post, token);
    }

    public virtual async Task<CrowdfundAppData> CreateCrowdfundApp(string storeId,
        CrowdfundAppRequest request, CancellationToken token = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        return await SendHttpRequest<CrowdfundAppData>($"api/v1/stores/{storeId}/apps/crowdfund", request, HttpMethod.Post, token);
    }

    public virtual async Task<PointOfSaleAppData> UpdatePointOfSaleApp(string appId,
        PointOfSaleAppRequest request, CancellationToken token = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        return await SendHttpRequest<PointOfSaleAppData>($"api/v1/apps/pos/{appId}", request, HttpMethod.Put, token);
    }

    public virtual async Task<AppBaseData> GetApp(string appId, CancellationToken token = default)
    {
        if (appId == null) throw new ArgumentNullException(nameof(appId));
        return await SendHttpRequest<AppBaseData>($"api/v1/apps/{appId}", null, HttpMethod.Get, token);
    }

    public virtual async Task<AppBaseData[]> GetAllApps(string storeId, CancellationToken token = default)
    {
        if (storeId == null) throw new ArgumentNullException(nameof(storeId));
        return await SendHttpRequest<AppBaseData[]>($"api/v1/stores/{storeId}/apps", null, HttpMethod.Get, token);
    }

    public virtual async Task<AppBaseData[]> GetAllApps(CancellationToken token = default)
    {
        return await SendHttpRequest<AppBaseData[]>("api/v1/apps", null, HttpMethod.Get, token);
    }

    public virtual async Task<PointOfSaleAppData> GetPosApp(string appId, CancellationToken token = default)
    {
        if (appId == null) throw new ArgumentNullException(nameof(appId));
        return await SendHttpRequest<PointOfSaleAppData>($"api/v1/apps/pos/{appId}", null, HttpMethod.Get, token);
    }

    public virtual async Task<CrowdfundAppData> GetCrowdfundApp(string appId, CancellationToken token = default)
    {
        if (appId == null) throw new ArgumentNullException(nameof(appId));
        return await SendHttpRequest<CrowdfundAppData>($"api/v1/apps/crowdfund/{appId}", null, HttpMethod.Get, token);
    }

    public virtual async Task DeleteApp(string appId, CancellationToken token = default)
    {
        if (appId == null) throw new ArgumentNullException(nameof(appId));
        await SendHttpRequest($"api/v1/apps/{appId}", null, HttpMethod.Delete, token);
    }
}
