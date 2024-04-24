// See https://aka.ms/new-console-template for more information

using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.Concurrent;
var tasks = new List<Task>();
var set = new ConcurrentDictionary<string, List<HubConnection>>();
Parallel.For(0, 10000, i =>
{
    if (set.Keys.Count <= 1 || !set.Keys.All(x => set[x].Count >= 5))
    {
        HubConnection asServerHubConnection = new HubConnectionBuilder()
                    .WithUrl("http://localhost:5160/internalServerCommunicationHub")
                    //.WithUrl("http://127.0.0.1:29772/internalServerCommunicationHub")
                    .WithAutomaticReconnect()
                    .Build();
        asServerHubConnection.StartAsync().Wait();
        var result = asServerHubConnection.InvokeAsync<string>("DoSomething");
        result.Wait();
        var value = result.Result;
        set.AddOrUpdate(value, new List<HubConnection> { asServerHubConnection }, (key, list) =>
        {
            list.Add(asServerHubConnection);
            return list;
        });

        Console.WriteLine(value);
    }
});
System.Diagnostics.Debugger.Break();
//for (int i = 0; i < 10000; i++)
//{
//var task = Task.Factory.StartNew(() =>
//{
//    var asServerHubConnection = new HubConnectionBuilder()
//                 .WithUrl("http://localhost:5160/internalServerCommunicationHub")
//                 .WithAutomaticReconnect()
//                 .Build();
//    asServerHubConnection.StartAsync().Wait();
//    var result = asServerHubConnection.InvokeAsync<string>("DoSomething");
//    var value = result.Result;
//    Console.WriteLine(value);
//});
//tasks.Add(task);
//}
Task.WaitAll(tasks.ToArray());
Console.WriteLine("Hello, World!");
Console.ReadLine();
