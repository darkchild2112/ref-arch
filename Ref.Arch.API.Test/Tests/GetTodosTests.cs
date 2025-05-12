using Microsoft.AspNetCore.Routing;
using System.Net;
using System.Text.Json;

namespace Ref.Arch.Api.Test.Tests;

public class GetTodosTests : IntegrationTest
{
    private static readonly string EndpointAddress = "/api/v1/todos";

    private record Todo(int UserId, int Id, string Title, bool Completed);

    [Fact]
    public async Task Should_Return_Success_Status()
    {
        var response = await Client.GetAsync(EndpointAddress);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Should_Return_An_Array_Of_Todos()
    {
        var response = await Client.GetAsync(EndpointAddress);

        var content = await response.Content.ReadAsStringAsync();

        var todos = JsonSerializer.Deserialize<IEnumerable<Todo>>(content, JsonOptions);

        Assert.NotNull(todos);
        Assert.True(todos.Any());

        foreach(var todo in todos)
        {
            Assert.True(todo.UserId > 0);
            Assert.True(todo.Id > 0);
            Assert.False(string.IsNullOrEmpty(todo.Title));
        }
    }

    [Fact]
    public async Task Should_Return_A_Todo()
    {
        var todoId = 1;
        var response = await Client.GetAsync($"{EndpointAddress}/{todoId}");

        var content = await response.Content.ReadAsStringAsync();

        var todo = JsonSerializer.Deserialize<Todo>(content, JsonOptions);

        Assert.NotNull(todo);
        Assert.True(todo.Id == todoId);
        Assert.True(todo.UserId > 0);
        Assert.False(string.IsNullOrEmpty(todo.Title));
    }

    [Fact]
    public async Task Should_Return_Not_Found_Status_When_Given_An_Invalid_Id()
    {
        var response = await Client.GetAsync($"{EndpointAddress}/0");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
