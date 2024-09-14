using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace server
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockListApiController : ControllerBase
    {
        private readonly BlockListContext _dbContext;
        public BlockListApiController(BlockListContext dbContext){
            _dbContext = dbContext;
        }
        [HttpGet]
        public ActionResult<IEnumerable<BlockedUser>> Get()
        {
            var BlockedUser = _dbContext.BlockedUsers.Find(1);

            if (BlockedUser is null)
            {
                return NotFound();
            }

            return Ok(BlockedUser);
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            var BlockedUser = new BlockedUser(){
                Name = "test"
            };

            _dbContext.Add(BlockedUser);
            _dbContext.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}