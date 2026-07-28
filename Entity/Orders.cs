namespace MyFristProject.Entity
{
    public class Orders
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = new User();
        public int ProductId { get; set; }
        public Prodect Product { get; set; } = new Prodect();
        public DateTime OrderDate { get; set; }
    }
}