using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OllamaDeepSeek.Pages;

public class ChatRequest : PageModel{
    public List<ChatMessage> Messages{ get; set; } = [];

    public void OnGet(){ 
        var content = $"DeepSeek: {DateTime.Now:MM/dd/yyyy - HH:mm:ss} : Hello, how a can help you \ud83d\ude03 ? "; 
        Messages.Add(new ChatMessage{ Role = "assistant", Content = content });        
    }
}

public class ChatMessage{
    public required string Role { get; set; }
    public required  string Content { get; set; }
}