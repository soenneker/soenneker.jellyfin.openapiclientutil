using Soenneker.Jellyfin.OpenApiClientUtil.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Jellyfin.OpenApiClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class JellyfinOpenApiClientUtilTests : HostedUnitTest
{
    private readonly IJellyfinOpenApiClientUtil _openapiclientutil;

    public JellyfinOpenApiClientUtilTests(Host host) : base(host)
    {
        _openapiclientutil = Resolve<IJellyfinOpenApiClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
