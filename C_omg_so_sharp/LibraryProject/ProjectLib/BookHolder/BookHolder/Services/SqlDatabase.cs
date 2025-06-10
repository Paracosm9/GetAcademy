using Dapper;
using Npgsql;

namespace BookHolder;

//https://www.npgsql.org/doc/basic-usage.html
//https://www.code4it.dev/blog/postgres-crud-dapper/
public class SqlDatabase : IBookRepo


{
	private const string DATABASE_NAME = "myfirstlibrary";
	private const string TABLE_NAME = "books";

	NpgsqlConnection _connection = new NpgsqlConnection(
		connectionString: $"Server=localhost;Port=5432;User Id=postgres;Password=Lol12345;Database={DATABASE_NAME};");

	public SqlDatabase()
	{
		_connection.Open();
	}

	public void AddBook(Book book)
	{
		throw new NotImplementedException();
	}

	public async Task<List<Book>> GetAllBooks()
	{
		var task = await _connection.QueryAsync<Book>(
			$"SELECT * FROM {TABLE_NAME} ");
		await _connection.CloseAsync();
		return task.ToList();
	}

	public async Task<Book> GetBookById(int id)
	{
		
		string commandText = $"SELECT * FROM {TABLE_NAME} WHERE ID = @id";
		var queryArgs = new { Id = id };
		var task = await _connection.QueryFirstAsync<Book>(commandText, queryArgs);
		await _connection.CloseAsync();
		return task;
	}

	public Task<List<Book>> GetFilteredBooks(Dictionary<string, string> query)
	{
		throw new NotImplementedException();
	}

	public void UpdateBook(Book book)
	{
		throw new NotImplementedException();
	}

	public void DeleteBook(int id)
	{
		throw new NotImplementedException();
	}


	// public class Human
	// {
	// 	public string Name { get; set; }
	// 	public DateTime Birthday { get; set; }
	// 	public string LivingPlace { get; set; }
	//
	// 	public Human(string name, DateTime birthday, string livingPlace)
	// 	{
	// 		Name = name;
	// 		Birthday = birthday;
	// 		LivingPlace = livingPlace;
	// 	}
	//
	// 	public Human()
	// 	{
	// 	}
	// }

	// public async Task<IEnumerable<Book>> GetAllBooks()
	// {
	// 	var task = await _connection.QueryAsync<Book>(
	// 		$"SELECT * FROM {TABLE_NAME} ");
	// 	await _connection.CloseAsync();
	// 	return task;
	// }
}