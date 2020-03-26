# .NET-Core-Chat-App-using-WebSockets
Chat app made with C#, .NET Core and WebSockets.

You can see visual representation of the code in Pictures file uploaded with my code.

Tools i used for client app:
-HTML, JS
Tools i used for server app:
-WebSockets
-NET Core
-DependencyInjection
-LINQ (almost none)...
-JSON
and more

Point of this Chat app was to teach myself basics of internet communication such as
using Pipeline, Request Delegates, WebSockets, Asynchronus Programming, Client&Server communication, Threading and Middleware Build
and more.



Demonstration:
Step 1: Prep.

![](https://github.com/fisamodo/.NET-Core-Chat-App-using-WebSockets/blob/master/Pictures/1.png)

Step 2: Connecting a client to WebSocket Server Url

![](https://github.com/fisamodo/.NET-Core-Chat-App-using-WebSockets/blob/master/Pictures/2.png)

Step 3: By leaving "Recipient ID" blank, we tell the program that the message is a Broadcast to all clients 

![](https://github.com/fisamodo/.NET-Core-Chat-App-using-WebSockets/blob/master/Pictures/3.png)

Step 4: By putting another client's ConnectionID, we can send a message directly to another client, instead of a Broadcast

![](https://github.com/fisamodo/.NET-Core-Chat-App-using-WebSockets/blob/master/Pictures/4.png)

Step 5: Closing a socket disconnects us from the WebSocket Server

![](https://github.com/fisamodo/.NET-Core-Chat-App-using-WebSockets/blob/master/Pictures/5.png)
