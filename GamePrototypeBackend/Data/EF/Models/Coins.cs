namespace GamePrototypeBackend.Data.Models
{
    public class Coins
    {
        public int Id { get; set; }
        public int CountOfCoins { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
