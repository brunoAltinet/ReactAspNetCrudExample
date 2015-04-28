using System.ComponentModel.DataAnnotations;
using AltiFinReact.Core.Framework;

namespace AltiFinReact.Core.Entities
{
    public class InvoiceItem:AuditEntity
    {

        public virtual Invoice Invoice { get; set; }

        public virtual int? InvoiceId { get; set; }

        public int? Ordinal { get; set; }

        [MaxLength(500)]
        public string Name { get; set; }

        public decimal? UnitPrice { get; set; }

        public decimal? Qty { get; set; }

        public decimal? Price { get; set; }
    }
}
