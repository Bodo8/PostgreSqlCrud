using System.ComponentModel.DataAnnotations;

namespace ChatService.Domain.Models
{
    public class Client
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
