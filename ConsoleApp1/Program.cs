// See https://aka.ms/new-console-template for more information

using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.Concurrent;
var tasks = new List<Task>();
var set = new ConcurrentDictionary<string, List<HubConnection>>();
HubConnection asServerHubConnection = new HubConnectionBuilder()
                    .WithUrl("http://127.0.0.1:21903/internalServerCommunicationHub")
                    //.WithUrl("http://localhost:5160/internalServerCommunicationHub")
                    .WithAutomaticReconnect()
                    .Build();
asServerHubConnection.StartAsync().Wait();
Console.WriteLine("Hello, World!");
Console.ReadLine();
