using ChatService.Domain.Enums;

namespace ChatService.Domain.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdDate { get; set; }
        public Status Status { get; set; }
        public bool IsPaid { get; set; }
    }
}
