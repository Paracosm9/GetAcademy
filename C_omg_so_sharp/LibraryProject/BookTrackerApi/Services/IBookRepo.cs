namespace BookTrackerApi.Services;

public interface IBookRepo
{
	public void AddBook(Book book);
	public List<Book> GetAllBooks();
	
	public Book GetBookById(int id);

	public List<Book> GetFilteredBooks(Dictionary<string, string> query); 

}