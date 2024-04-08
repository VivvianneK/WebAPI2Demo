using WebApplication2.Model;

namespace WebApplication2.Dtos
{
    public class OrderForUpdate
    {

        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int ClientId { get; set; }

    }
}
