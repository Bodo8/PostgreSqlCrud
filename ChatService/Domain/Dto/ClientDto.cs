
namespace ChatService.Domain.Dto
{
    public class ClientDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string City { get; set; }

        public IList<OrderDto> Orders { get; set; } = new List<OrderDto>();
    }
}
