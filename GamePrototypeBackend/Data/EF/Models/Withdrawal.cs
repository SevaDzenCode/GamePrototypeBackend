namespace GamePrototypeBackend.Data.Models
{
    public class Withdrawal
    {
        public int Id { get; set; }
        public string Wallet { get; set; }
        public int Sum { get; set; }
        public string Status { get; set; }
        
        public Balance Balance { get; set; }
        public int BalanceId { get; set; }
    }
}
