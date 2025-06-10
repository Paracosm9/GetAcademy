
using System.Text.Json;

namespace BookHolder;

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