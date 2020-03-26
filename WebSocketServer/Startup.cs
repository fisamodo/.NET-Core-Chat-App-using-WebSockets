using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using  System.Net.WebSockets;


namespace WebSocketServer
{

    public class Startup
    {
        
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) //request pipeline, google the pic to understand better
        {
            app.UseWebSockets(); //middleware, part of pipeline request

            app.Use(async (context, next) => //middleware, part of pipeline request
             {
                WriteRequestParam(context);
                if(context.WebSockets.IsWebSocketRequest) //checks if it's a websocket, if so we create websocket object with await context.web.....
                {
                    WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync(); //async method, may take time, accepts requests
                    Console.WriteLine("WebSocket Connected");    
                }
                else
                {
                    Console.WriteLine("Hello from the 2rd request delegate."); //runs when i go to http://localhost5000
                    await next(); //creates next request delegate in the pipeline
                }
            });

            app.Run(async context =>{
                Console.WriteLine("Hello from the 3rd request delegate."); //runs after 2nd delegate when i go to http://localhost5000
                await context.Response.WriteAsync("Hello from the 3rd request delegate");
            });

        }
        public void WriteRequestParam(HttpContext context)
        {
            Console.WriteLine("Request Method: "+ context.Request.Method);
            Console.WriteLine("Request Protocol: "+ context.Request.Protocol);

            if(context.Request.Headers !=null)
            {
                foreach(var h in context.Request.Headers)
                {
                    Console.WriteLine("--> " + h.Key + ":" + h.Value);
                }
            }

        }
    }
}
