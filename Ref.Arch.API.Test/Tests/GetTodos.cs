using System.Net;
using System.Text.Json;

namespace Ref.Arch.API.Test.Tests;

public class GetTodos : IntegrationTest
{
    private record Todo(int UserId, int Id, string Title, bool Completed);

    // Move to base class
    private readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    [Fact]
    public async Task Should_Return_Success_Status()
    {
        var response = await Client.GetAsync("/todos");

        Assert.True(response.StatusCode == HttpStatusCode.OK);
    }

    [Fact]
    public async Task Should_Return_An_Array_Of_Todos()
    {
        var response = await Client.GetAsync("/todos");

        var content = await response.Content.ReadAsStringAsync();

        var todos = JsonSerializer.Deserialize<IEnumerable<Todo>>(content, JsonOptions);

        Assert.NotNull(todos);
        Assert.True(todos is IEnumerable<Todo>);
        Assert.True(todos.Any());
    }

    [Fact]
    public async Task Should_Return_A_Todo()
    {
        var todoId = 1;
        var response = await Client.GetAsync($"/todos/{todoId}");

        var content = await response.Content.ReadAsStringAsync();

        var todo = JsonSerializer.Deserialize<Todo>(content, JsonOptions);

        Assert.NotNull(todo);
        Assert.True(todo.Id == todoId);
        Assert.True(todo.UserId > 0);
        Assert.False(string.IsNullOrEmpty(todo.Title));
    }

    [Fact]
    public async Task Should_Return_Not_Found_Status_When_Given_A_Bad_Id()
    {
        var response = await Client.GetAsync($"/todos/badId");

        Assert.True(response.StatusCode == HttpStatusCode.NotFound);
    }
}
