using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Model
{
    public class Product
    {
        public int ProductId { get; set; }


       
        [MinLength(3)]
        public string Name { get; set; }

     
        [Range(10,10000, ErrorMessage ="Price should be between 10 to 100000 only.")]
        public decimal Price { get; set; }

        
        public string Category { get; set; }

       
        public string Description { get; set; }

        
        public int Quantity { get; set; }

  
        public string Imageurl { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
