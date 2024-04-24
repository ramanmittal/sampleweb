using Microsoft.AspNetCore.SignalR;
using WebApplication1.Controllers;

namespace WebApplication1.Hubs
{
    public class InternalServerCommunicationHub : Hub
    {

        public string DoSomething()
        {
            for (long i = 0; i < 10000000; i++)
            {
                var sqrt = Math.Pow(i, 1.0 / 5);
            }
            return IdentityController.Id;
        }
    }
}
