public class BookInput
{
	public string Title { get; set; }
	public int Author { get; set; }
	public List<int> Genre { get; set; }
	public string Description { get; set; }
	public string CoverPath { get; set; }
	public string Status { get; set; }
	public string Notes { get; set; }
	public string Review { get; set; }
	public int AmountOfPages { get; set; }
	public string DownloadLink { get; set; }
	public string Serie { get; set; }
	public string ReadingStatus { get; set; }
	public string PublishingStatus { get; set; }

	public BookInput(string title, int author, List<int> genre, string description, string coverPath, string status,
		string notes, string review, int amountOfPages, string downloadLink, string serie, string readingStatus,
		string publishingStatus)
	{
		Title = title;
		Author = author;
		Genre = genre;
		Description = description;
		CoverPath = coverPath;
		Status = status;
		Notes = notes;
		Review = review;
		AmountOfPages = amountOfPages;
		DownloadLink = downloadLink;
		Serie = serie;
		ReadingStatus = readingStatus;
		PublishingStatus = publishingStatus;
	}
}