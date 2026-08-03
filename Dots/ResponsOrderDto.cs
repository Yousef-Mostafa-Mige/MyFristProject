namespace MyFristProject.Dots
{
    public class ResponsorderDto
    {
        public int UserId{get; set;}
        public required UserDot User{get; set;}
        public required ProdectDto Prodect{get; set;}
        public DateTime time{get; set;}=DateTime.Now;

    }
}