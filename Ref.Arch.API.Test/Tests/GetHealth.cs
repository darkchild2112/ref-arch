using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ref.Arch.API.Test.Tests;

public class GetHealth : IntegrationTest
{
    [Fact]
    public async Task Should_Return_Success_Status()
    {
        var response = await base.Client.GetAsync("/health");

        Assert.True(response.StatusCode == HttpStatusCode.OK);
    }
}
