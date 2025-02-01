// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.getElementById("chatForm").addEventListener("submit", async function (e) { e.preventDefault();
    let submitButton = document.getElementById("buttonSend");
    submitButton.disabled = true;

    let inputField = document.getElementById("userMessage");
    let message = inputField.value.trim();
    if (!message) return;

    let chatBox = document.getElementById("chatBox");
    let data = new Date();
    data = data.toLocaleString("en-US", {
        month: "2-digit",
        day: "2-digit",
        year: "numeric",
        hour: "2-digit",
        minute: "2-digit",
        second: "2-digit",
        hour12: false
    }).replace(",", " -");

    chatBox.innerHTML += `<div class="message user"> You: <i>${data}</i> ${message}</div>`;
    inputField.value = "";
    chatBox.scrollTop = chatBox.scrollHeight;

    let assistantMessage = document.createElement("div");
    assistantMessage.className = "message assistant";
    assistantMessage.innerHTML =
        `<span class="response-content"></span> 
            <span class="spinner-container">
            <div class="spinner-grow text-primary" role="status">
            <span class="visually-hidden"></span>
            </div>
            </span>`;
    chatBox.appendChild(assistantMessage);
    chatBox.scrollTop = chatBox.scrollHeight;

    try {
        const url = new URL('/Chat/GetChatResponse', window.location.origin);
        url.searchParams.append('message', message);

        const response = await fetch(url, {
            method: 'GET',
            headers: {'Accept': 'text/event-stream'}
        });


        if (!response.ok) console.log("Error call the controller. ");

        const reader = response.body.getReader();
        const decoder = new TextDecoder();
        const responseContent = assistantMessage.querySelector('.response-content');

        let dataVisibleChat = false;

        while (true) {
            const { value, done } = await reader.read();
            if (done) break;

            let chunk = decoder.decode(value, { stream: true });
            chunk.split('\n').forEach(line => {
                if (line.trim() === "") return;
                try {
                    const data = JSON.parse(line);
                    const content = data.message?.content || "";
                    if (!dataVisibleChat) {
                        let responseData = new Date();
                        responseData = responseData.toLocaleString("en-US", {
                            month: "2-digit",
                            day: "2-digit",
                            year: "numeric",
                            hour: "2-digit",
                            minute: "2-digit",
                            second: "2-digit",
                            hour12: false
                        }).replace(",", " -");
                        responseContent.innerHTML += `DeepSeek: <i>${responseData}</i> : ${content}`;
                        dataVisibleChat = true;
                    }  else {
                        responseContent.innerHTML += content;
                    }
                    chatBox.scrollTop = chatBox.scrollHeight;
                } catch (error) {
                    console.error("Error to process JSON:", error);
                }
            });
        }
        const spinnerContainer = assistantMessage.querySelector('.spinner-container');
        spinnerContainer.style.display = 'none';
        submitButton.disabled = false;
    } catch (error) {
        submitButton.disabled = false;
        console.error("It was not possible to obtain data from the OLLAMA API.:", error);
        const spinnerContainer = assistantMessage.querySelector('.spinner-container');
        spinnerContainer.style.display = 'none';
        assistantMessage.innerHTML = "Really, can't believe that, we get a error.";
    }
});