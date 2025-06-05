using System.Text.Json;
using BookTrackerApi;
using BookTrackerApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IBookRepo, PlugForDatabase>(); // ChatGPT offered. 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/smalllibrary",  () => new BookService(new PlugForDatabase()).GetAllBooks())
    .WithName("Library")
    .WithOpenApi();

//https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-9.0&tabs=visual-studio
app.MapPost("/smalllibraryAdd", (Book book) =>
    {
        BookService service = new BookService(new PlugForDatabase()); 
        service.AddBook(book);
        return Results.Ok(book);
    }
    ).WithName("AddBookToLibrary").WithOpenApi();
app.MapGet(
"/smalllibrary/{id}", (int id) =>
    
        new BookService(new PlugForDatabase()).GetBookById(id)).WithName("GetBookById").WithOpenApi();

app.Run();

