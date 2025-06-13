using BookHolder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IBookRepo, SqlDatabase>(); // ChatGPT offered. 
builder.Services.AddSingleton<BookService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var bookService = app.Services.GetService<BookService>(); //research after (dependency injection) 

app.MapGet("/smalllibrary", () => bookService?.GetAllBooks())
	.WithName("Library")
	.WithOpenApi();

//https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-9.0&tabs=visual-studio
app.MapPost("/smalllibraryAdd", async (BookInput bookInput) =>
	{
		try
		{
			var author = await bookService.GetAuthor(bookInput.Author);
			var genres = await bookService.GetGenres(bookInput.Genre);
			Book book = new Book(
				0,
				bookInput.Title,
				author, genres,
				bookInput.Description, 
				bookInput.CoverPath,
				bookInput.Status, 
				bookInput.Notes,
				bookInput.Review,
				bookInput.AmountOfPages,
				bookInput.DownloadLink,
				bookInput.Serie,
				bookInput.ReadingStatus, 
				bookInput.PublishingStatus
				);
			bookService.AddBook(book, bookInput);
			return Results.Ok(book);
		}
		catch (Exception e)
		{
			return Results.StatusCode(501);
		}
	}
).WithName("AddBookToLibrary").WithOpenApi();


app.MapGet(
	"/smalllibrary/{id}", (int id) =>
		bookService?.GetBookById(id)).WithName("GetBookById").WithOpenApi();

app.MapGet("/smalllibraryFilter", (HttpRequest request) =>
{
	Dictionary<String, String> filters = new Dictionary<string, string>();
	foreach (var key in request.Query.Keys)
	{
		filters.Add(key, request.Query[key]);
	}

	return bookService.GetFilteredBooks(filters);
}).WithName("QueryStrings").WithOpenApi();

app.MapGet(
	"/smalllibrary/deleteBook/{id}", (int id) =>
	{
		bookService?.DeleteBook(id);
		return Results.Ok("Book was successfully deleted"); 
	}).WithName("DeleteBookById").WithOpenApi();


app.MapPost("/smalllibrary/update/{id}", async (BookInput bookInput, int id) =>
	{
		try
		{
			bookService.UpdateBook(bookInput, id);
			return Results.Ok($"Book №{id} was successfully updated");
		}
		catch (Exception e)
		{
			return Results.StatusCode(501);
		}
	}
).WithName("UpdateBook").WithOpenApi();

app.Run();