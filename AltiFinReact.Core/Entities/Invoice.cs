using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AltiFinReact.Core.Framework;

namespace AltiFinReact.Core.Entities
{
    public class Invoice:AuditEntity
    {
        
        [MaxLength(100)]
        public string Code { get; set; }

        public int? Ordinal { get; set; }

        [MaxLength(100)]
        public string OrderNumber { get; set; }

        public Partner Partner { get; set; }

        public int? PartnerId { get; set; }

        public DateTime? Date { get; set; }

        public decimal? NettoValue { get; set; }

        public decimal? TaxValue { get; set; }

        public decimal? BruttoValue { get; set; }

        public decimal? DiscountPct { get; set; }

        public DateTime? PaymentDeadline { get; set; }

        [MaxLength(500)]
        public string Note { get; set; }

        public ICollection<InvoiceItem> InvoiceItems { get; set; } 
    }
}
