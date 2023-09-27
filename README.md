# WebSocket Server and Client for Testing WebSocket Connections and Timeouts

This repository contains a basic WebSocket server implemented in C# and an HTML-based WebSocket client for testing WebSocket connections and timeouts.

## WebSocket Server (C#)

The WebSocket server is a simple C# application that listens for WebSocket connections on `http://*:9000/`. It handles WebSocket requests and maintains an echo loop, sending back any received data. The server demonstrates the handling of WebSocket connections and basic WebSocket communication.

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)

### Usage

1. Clone this repository to your local machine.
2. Open a terminal and navigate to the `WebSocketServer` directory.
3. Run the WebSocket server using the `dotnet run` command.
4. The WebSocket server will start listening on `http://*:9000/`.

## WebSocket Client (HTML/JavaScript)

The WebSocket client is an HTML page that creates a WebSocket connection to the server when you click the "Start Connection" button. It displays the connection status, connection duration, and any received messages. Additionally, you can enable the sending of test data to the server every 10 seconds by checking the "Enable sending test data" checkbox.

### Usage

1. Open the `WebSocketClient.html` file in a web browser.
2. Click the "Start Connection" button to establish a WebSocket connection with the server.
3. Observe the connection status, duration, and any received messages in the HTML page.

### Dependencies

No additional dependencies are required to run the WebSocket client. It uses plain HTML and JavaScript.

## License

This repository is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
