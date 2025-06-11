using System.Reflection;
using Dapper;
using Npgsql;

namespace BookHolder;

//https://www.npgsql.org/doc/basic-usage.html
//https://www.code4it.dev/blog/postgres-crud-dapper/
public class SqlDatabase : IBookRepo

{
	private const string DATABASE_NAME = "myfirstlibrary";
	private const string TABLE_NAME = "books";

	private const string CONNECTION_STRING =
		$"Server=localhost;Port=5432;User Id=postgres;Password=Lol12345;Database={DATABASE_NAME};";

	NpgsqlConnection _connection = new NpgsqlConnection(
		connectionString: CONNECTION_STRING);

	public SqlDatabase()
	{
	}
	
	//en solution

	public async Task AddBook(Book book) //Task = same as void. 
	{
		_connection = new NpgsqlConnection(
			connectionString: CONNECTION_STRING);
		_connection.Open();
		string commandText =
			$"INSERT INTO books (title, author, genre, description,\"coverPath\", status,notes, review, \"amountOfPages\",\"downloadLink\",serie,\"readingStatus\",\"publishingStatus\") " +
			$"VALUES (@title, @author, @genre, @description,@coverPath,@status,@notes,@review,@amountOfPages,@downloadLink,@serie,@readingStatus,@publishingStatus);";
		var queryArgs = new
		{ title = book.Title,
		  //set up Genre and Author. 
		  author = 1,
		  genre = new[]
		  { 1 },
		  description = book.Description,
		  coverPath = book.CoverPath,
		  status = book.Status,
		  notes = book.Notes,
		  review = book.Review,
		  amountOfPages = book.AmountOfPages,
		  downloadLink = book.DownloadLink,
		  serie = book.Serie,
		  readingStatus = book.ReadingStatus,
		  publishingStatus = book.PublishingStatus };
		await _connection.ExecuteAsync(commandText, queryArgs);
		await _connection.CloseAsync();
	}

	public async Task<List<Book>> GetAllBooks()
	{
		_connection = new NpgsqlConnection(
			connectionString: CONNECTION_STRING);
		_connection.Open();
		var task = await _connection.QueryAsync<Book>(
			$"SELECT * FROM {TABLE_NAME} ");
		await _connection.CloseAsync();
		return task.ToList();
	}

	public async Task<Book> GetBookById(int id)
	{
		_connection = new NpgsqlConnection(
			connectionString: CONNECTION_STRING);
		_connection.Open();
		string commandText = $"SELECT * FROM {TABLE_NAME} WHERE ID = @id";
		var queryArgs = new
		{ Id = id };
		var task = await _connection.QueryFirstAsync<Book>(commandText, queryArgs);
		await _connection.CloseAsync();
		return task;
	}

	public async Task<List<Book>> GetFilteredBooks(Dictionary<string, string>? query)
	{
		_connection = new NpgsqlConnection(
			connectionString: CONNECTION_STRING);
		_connection.Open();
		string commandText = CreateCommandString(query);
		var task = await _connection.QueryAsync<Book>(
			commandText);
		await _connection.CloseAsync();
		return task.ToList();
	}

	public async void UpdateBook(Book book)
	{
		throw new NotImplementedException();
	}

	public async void DeleteBook(int id)
	{
		_connection = new NpgsqlConnection(
			connectionString: CONNECTION_STRING);
		_connection.Open();
		string commandText = $"DELETE FROM {TABLE_NAME} WHERE ID = @id";
		var queryArgs = new
		{ Id = id };
		await _connection.ExecuteAsync(commandText, queryArgs);
		await _connection.CloseAsync();
	}

	private string CreateCommandString(Dictionary<string, string>? query)
	{
		string commandText = $"SELECT * FROM {TABLE_NAME} "; 
		int iterator = 0;
		foreach (var (key, value) in query)
		{
			Console.WriteLine(key + " " + value);
			if (key.ToLower() == "orderby") continue;
			commandText += iterator == 0 ? $"WHERE \"{key}\"={GetFixedValue(key, value)} " :
				$" AND \"{key}\"={GetFixedValue(key, value)} " ;
			iterator++;
		}
		if (query.TryGetValue("orderBy", out var orderBy))
		{
			commandText = commandText + "ORDER BY " + orderBy;
		}

		Console.WriteLine(commandText);
		return commandText;
	}

	private string GetFixedValue(string key, string value)
	{
		return (key is "amountOfPages" or "id") ? value : $"'{value}'";
	}
}