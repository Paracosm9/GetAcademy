namespace BookHolder;

public interface IBookRepo
{
	public Task AddBook(Book book, BookInput bookInput);
	public Task<List<Book>>GetAllBooks();
	
	public Task<Book> GetBookById(int id);

	public Task<List<Book>>GetFilteredBooks(Dictionary<string, string> query); 
	
	public void UpdateBook(BookInput bookInput, int id);

	public void DeleteBook(int id);
	public Task<Author> GetAuthor(int id);
	public Task<List<Genre>> GetGenres(List<int> genresIds);
}