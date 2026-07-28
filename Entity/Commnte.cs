namespace MyFristProject.Entity
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int UserId { get; set; }
        public User User { get; set; } = new User();
        public int ProductId { get; set; }
        public Prodect Product { get; set; } = new Prodect();
    }
}