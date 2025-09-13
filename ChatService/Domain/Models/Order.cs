using ChatService.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatService.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Column(TypeName = "numeric(10,2)")]
        public decimal Amount { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdDate { get; set; }
        public Status Status { get; set; }
        public bool IsPaid { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; } = null!;
    }
}
