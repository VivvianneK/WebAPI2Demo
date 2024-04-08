using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Dtos;
using WebApplication2.Model;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public OrderController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Order>> Get()
        {
            return await _dataContext.Orders.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var order = await _dataContext.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        
       
        [HttpPost]
        public async Task<ActionResult<Order>> Post(OrderForPost orderForPost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                // Create a new order object
                var order = new Order
                {
                    OrderDate = DateTime.Now, // Set the order date to current date/time
                    ClientId = orderForPost.ClientId, // Assign the client ID
                    OrderDetails = new List<OrderDetail>() // Initialize the order details list
                };

                // Iterate through the selected products and create order details
                foreach (var orderDetailForPost in orderForPost.OrderDetails)
                {
                    var product = await _dataContext.Products.FindAsync(orderDetailForPost.ProductId);
                    if (product != null)
                    {
                        var orderDetail = new OrderDetail
                        {
                            ProductId = product.ProductId,
                            Quantity = orderDetailForPost.Quantity,
                            Product = product
                        };
                        order.OrderDetails.Add(orderDetail); // Add order detail to the order
                    }
                    else
                    {
                        return NotFound($"Product with ID {orderDetailForPost.ProductId} not found.");
                    }
                }

                _dataContext.Orders.Add(order); 
                await _dataContext.SaveChangesAsync(); 

                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OrderForUpdate orderForUpdate)
        {
            // Check if the ID in the URL matches the ID in the request body
            if (id != orderForUpdate.OrderId)
            {
                return BadRequest("Order ID mismatch");
            }

            // Find the order in the database
            var order = await _dataContext.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            // Update order properties based on the DTO
            order.OrderDate = orderForUpdate.OrderDate;
            order.ClientId = orderForUpdate.ClientId;

            try
            {
                // Save changes to the database
                await _dataContext.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle concurrency exceptions if needed
                return StatusCode(500, "Concurrency error occurred");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _dataContext.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _dataContext.Orders.Remove(order);
            await _dataContext.SaveChangesAsync();

            return NoContent();
        }

    }
}
