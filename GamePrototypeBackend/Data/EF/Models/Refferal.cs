using System.ComponentModel.DataAnnotations.Schema;

namespace GamePrototypeBackend.Data.Models
{
    //TODO added foreign key to Users
    [NotMapped]
    public class Refferal
    {
        public int Id { get; set; }
        public int UserIdFrom { get; set; }
        public int UserIdTo { get; set;}
        public bool isPaid { get; set; }
    }
}
