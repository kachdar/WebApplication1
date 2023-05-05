namespace WebApplication1.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; } = "";
        public string? Address { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public List<Order>? Orders { get; set; } = new();
    }
}
