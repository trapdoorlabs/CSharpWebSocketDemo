using System.Net;
using System.Net.WebSockets;

class WebSocketServer
{
    static async Task Main(string[] args)
    {
        var httpListener = new HttpListener();
        httpListener.Prefixes.Add("http://*:9000/");
        httpListener.Start();

        Console.WriteLine("WebSocket server is running. Waiting for connections...");

        while(true)
        {
            var context = await httpListener.GetContextAsync();
            if(context.Request.IsWebSocketRequest)
            {
                // If the web socket request is received, handle it and return
                await HandleWebSocketRequest(context);
            }
            else
            {
                // If the web socket request is not received, return a 400 HTTP error
                context.Response.StatusCode = 400;
                context.Response.Close();
            }
        }
    }

    /// <summary>
    /// Handle a WebSocket request by accepting the connection and performing a simple echo loop.
    /// </summary>
    /// <param name="context">HttpListenerContext with Websocket</param>
    static async Task HandleWebSocketRequest(HttpListenerContext context)
    {
        var webSocketContext = await context.AcceptWebSocketAsync(null);
        var webSocket = webSocketContext.WebSocket;
        var buffer = new byte[1024];

        // While the WebSocket connection remains open run a simple loop that receives data and sends it back.
        while(webSocket.State == WebSocketState.Open)
        {
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            if(result.MessageType == WebSocketMessageType.Close)
            {
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                break;
            }
            else if(result.MessageType == WebSocketMessageType.Binary)
            {
                context.Response.StatusCode = 400;
                context.Response.Close();
                break;
            }
            else if(result.MessageType == WebSocketMessageType.Text)
            {
                // Reduce the buffer to only be the size of the received data
                var actual = new ArraySegment<byte>(buffer, 0, result.Count);
                await webSocket.SendAsync(actual, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        Console.WriteLine("WebSocket connection closed");

        // Close the WebSocket connection if still open
        if(webSocket.State != WebSocketState.Closed)
        {
            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
        }
    }
}
