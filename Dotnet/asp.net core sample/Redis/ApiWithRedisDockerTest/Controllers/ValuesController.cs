using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiWithRedisDockerTest.Filter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace ApiWithRedisDockerTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger _logger;

        public ValuesController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("ValuesController");
        }

        // GET api/values
        [HttpGet]
        [Aduit]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            _logger.LogDebug("Test");
            Console.WriteLine("test");
            ConnectionMultiplexer conn = await ConnectionMultiplexer.ConnectAsync("localhost");
            IDatabase db = conn.GetDatabase(0);
            var item = db.StringGet("mykey");
            if (String.IsNullOrWhiteSpace(item))
                return new string[] { "value1", "value2" };
            else
                return new string[] { "成功连接redis：", item };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
