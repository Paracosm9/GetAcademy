namespace BookHolder;

public class BookService 

{
	private IBookRepo _repository; 
	public BookService(IBookRepo repository)
	{
		_repository = repository;
	}
	public void AddBook(Book book, BookInput bookInput)
	{
		_repository.AddBook(book, bookInput);
	}
	public Task<List<Book>> GetAllBooks()
	{
		return _repository.GetAllBooks();
	}
	public Task<Book> GetBookById(int id)
	{
		return _repository.GetBookById(id);
	}

	public Task<List<Book>> GetFilteredBooks(Dictionary<string, string> query)
	{
		return _repository.GetFilteredBooks(query);
	}

	public Task<Author> GetAuthor(int id)
	{
		return _repository.GetAuthor(id);
	}
	
	public Task<List<Genre>> GetGenres(List<int> genresIds)
	{
		return _repository.GetGenres(genresIds);
	}

	public void DeleteBook(int id)
	{
		_repository.DeleteBook(id);
	}

	public void UpdateBook(BookInput bookInput, int id)
	{
		_repository.UpdateBook(bookInput, id);
	}
}