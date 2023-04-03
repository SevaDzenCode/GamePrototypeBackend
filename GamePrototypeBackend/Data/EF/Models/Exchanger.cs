namespace GamePrototypeBackend.Data.Models
{
    public class Exchanger
    {
        public int Id { get; set; }
        public int CountOfCoins { get; set; }
        public decimal CoinsCost { get; set; }
        public DateTime ActualData { get; set; }
        
        public List<CoinsTransfer>? CoinsTransfer { get; set; }
    }
}
