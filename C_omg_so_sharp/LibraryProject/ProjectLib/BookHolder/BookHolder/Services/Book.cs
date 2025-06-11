namespace BookHolder;


//lage et prosjekt - Dag
public class Book
{
	public int Id { get; set; } // database? 

	public string Title { get; set; }

	// public Author Author { get; set; }
	// public Genre Genre { get; set; }
	public string Description { get; set; }
	public string CoverPath { get; set; } // change to byte[] after
	public string Status { get; set; } // Read / not read
	public string Notes { get; set; }
	public string Review { get; set; }
	public int AmountOfPages { get; set; }
	public string DownloadLink { get; set; }
	public string Serie { get; set; }
	public string ReadingStatus { get; set; } // Planned / Reading / Finished
	public string PublishingStatus { get; set; } // Ongoing / Ended
	// public Tier Tier { get; set; } // Tier: S / A+ / A / B / C / D / E


	public string[] propertyNames =
	[ "Id", "Title", "Desctiption", "CoverPath", "Status", "Notes", "Review", "AmountOfPages", "DownloadLink", "Serie", "ReadingStatus", "PublishingStatus" ];
	
	public Book(int Id, string Title,
		string Description, string CoverPath,
		string Status, string Notes, string Review, int AmountOfPages, string DownloadLink, string Serie,
		string ReadingStatus, string PublishingStatus
	)
	{
		this.Id = Id;
		this.Title = Title;
		this.Description = Description;
		this.CoverPath = CoverPath;
		this.Status = Status;
		this.Notes = Notes;
		this.Review = Review;
		this.AmountOfPages = AmountOfPages;
		this.DownloadLink = DownloadLink;
		this.Serie = Serie;
		this.ReadingStatus = ReadingStatus;
		this.PublishingStatus = PublishingStatus;
	}
	
	public Book(){}
	
}