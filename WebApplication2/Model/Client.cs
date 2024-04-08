using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Model
{
    public class Client
    {
        public int ClientId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }


        [StringLength(50)]
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

       
        [StringLength(100)]
        public string County { get; set; }


        [StringLength(100)]
        public string Subcounty { get; set; }


        [StringLength(100)]
        public string Ward { get; set; }


        [StringLength(100)]
        public string Town { get; set; }


        public ICollection<Order> Orders { get; set; }
    }
}
