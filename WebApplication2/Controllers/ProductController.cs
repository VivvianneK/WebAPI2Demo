using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Dtos;
using WebApplication2.Model;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public ProductController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>>Get()
        {
            return await _dataContext.Products.ToListAsync();

        }

        [HttpPost]
        public async Task<ActionResult> Post(ProductForPost productForPost)
        {
            // Convert ProductForPost to Product
            var product = new Product
            {
                Name = productForPost.Name,
                Price = productForPost.Price,
                Category = productForPost.Category,
                Description = productForPost.Description,
                Quantity = productForPost.Quantity,
                Imageurl = productForPost.Imageurl
                
            };
            _dataContext.Products.Add(product);
            await _dataContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> Get(int Id)
        {
            var product = await _dataContext.Products.FindAsync(Id);
            if (product == null)
            {
                return NotFound();
            } 
            else
                return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductForUpdateDto productDto)
        {
            if (id == 0)
            {
                return BadRequest("Invalid id"); // Return bad request if id is 0
            }

            var product = await _dataContext.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound(); // Return 404 Not Found if product with given id doesn't exist
            }

            // Update product properties from DTO
            product.Name = productDto.Name;
            product.Price = productDto.Price;
            product.Category = productDto.Category;
            product.Description = productDto.Description;
            product.Quantity = productDto.Quantity;
            product.Imageurl = productDto.Imageurl;

            await _dataContext.SaveChangesAsync(); // Save changes to the database

            return NoContent(); // Return 204 No Content upon successful update
        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _dataContext.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound(); 
            }

            _dataContext.Products.Remove(product); // Mark product for deletion
            await _dataContext.SaveChangesAsync(); 

            return NoContent(); // Return 204 No Content upon successful deletion
        }


    }
}
