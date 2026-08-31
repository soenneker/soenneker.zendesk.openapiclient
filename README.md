[![](https://img.shields.io/nuget/v/soenneker.zendesk.openapiclient.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.zendesk.openapiclient/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.zendesk.openapiclient/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.zendesk.openapiclient/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.zendesk.openapiclient.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.zendesk.openapiclient/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.zendesk.openapiclient/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.zendesk.openapiclient/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Zendesk.OpenApiClient

A Kiota-generated .NET client for Zendesk, with typed request builders and models for the account API.

## Installation

```shell
dotnet add package Soenneker.Zendesk.OpenApiClient
```

## Usage

The generated client requires a Kiota request adapter and your account origin. This example lists tickets with Zendesk API-token authentication:

```csharp
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Zendesk.OpenApiClient;
using System.Net.Http.Headers;
using System.Text;

string credential = Convert.ToBase64String(
    Encoding.UTF8.GetBytes($"{emailAddress}/token:{apiToken}"));

using var httpClient = new HttpClient();
httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Basic", credential);

var adapter = new HttpClientRequestAdapter(
    new AnonymousAuthenticationProvider(),
    httpClient: httpClient)
{
    BaseUrl = "https://acme.zendesk.com"
};

var client = new ZendeskOpenApiClient(adapter);

var response = await client.Api.V2.Tickets.GetAsync(request =>
{
    request.QueryParameters.PerPage = 25;
}, cancellationToken);

foreach (var ticket in response?.Tickets ?? [])
    Console.WriteLine($"{ticket.Id}: {ticket.Subject}");
```

For OAuth, set `Authorization` to `Bearer {access_token}` instead. The adapter `BaseUrl` must be the account origin, such as `https://acme.zendesk.com`; request builders already include paths such as `/api/v2/tickets`.

The generated surface follows the URL hierarchy, so `/api/v2/tickets` is exposed as `client.Api.V2.Tickets`.
