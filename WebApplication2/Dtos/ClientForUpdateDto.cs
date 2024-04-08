using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Dtos
{
    public class ClientForUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; } 
        public string County { get; set; }
        public string Subcounty { get; set; }  
        public string Ward { get; set; }
        public string Town { get; set; }
    }
}
