using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Dtos;
using WebApplication2.Model;
using System.Linq;


namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public OrderDetailController(DataContext dataContext)
        {
            _dataContext = dataContext;
            
        }
       
        [HttpGet]
        public async Task<IEnumerable<OrderDetail>> Get()
        {
            return await _dataContext.OrderDetails.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<OrderDetail>> Post(OrderDetailForPost orderDetailForPost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var orderDetail = new OrderDetail
                {
                    // Map properties from DTO to entity
                    OrderDetailsId = orderDetailForPost.OrderDetailsId,
                    OrderId = orderDetailForPost.OrderId,
                    ProductId = orderDetailForPost.ProductId,
                    Quantity = orderDetailForPost.Quantity
                };

                _dataContext.OrderDetails.Add(orderDetail);
                await _dataContext.SaveChangesAsync();

              
                return CreatedAtAction(nameof(GetOrderDetail), new { OrderId = orderDetail.OrderId, ProductId = orderDetail.ProductId }, orderDetail);



            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }


        }

        [HttpGet("{OrderId}/{ProductId}")]
        public async Task<ActionResult<OrderDetail>> GetOrderDetail(int OrderId, int ProductId)
        {
            var orderDetail = await _dataContext.OrderDetails.FindAsync(OrderId, ProductId);

            if (orderDetail == null)
            {
                return NotFound();
            }

            return orderDetail;
        }


        [HttpPut("{orderId}/{productId}")]
        public async Task<IActionResult> PutOrderDetail(int orderId, int productId, OrderDetailForUpdate orderDetailForUpdate)
        {
            var orderDetail = await _dataContext.OrderDetails.FindAsync(orderId, productId);
            if (orderDetail == null)
            {
                return NotFound();
            }

            // Update properties of the order detail entity
            orderDetail.Quantity = orderDetailForUpdate.Quantity;

            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailExists(orderId, productId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            


            return NoContent();
        }


        [HttpDelete("{orderId}/{productId}")]
        public async Task<IActionResult> DeleteOrderDetail(int orderId, int productId)
        {
            var orderDetail = await _dataContext.OrderDetails.FindAsync(orderId, productId);
            if (orderDetail == null)
            {
                return NotFound();
            }

            _dataContext.OrderDetails.Remove(orderDetail);
            await _dataContext.SaveChangesAsync();

            return NoContent();
        }


        private bool OrderDetailExists(int id, int productId)
        {
            return _dataContext.OrderDetails.Any(e => e.OrderDetailsId == id);
        }
       


    }
}
