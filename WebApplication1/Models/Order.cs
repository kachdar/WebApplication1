namespace WebApplication1.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public State State { get; set; } = State.New;

        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public List<Book>? Books { get; set; } = new();
    }
    public enum State
    { 
        New,
        Accepted,
        Done,
        Canceled,
    }
}
