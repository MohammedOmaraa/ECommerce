
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Domain.Entities.Identity
{
    public class Address
    {
        public int Id { get; set; }

        public string Street { get; set; } = default!;

        public string City { get; set; } = default!;

        public string Country { get; set; } = default!;

        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public ApplicationUser User { get; set; } = default!;

        [ForeignKey("User")]
        public string UserId { get; set; } = default!;
    }
}
