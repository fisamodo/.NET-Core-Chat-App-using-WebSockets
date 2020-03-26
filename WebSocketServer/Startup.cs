using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.WebSockets;
using System.Threading;
using WebSocketServer.Middleware;


namespace WebSocketServer
{

    public class Startup
    {
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddWebSocketManager();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) //request pipeline, google the pic to understand better
        {
            app.UseWebSockets(); //middleware, part of pipeline request

            app.UseWebSocketServer();
            

            app.Run(async context =>{ //3rd request delegate
                Console.WriteLine("Hello from the 3rd request delegate."); // runs after 2nd delegate when i go to http://localhost5000
                await context.Response.WriteAsync("Hello from the 3rd request delegate");
            });
        }

       

        }
       
    }

