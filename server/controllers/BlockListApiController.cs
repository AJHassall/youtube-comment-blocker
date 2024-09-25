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
            var BlockedUser = _dbContext.BlockedUsers.ToList();

            return Ok(BlockedUser);
        }

        [HttpPut]
        public void Put([FromBody] string username)
        {
            var BlockedUser = new BlockedUser(){
                Name = username,
            };

            _dbContext.Add(BlockedUser);
            _dbContext.SaveChanges();
        }
    }
}