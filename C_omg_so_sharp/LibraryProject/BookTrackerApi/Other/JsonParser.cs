using System.Text.Json;
using BookTrackerApi.Services;

namespace BookTrackerApi;

public class JsonParser
{
	
	public static Book ParseBook(string filePath)
	{	
		var options = new JsonSerializerOptions
		{
		PropertyNameCaseInsensitive = true //asked ChatGPT about this. 
		};

		string text = File.ReadAllText(filePath); 
		Book? book = JsonSerializer.Deserialize<Book>(text, options);
		Console.WriteLine(text);
		return book;
	}
}