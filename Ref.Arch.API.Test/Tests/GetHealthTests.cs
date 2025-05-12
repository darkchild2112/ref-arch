using System.Net;

namespace Ref.Arch.Api.Test.Tests;

public class GetHealthTests : IntegrationTest
{
    [Fact]
    public async Task Should_Return_Success_Status()
    {
        var response = await base.Client.GetAsync("/health");

        Assert.True(response.StatusCode == HttpStatusCode.OK);
    }
}
