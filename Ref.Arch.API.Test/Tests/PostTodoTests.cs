using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using WireMock.Matchers;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Ref.Arch.Api.Test.Tests;

public class PostTodoTests : IntegrationTest
{
    private record TodoRequest(int UserId, string Title, bool Completed);

    [Fact]
    public async Task Should_Return_Success_Status()
    {
        var request = new TodoRequest(1, "Test Todo", false);

        JsonPlaceholderApi.Given(
            Request.Create()
            .WithPath("/todos")
            .WithBody(new JsonPartialMatcher(request)))
            .RespondWith(Response.Create()
            .WithStatusCode(201)
            .WithBodyAsJson(new { id = 201 }));

        var response = await Client.PostAsync("api/v1/todos", JsonContent.Create(request));

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task Should_Return_The_New_Todo_When_Successfully_Created()
    {
        var request = new TodoRequest(1, "Test Todo", false);

        JsonPlaceholderApi.Given(
            Request.Create()
            .WithPath("/todos")
            .WithBody(new JsonPartialMatcher(request)))
            .RespondWith(Response.Create()
            .WithStatusCode(201)
            .WithBodyAsJson(new { id = 300 }));

        var response = await Client.PostAsync("api/v1/todos", JsonContent.Create(request));

        var content = await response.Content.ReadAsStringAsync();
        var todoId = JsonSerializer.Deserialize<int>(content, JsonOptions);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(300, todoId);
    }

    [Theory]
    [InlineData(0, "Test Todo", false)] // Invalid UserId
    [InlineData(1, "", false)] // Invalid Title
    [InlineData(1, "kGqvNEtaYdLbRpWchXnfuJmAHiuhioZiyVMlOjTQxgeFBrCPKUsSzIwDkhnvlyqeYjMTAoHRsNCXpFgLUbmKtdZhVJrwcEaqYplxWnOvjUBisKMHTrDzFYcgPEaQLXWndJotvrhmcNZyukgsBAIqCJmbXTvoWnhplYardcFGZeMRUsKjiECqVtNHoqvNEtaYdLbRpWchXnfuJmAHiuhioZiyVMlOjTQxgeFBrChgtfrdfrtgyhujikolpoi", false)] // Invalid Title Length
    public async Task Should_Fail_With_Invalid_Parameters(int userId, string title, bool completed)
    {
        var response = await Client.PostAsync("api/v1/todos", JsonContent.Create(new TodoRequest(userId, title, completed)));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
