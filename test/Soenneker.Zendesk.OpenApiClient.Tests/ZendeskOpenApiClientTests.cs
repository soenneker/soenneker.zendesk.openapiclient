using Soenneker.Tests.HostedUnit;

namespace Soenneker.Zendesk.OpenApiClient.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class ZendeskOpenApiClientTests : HostedUnitTest
{
    public ZendeskOpenApiClientTests(Host host) : base(host)
    {
    }

    [Test]
    public void Default()
    {

    }
}
