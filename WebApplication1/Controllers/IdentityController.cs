using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : Controller
    {
        private static string Id = Guid.NewGuid().ToString();
        internal static List<Thread> threads = new List<Thread>();
        internal static bool Stopped = false;
        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            return Ok(new { Id, Stopped, Count = threads.Where(x => x.ThreadState == ThreadState.Running).Count() });
        }
        [HttpGet]
        [Route("StopThred")]
        public IActionResult StopThred()
        {
            Stopped = true;
            GC.Collect();
            return Ok();
        }
        [HttpGet]
        [Route("AddThread")]
        public IActionResult AddThread()
        {
            Stopped = false;
            StartThread();
            return Ok();
        }

        internal static void StartThread()
        {
            var thread = new Thread(() =>
            {
                byte[] buffer = new byte[1024 * 1024 * 100]; // Allocate 100 MB of memory
                double result = 0;
                int i = 1;
                try
                {
                    while (!Stopped)
                    {
                        result += Math.Sqrt(i);

                        i++;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Out of memory exception: " + ex.Message);
                }
            });
            thread.Start();
            threads.Add(thread);

        }
    }
}
