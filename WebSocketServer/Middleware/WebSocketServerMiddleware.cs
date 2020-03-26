using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace WebSocketServer.Middleware
{
    public class WebSocketServerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly WebSocketServerConnectionManager _manager;
        public WebSocketServerMiddleware(RequestDelegate next, WebSocketServerConnectionManager manager)
        {
            _next =  next;
            _manager = manager;
        }

         public async Task InvokeAsync(HttpContext context)
         {
                if(context.WebSockets.IsWebSocketRequest) //checks if it's a websocket, if so we create websocket object with await context.web.....
                {
                    WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync(); //async method, may take time, accepts requests
                    Console.WriteLine("WebSocket Connected"); 

                    string ConnID = _manager.AddSocket(webSocket);
                    await SendConnIDAsync(webSocket, ConnID);
                    
                    await ReceiveMessage(webSocket, async(result, Buffer) => //message receiver
                    {
                        if(result.MessageType == WebSocketMessageType.Text) //chekcs if types are correct
                        {
                            Console.WriteLine("Message Recieved");
                            Console.WriteLine($"Message: {Encoding.UTF8.GetString(Buffer, 0, result.Count)}");  //decoding a buffer using UTF8
                            return;
                        }
                        else if(result.MessageType == WebSocketMessageType.Close) //closes if requested
                        {
                            Console.WriteLine("Recieved Close message");
                            return;
                        }
                    }   );
                }
                else
                {
                    Console.WriteLine("Hello from the 2rd request delegate."); //runs when i go to http://localhost5000
                    await _next(context); //creates next request delegate in the pipeline
                }
         }

         private async Task SendConnIDAsync(WebSocket socket, string connID) //sends connection id back to client
         {
             var buffer = Encoding.UTF8.GetBytes("ConnID: "+connID); //encodes connectionID
             await socket.SendAsync(buffer, WebSocketMessageType.Text,true, CancellationToken.None);
         }
         private async Task ReceiveMessage(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            var buffer = new byte[1024 * 4];

            while(socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer),
                    cancellationToken: CancellationToken.None);
                
                handleMessage(result, buffer);
            }
        }


    }
}