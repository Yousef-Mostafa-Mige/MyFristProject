using MyFristProject.Dots;

namespace MyFristProject.Entity
{
    public class Orders
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = new User();
        public int ProdectId { get; set; }
        public Prodect Product { get; set; } = new Prodect();
        public DateTime time { get; set; } = DateTime.Now;
    }
}