using Microsoft.AspNetCore.SignalR;
using Prometheus;
using WebApplication1.Controllers;

namespace WebApplication1.Hubs
{
    public class InternalServerCommunicationHub : Hub
    {
        private static readonly Gauge JobsInQueue = Metrics
    .CreateGauge("myapp_jobs_queued", "Number of jobs waiting for processing in the queue.");
        public string DoSomething()
        {
            for (long i = 0; i < 10000000; i++)
            {
                var sqrt = Math.Pow(i, 1.0 / 5);
            }
            return IdentityController.Id;
        }
        public async override Task OnConnectedAsync()
        {
            JobsInQueue.Inc();
            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            JobsInQueue.Dec();
            await base.OnDisconnectedAsync(exception);
        }
    }
}
