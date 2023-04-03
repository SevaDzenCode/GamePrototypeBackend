namespace GamePrototypeBackend.Data.Repository.Models
{
    public class ExchangerModel 
    {
        public int Id { get; set; }
        public int CountOfCoins { get; set; }
        public int CoinsCost { get; set; }
        public DateTime? ActualData { get; set; }
    }
}
