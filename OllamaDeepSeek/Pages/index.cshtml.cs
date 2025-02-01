using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OllamaDeepSeek.Pages;

public class ChatRequest : PageModel{
    public List<ChatMessage> Messages{ get; set; } = [];
    
    [BindProperty] 
    public required string UserMessage{ get; set; }

    public void OnGet(){ 
        var content = $"DeepSeek: {DateTime.Now:MM/dd/yyyy - HH:mm:ss} : Hello, how a can help you \ud83d\ude03 ? "; 
        Messages.Add(new ChatMessage{ role = "assistant", content = content });        
    }
}

public class ChatResponse{
    public required string model { get; set; }
    public required ChatMessage message { get; set; }
}

public class ChatMessage{
    public required string role { get; set; }
    public required  string content { get; set; }
}