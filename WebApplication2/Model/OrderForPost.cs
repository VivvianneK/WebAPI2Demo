namespace WebApplication2.Model
{
    public class OrderForPost
    {

        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int ClientId { get; set; }
        public List<OrderDetailForPost> OrderDetails { get; set; }
    }
}
