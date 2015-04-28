using System.ComponentModel.DataAnnotations;
using AltiFinReact.Core.Framework;

namespace AltiFinReact.Core.Entities
{
    public class Partner:AuditEntity
    {
        [MaxLength(500)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        [MaxLength(500)]
        public string Address { get; set; }

        [MaxLength(50)]
        public string Oib { get; set; }

        [MaxLength(100)]
        public string ContactPerson { get; set; }

        [MaxLength(50)]
        public string AccountNumber { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(100)]
        public string Country { get; set; }

    }
}
