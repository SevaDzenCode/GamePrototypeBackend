namespace GamePrototypeBackend.Data.Models
{
    public class CoinsTransfer
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ValueOfCoins { get; set; }

        public Balance Balance { get; set; }
        public int BalanceId { get; set; }

        public Exchanger Exchangers { get; set; }
        public int ExchangerId { get; set; }
    }
}
