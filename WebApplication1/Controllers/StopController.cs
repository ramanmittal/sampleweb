using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Net;
using System.Net.Sockets;
using WebApplication1.Hubs;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StopController : Controller
    {
        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index([FromServices]IHubContext<InternalServerCommunicationHub> context)
        {   
            await context.Clients.All.SendAsync("asdf", "Stop Called");
            return Ok();
        }
    }
}
