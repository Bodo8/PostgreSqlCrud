using ChatService.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatService.Domain.Models
{
    public class Order
    {
        private DateTime _createdDate;
        private DateTime _updDate;

        public int Id { get; set; }

        [Column(TypeName = "numeric(10,2)")]
        public decimal Amount { get; set; }

        [Column(TypeName = "timestamp without time zone")]
        public DateTime CreatedDate {
            get => _createdDate;
            set => _createdDate = DateTime.SpecifyKind(value, DateTimeKind.Unspecified);
        }

        [Column(TypeName = "timestamp without time zone")]
        public DateTime UpdDate {
            get => _updDate;
            set => _updDate = DateTime.SpecifyKind(value, DateTimeKind.Unspecified);
        }
        public Status Status { get; set; }
        public bool IsPaid { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; } = null!;
    }
}
