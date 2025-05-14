using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Ref.Arch.Api.Test.Tests;

public class DeleteTodoTests : IntegrationTest
{
    [Fact]
    public async Task Should_Return_Success_Status()
    {
        var todoId = 1;

        JsonPlaceholderApi.Given(
            Request.Create()
            .WithPath($"/todos/{todoId}"))
            .RespondWith(Response.Create().WithStatusCode(200));

        var response = await Client.DeleteAsync($"api/v1/todos/{todoId}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Should_Return_Successful_Status_When_Given_An_Invalid_Id()
    {
        var todoId = 0;

        var response = await Client.DeleteAsync($"api/v1/todos/{todoId}");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
