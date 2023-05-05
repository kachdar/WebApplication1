using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services.BookService;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly PostgreSqlContext postgreSqlContext;
        public OrderController(PostgreSqlContext context)
        {
            postgreSqlContext = context;
        }

        [HttpGet("{id}\books")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            //var order = await postgreSqlContext.Orders.FindAsync(id);
            //var result = postgreSqlContext.Books.Where(b => b.Orders.Any(o => o.Id == id));

            var result = await postgreSqlContext.Orders.Include(o => o.Books).FirstAsync(o => o.Id == id);

            if (result is null)
                return NotFound("Book doesn't exist");

            return Ok(result);
        }
    }
}
