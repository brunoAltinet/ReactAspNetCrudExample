using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltiFinReact.Web.Models
{
    public class InvoiceDto
    {
        public int? Id { get; set; }
        public int? Ordinal { get; set; }
        public string PartnerName { get; set; }
    }
}