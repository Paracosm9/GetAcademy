namespace BookHolder;

public interface IBookRepo
{
	public Task AddBook(Book book);
	public Task<List<Book>>GetAllBooks();
	
	public Task<Book> GetBookById(int id);

	public Task<List<Book>>GetFilteredBooks(Dictionary<string, string> query); 
	
	public void UpdateBook(Book book);

	public void DeleteBook(int id);
}