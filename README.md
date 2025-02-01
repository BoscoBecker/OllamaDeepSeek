#  Stream Chat DeepSeek Offline with Ollama  

![image](https://github.com/user-attachments/assets/c9e44aab-a0f8-48c7-b669-aa52d478af36)


## Technologies Used
This project integrates the **DeepSeek** model with the **Ollama** service, allowing the user to interact with a chat assistant in real time. The goal is to create a chat application where the AI ​​model responds to messages sent by the user.
**DeepSeek** is an advanced language model provided by Ollama and available in its [official library](https://ollama.com/library/deepseek-r1).

## Project Structure
- **DeepSeek Template**: [DeepSeek - Ollama](https://ollama.com/library/deepseek-r1)
- Template Version: deepseek-r1:1.5b
- **Application Route**: [http://localhost:5268](http://localhost:5268)
- **Ollama API Endpoint**: [http://localhost:11434/api/chat](http://localhost:11434/api/chat)

![image](https://github.com/user-attachments/assets/67e78042-f1d8-45df-99f3-f4083ed24c1c)


## Functionality

- **User Input**: The user sends messages to the AI assistant.
- **Assistant Response**: The AI assistant (DeepSeek) responds based on the messages sent, using the **Ollama** API. - **Chat Interface**: The user interface displays messages in real time, with a loading spinner while waiting for the assistant's response.

## Requirements

- **.NET 9** or higher
- ASPNET core
- **Ollama API**: Install Ollama and configure the API at [localhost:11434](http://localhost:11434).
- **Frontend**: HTML, CSS and JavaScript.

## How to Run the Project

```bash
git https://github.com/BoscoBecker/OllamaDeepSeek.git
cd OllamaDeepSeek
```

Install the dependencies: If you are using .NET, make sure all the dependencies are installed. Otherwise, follow the documentation of the framework you are using.

Start the backend server: In the backend (ASP.NET) project directory, run:

```bash
dotnet run
```

Access the application: Open the browser and go to http://localhost:5268.

Make sure that Ollama is running on port 11434.

Architecture
Frontend (HTML, JavaScript):

The application allows the user to send text messages.
The messages are sent to the server using AJAX (fetch).
The responses from the model are displayed to the user with a "spinner" effect until the complete response is received.
Backend (ASP.NET):

The ASP.NET server handles the chat requests.
It sends the received messages to the Ollama API to generate the responses.
It uses Text-Event-Stream (SSE) to send responses in real time while the model is processing.
Backend Code (ASP.NET)
Example of endpoint code that calls Ollama:

``` csharp

[HttpGet("GetChatResponse")]
public async Task<IActionResult> GetChatResponse(string message){
 try{
 
 using (var client = new HttpClient()){
 var content = new StringContent(JsonConvert.SerializeObject(new { message }), Encoding.UTF8, "application/json");
 var response = await client.PostAsync("http://localhost:11434/api/chat", content);// Call the Ollama API
 var responseString = await response.Content.ReadAsStringAsync();
 
  return Ok(responseString);
  }
 }
 catch (Exception ex){
 return StatusCode(500, $"Error processing request: {ex.Message}");
 }
}
```
How Communication with Ollama Works
Ollama is used as the backend of the AI ​​model, which processes the sent messages and generates the responses.

Request to the Ollama API
When the user sends a message, the backend makes a POST request to the endpoint http://localhost:11434/api/chat with the request body containing the message:

``` json
{
"message": "Your message"
}
```
The response from Ollama is returned and sent to the frontend, where the content is displayed on the chat screen.

Frontend (JavaScript)
Example JavaScript code to send the message and display the response:
``` javascript

  const url = new URL('/Chat/GetChatResponse', window.location.origin);
        url.searchParams.append('message', message);
  const response = await fetch(url, { method: 'GET', headers: {'Accept': 'text/event-stream'} });

   ```
Contribution : Feel free to open issues or submit pull requests to improve the project! And Give me a  🌟 .

