namespace GamePrototypeBackend.Data.Models
{
    public class Settings
    {
        public int Id { get; set; }
        public decimal? MinWithdrawal { get; set; }
        public decimal? MinDeposit{ get; set; }
        public decimal? SumFromRefferals { get; set; }
    }
}
