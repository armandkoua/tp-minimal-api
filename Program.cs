using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using tp_minimal_api;

var builder = WebApplication.CreateBuilder();

// services
builder.Services.AddSingleton<TodoService>();
var app =  builder.Build();


//   Endpoints

app.MapGet("todos", (TodoService todoService) =>
{
    var todos = todoService.GetAll();

    return Results.Ok(todos);

    //return Results.NotFound();
});


app.MapGet("todos/active", (TodoService todoService) =>
{
    var todos = todoService.GetActives();
    if (todos.Any()) return Results.Ok(todos);

    return Results.NotFound();

});


app.MapGet("todos/{id:int}", (int id, TodoService todoService) =>
{
    var todo = todoService.GetById(id);

    if(todo is not null)
    {
        return Results.Ok(todo);
    }

    return Results.NotFound();
});



app.MapPost("todos/", ([FromBody] string title, TodoService todoService) =>
{
    var created = todoService.Create(title);
    return Results.Ok(created);
});


app.MapPut("todos/{id:int}", (int id, [FromBody] Todo todoToUpdate, TodoService todoService) =>
{
    var toUpdate = todoService.GetById(id);
    if(toUpdate is null)
    {
        return Results.NotFound();
    }

    var todo = new Todo(id, todoToUpdate.title, todoToUpdate.startDate, todoToUpdate.endDate);
    todoService.Update(todo);
    return Results.Accepted();
});



app.MapDelete("todos/{id:int}", (int id, TodoService todoService) =>
{
    var toUpdate = todoService.GetById(id);
    if (id <= 0)
    {
        return Results.NotFound();
    }    
    todoService.Delete(id);
    return Results.Accepted();
});






app.Run();