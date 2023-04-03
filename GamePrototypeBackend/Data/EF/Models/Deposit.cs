namespace GamePrototypeBackend.Data.Models
{
    public class Deposit
    {
        public int Id { get; set; }
        public string Wallet { get; set; }
        public decimal Sum { get; set; }

        public Balance Balance { get; set; }
        public int BalanceId { get; set; }
    }
}
