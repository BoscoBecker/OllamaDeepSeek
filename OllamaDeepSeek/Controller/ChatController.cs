using System.Text;
using Microsoft.AspNetCore.Mvc;
using OllamaDeepSeek.Pages;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace OllamaDeepSeek.Controllers;

public class ChatController() : Controller{

    [HttpGet]
    public async Task GetChatResponse([FromQuery] string message){
        Response.ContentType = "text/event-stream";
        Response.Headers.CacheControl = "no-cache";
        Response.Headers.Connection = "keep-alive";
        
        var messages = new[]{ new{ role = "user", content = message } };
        var options = new{
            seed = 101,
            temperature = 0.8,
            top_p = 0.9,
            top_k = 40,
            stream = true 
        };
    
        var requestBody = JsonSerializer.Serialize(new{ model = "deepseek-r1:1.5b", messages, options });
        using var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:11434/api/chat"){
            Content = new StringContent(requestBody, Encoding.UTF8, "application/json")};

        using   var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
        await using var stream = await response.Content.ReadAsStreamAsync();
        using var reader = new StreamReader(stream);
        await using var writer = new StreamWriter(Response.Body, Encoding.UTF8, leaveOpen: true);
        while (!reader.EndOfStream){
            var line = await reader.ReadLineAsync();
            if (string.IsNullOrEmpty(line)) continue;
            await writer.WriteAsync(line);
            await writer.FlushAsync();
        }
    }
}
