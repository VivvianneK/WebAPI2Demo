using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Dtos;
using WebApplication2.Model;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public ClientController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        public async Task<IEnumerable<Client>> Get()
        {
            return await _dataContext.Clients.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(ClientForPost clientForPost)
        {
            var client = new Client()
            {
                FirstName = clientForPost.FirstName,
                LastName = clientForPost.LastName,
                PhoneNumber = clientForPost.PhoneNumber,
                Email = clientForPost.Email,
                County = clientForPost.County,
                Subcounty = clientForPost.Subcounty,
                Ward = clientForPost.Ward,
                Town = clientForPost.Town
            };

            _dataContext.Clients.Add(client);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> Get(int Id)
        {

            var client = await _dataContext.Clients.FindAsync(Id);
            if (client == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(client);
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(int Id,ClientForUpdateDto clientDto) 
        {
            if (Id == 0)
            {
                return BadRequest("Invalid Id.");
            }
            var client = await _dataContext.Clients.FindAsync(Id);
            if (client == null)
            {
                return NotFound();
            }

            // Update product properties from DTO
            client.FirstName = clientDto.FirstName;
            client.LastName = clientDto.LastName;
            client.PhoneNumber = clientDto.PhoneNumber;
            client.Email = clientDto.Email;
            client.County = clientDto.County;
            client.Subcounty = clientDto.Subcounty;
            client.Ward = clientDto.Ward;
            client.Town = clientDto.Town;

            await _dataContext.SaveChangesAsync(); // Save changes to the database

            return NoContent(); // Return 204 No Content upon successful update

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var client= await _dataContext.Clients.FindAsync(Id);

            if (client == null)
            {
                return NotFound();
            }

            _dataContext.Clients.Remove(client); // Mark client for deletion
            await _dataContext.SaveChangesAsync();

            return NoContent(); // Return 204 No Content upon successful deletion
        }


    }
}
