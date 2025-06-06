namespace BookTrackerApi.Services;

public class BookService 

{
	
	private IBookRepo _repository; 
	public BookService(IBookRepo repository)
	{
		_repository = repository;
	}
	public void AddBook(Book book)
	{
		_repository.AddBook(book);
	}
	public List<Book> GetAllBooks()
	{
		return _repository.GetAllBooks();
	}
	public Book GetBookById(int id)
	{
		return _repository.GetBookById(id);
	}

	public List<Book> GetFilteredBooks(Dictionary<string, string> query)
	{
		return _repository.GetFilteredBooks(query);
	}
}