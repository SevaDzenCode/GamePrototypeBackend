using System.ComponentModel.DataAnnotations.Schema;

namespace GamePrototypeBackend.Data.Models
{
    public class Balance
    {
        public int Id { get; set; }
        public decimal balance { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public List<CoinsTransfer>? CoinsTransfers { get; set; }
        public List<Withdrawal>? Withdrawal { get; set; }

        public List<Deposit>? Deposit { get; set; }

        //public CoinsTransfer CoinsTransfer { get; set; } 
    }
}
