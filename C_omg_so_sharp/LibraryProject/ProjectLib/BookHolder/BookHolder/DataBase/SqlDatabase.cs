
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
	
	//en solution

	public async Task AddBook(Book book, BookInput bookInput) //Task = same as void. 
	{
		_connection = new NpgsqlConnection(
			connectionString: CONNECTION_STRING);
		_connection.Open();
		string commandText =
			$"INSERT INTO books (title, author, genre, description,\"coverPath\", status,notes, review, \"amountOfPages\",\"downloadLink\",serie,\"readingStatus\",\"publishingStatus\") " +
			$"VALUES (@title, @author, @genre, @description,@coverPath,@status,@notes,@review,@amountOfPages,@downloadLink,@serie,@readingStatus,@publishingStatus);";
		var queryArgs = new
		{ title = book.Title,
		  author = bookInput.Author,
		  genre = bookInput.Genre.ToArray(),
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
		var bookDict = new Dictionary<int, Book>();
		_connection = new NpgsqlConnection(
			connectionString: CONNECTION_STRING);
		_connection.Open();
		string commandText =
			$" SELECT  b.id, b.title,\n b.description, b.status, b.\"coverPath\",\n b.status, b.notes, b.review, b.\"amountOfPages\",  \n b.\"downloadLink\",b.serie,b.\"readingStatus\", \n b.\"publishingStatus\", \n a.id AS a_id,  a.name AS name,  a.birthday AS birthday,  \n a.country AS country,\n g.id AS g_id, g.\"genreName\" as \"genreName\", \n g.\"genreDescription\" as \"genreShortDescription\"\n FROM books b \n JOIN authors a ON b.author = a.id\n join unnest(b.genre) AS genre_id ON TRUE\nJOIN genres g ON g.id = genre_id\n ORDER BY id";
		var task = _connection.Query<Book, Author, Genre, Book>(
			commandText,
			(book, author, genre) =>
			{
				if (!bookDict.TryGetValue(book.Id, out var existingBook))
				{
					existingBook = book;
					existingBook.Author = author;
					existingBook.Genres = new List<Genre>();
					bookDict.Add(existingBook.Id, existingBook);
				}

				existingBook.Genres.Add(genre);
				return existingBook;
			},
			splitOn: "a_id,g_id"
		);
		await _connection.CloseAsync();
		return bookDict.Values.ToList();
	}

	public async Task<Book> GetBookById(int id)
	{
		var bookDict = new Dictionary<int, Book>();
		_connection = new NpgsqlConnection(
			connectionString: CONNECTION_STRING);
		_connection.Open();
		string commandText =
			$" SELECT  b.id, b.title,\n b.description, b.status, b.\"coverPath\",\n b.status, b.notes, b.review, b.\"amountOfPages\",  \n b.\"downloadLink\",b.serie,b.\"readingStatus\", \n b.\"publishingStatus\", \n a.id AS a_id,  a.name AS name,  a.birthday AS birthday,  \n a.country AS country,\n g.id AS g_id, g.\"genreName\" as \"genreName\", \n g.\"genreDescription\" as \"genreDescription\"\n FROM books b \n JOIN authors a ON b.author = a.id\n join unnest(b.genre) AS genre_id ON TRUE\nJOIN genres g ON g.id = genre_id\n WHERE b.id = @id";
		var queryArgs = new
		{ Id = id };
		var task =
			_connection
				.Query<Book, Author, Genre,
					Book>( //https://stackoverflow.com/questions/7508322/how-do-i-map-lists-of-nested-objects-with-dapper
					commandText,
					(book, author, genre) =>
					{
						if (!bookDict.TryGetValue(book.Id, out var existingBook))
						{
							existingBook = book;
							existingBook.Author = author;
							existingBook.Genres = new List<Genre>();
							bookDict.Add(existingBook.Id, existingBook);
						}

						existingBook.Genres.Add(genre);
						return existingBook;
					},
					queryArgs,
					splitOn: "a_id,g_id"
				);
		await _connection.CloseAsync();
		return task.FirstOrDefault();
	}

	public async Task<List<Book>> GetFilteredBooks(Dictionary<string, string>? query)
	{
		_connection = new NpgsqlConnection(
			connectionString: CONNECTION_STRING);
		_connection.Open();

		var bookDict = new Dictionary<int, Book>();
		string commandText = CreateCommandString(query);
		var task =
			_connection.Query<Book, Author, Genre, Book>(
					commandText,
					(book, author, genre) =>
					{
						if (!bookDict.TryGetValue(book.Id, out var existingBook))
						{
							existingBook = book;
							existingBook.Author = author;
							existingBook.Genres = new List<Genre>();
							bookDict.Add(existingBook.Id, existingBook);
						}

						existingBook.Genres.Add(genre);
						return existingBook;
					},
					splitOn: "a_id,g_id");
		await _connection.CloseAsync();
		return bookDict.Values.ToList();
	}

	public async void UpdateBook(BookInput bookInput, int id)
	{
		_connection = new NpgsqlConnection(
			connectionString: CONNECTION_STRING);
		_connection.Open();
		string commandText = $"UPDATE {TABLE_NAME} SET title = @title, author = @author, genre = @genre, description = @description, \"coverPath\" = @coverPath, status = @status,notes = @notes, review = @review, \"amountOfPages\" = @amountOfPages, \"downloadLink\" = @downloadLink,serie = @serie,\"readingStatus\" = @readingStatus,\"publishingStatus\" = @publishingStatus WHERE id = @id "; 
                // WHERE id = @id";
		var queryArgs = new
		{ Id = id , 
		title = bookInput.Title, 
		author = bookInput.Author,
		genre = bookInput.Genre,
		description = bookInput.Description,
		coverPath = bookInput.CoverPath,
		status = bookInput.Status,
		notes = bookInput.Notes,
		review = bookInput.Review,
		amountOfPages = bookInput.AmountOfPages,
		downloadLink = bookInput.DownloadLink,
		serie = bookInput.Serie,
		readingStatus = bookInput.ReadingStatus,
		publishingStatus = bookInput.PublishingStatus,
		};
		await _connection.ExecuteAsync(commandText, queryArgs);
		await _connection.CloseAsync();
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
		string commandText = $"SELECT  b.id, b.title,\n b.description, b.status, b.\"coverPath\",\n b.status, b.notes, b.review, b.\"amountOfPages\",  \n b.\"downloadLink\",b.serie,b.\"readingStatus\", \n b.\"publishingStatus\", \n a.id AS a_id,  a.name AS name,  a.birthday AS birthday,  \n a.country AS country,\n g.id AS g_id, g.\"genreName\" as \"genreName\", \n g.\"genreDescription\" as \"genreDescription\"\n FROM {TABLE_NAME}  b \n JOIN authors a ON b.author = a.id\n join LATERAL unnest(b.genre) AS genre_id ON TRUE\nJOIN genres g ON g.id = genre_id\n  ";
		int iterator = 0;
		foreach (var (key, value) in query)
		{
			Console.WriteLine(key + " " + value);
			if (key.ToLower() == "orderby") continue;
			commandText += iterator == 0
				? $"WHERE \"{key}\"={GetFixedValue(key, value)} "
				: $" AND \"{key}\"={GetFixedValue(key, value)} ";
			iterator++;
		}

		if (query.TryGetValue("orderBy", out var orderBy))
		{
			commandText = commandText + "ORDER BY " + orderBy;
		}
		return commandText;
	}

	private string GetFixedValue(string key, string value)
	{
		return (key is "amountOfPages" or "id") ? value : $"'{value}'";
	}

	public async Task<Author> GetAuthor(int id)
	{
		_connection = new NpgsqlConnection(
			connectionString: CONNECTION_STRING);
		_connection.Open();
		
		string commandText = $"SELECT name, birthday, country FROM authors WHERE ID = @id ";
		var queryArgs = new
		{ Id = id };
		var author =   _connection.Query<Author>(commandText, queryArgs);
		await _connection.CloseAsync();
		return author.FirstOrDefault();
	}
	
	public async Task<List<Genre>> GetGenres(List<int> genresIds)
	{
		int[] genres = genresIds.ToArray();
		Console.WriteLine(genres.ToString());
		_connection = new NpgsqlConnection(
			connectionString: CONNECTION_STRING);
		_connection.Open();
		string commandText = $"SELECT \"genreName\", \"genreDescription\" FROM genres WHERE id = ANY (ARRAY [@array])";
		var queryArgs = new
		{ array = genres };
		var task =  _connection.Query<Genre>(commandText, queryArgs);
		await _connection.CloseAsync();
		return task.ToList();
	}
}