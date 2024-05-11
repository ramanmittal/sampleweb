using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
namespace WebApplication2.Controllers
{
    public class ConnectionController : Controller
    {
        public static List<HubConnection> conns = new List<HubConnection>();
        // GET: ConnectionController
        public ActionResult Index()
        {
            return View(conns.Select(x=>x.State).ToList());
        }      

        // GET: ConnectionController/Create
        public ActionResult Create()
        {
            HubConnection asServerHubConnection = new HubConnectionBuilder()
                    .WithUrl("http://127.0.0.1:21903/internalServerCommunicationHub")
                    //.WithUrl("https://localhost:7160/internalServerCommunicationHub")
                    .WithAutomaticReconnect()
                    .Build();
            asServerHubConnection.Closed += async (exp) =>
            {
                Console.WriteLine("Connection Closed");
            };
            asServerHubConnection.Reconnecting += async (exp) =>
            {
                Console.WriteLine("Connection Reconnecting");
            };
            asServerHubConnection.Reconnected += async (exp) =>
            {
                Console.WriteLine("Connection Reconnected");
            };
            asServerHubConnection.On<string>("asdf", (text) => {
                Console.WriteLine(text);
            });
            asServerHubConnection.StartAsync().Wait();
            conns.Add(asServerHubConnection);
            return RedirectToAction("Index");
        }

        // GET: ConnectionController/Delete/5
        public ActionResult Delete(int id)
        {
            if (conns.Count > id)
            {
                var conn = conns[id];
                if (conn.State != HubConnectionState.Disconnected)
                {
                    conn.StopAsync().Wait();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
