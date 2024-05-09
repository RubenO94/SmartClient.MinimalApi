using SmartClient.MinimalAPI.Core.DTO.Attachments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClient.MinimalAPI.Core.DTO.Contracts.ContractInvoices
{
    public class ContractInvoiceResponseDTO
    {
        public long InvoiceID { get; set; }
        public DateTime? InvoiceStart { get; set; }
        public DateTime? InvoiceEnd { get; set; }
        public decimal? TotalInvoiced { get; set; }
        public int UserID { get; set; }
        public string? InvoiceNumber { get; set; }
        public bool Invoiced { get; set; }
        public bool? Paid { get; set; }
        public DateTime? BillDate { get; set; }
        public string? ResumeFileUrl { get; set; }
        public AttachmentResponseDTO? File { get; set; }
    }
}
