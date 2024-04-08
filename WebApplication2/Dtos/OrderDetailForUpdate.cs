namespace WebApplication2.Dtos
{
    public class OrderDetailForUpdate
    {
        public int OrderDetailsId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
