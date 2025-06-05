namespace BookTrackerApi.Services;

public class PlugForDatabase : IBookRepo


{
	private List<Book> _books = new List<Book>
	{ new Book(
		  1,
		  "The Name of the Wind",
		  "A young wizard recounts his past, including life at a magical university.",
		  "/covers/name_of_the_wind.jpg",
		  "Unread",
		  "Highly recommended by fantasy communities.",
		  "Beautiful prose and great worldbuilding.",
		  662,
		  "https://example.com/download/name-of-the-wind",
		  "The Kingkiller Chronicle",
		  "Planned",
		  "Ongoing"
	  ),
	  new Book(
		  2,
		  "Mistborn: The Final Empire",
		  "A world ruled by an immortal emperor. A thief with unexpected powers joins a rebellion.",
		  "/covers/mistborn1.jpg",
		  "Read",
		  "Brandon Sanderson never disappoints.",
		  "Smart magic system and satisfying plot.",
		  541,
		  "https://example.com/download/mistborn1",
		  "Mistborn",
		  "Finished",
		  "Ended"
	  ),
	  new Book(
		  3,
		  "Re:Zero − Starting Life in Another World",
		  "A boy is transported to a fantasy world and discovers he can return from death.",
		  "/covers/rezero.jpg",
		  "Reading",
		  "Light novel adaptation with intense emotional arcs.",
		  "Sometimes repetitive, but very emotional.",
		  340,
		  "https://example.com/download/rezero1",
		  "Re:Zero",
		  "Reading",
		  "Ongoing")
	  };

	public void AddBook(Book book)
	{
		 book.Id = GetNewId();
		_books.Add(book);
	}

	public List<Book> GetAllBooks()
	{
		return _books;
	}

	private int GetNewId()
	{
		return _books.LastOrDefault().Id + 1;
	}

	public Book GetBookById(int id)
	{
		return _books.FirstOrDefault(book => book.Id == id) ?? throw new KeyNotFoundException();
	}
}