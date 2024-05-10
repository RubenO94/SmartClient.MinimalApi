using SmartClient.MinimalAPI.Core.DTO.Attachments;
using SmartClient.MinimalAPI.Core.DTO.Clients;
using SmartClient.MinimalAPI.Core.DTO.Clients.PartnerClients;
using SmartClient.MinimalAPI.Core.DTO.Contracts.ContractInvoices;
using SmartClient.MinimalAPI.Core.DTO.Contracts.ContractLimits;
using SmartClient.MinimalAPI.Core.DTO.SmartUsers;
using SmartClientWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClient.MinimalAPI.Core.DTO.Contracts
{
    public static class ContractExtensions
    {
        public static ContractResponseDTO ToResponseDTO(this Contract contract)
        {
            return new ContractResponseDTO()
            {
                ContractID = contract.ContractID,
                Active = contract.Active,
                DaysBeforeExpirationAlert = contract.Notice,
                Start = contract.Start,
                End = contract.End,
                Renewal = contract.Renewal,
                Type = contract.Type,
                HasOverduePayments = contract.UnpaidFlag,
                IsInvoiced = contract.BilledFlag,
                Client = contract.Client.ToResponseDTO(),
                Invoices = contract.Invoices.Select(invoice => invoice.ToResponseDTO()).ToList(),
                Limits = contract.Limits.Select(limit => limit.ToResponseDTO()).ToList(),
                Notes = contract.Notes,
                NotifyEmails = contract.Notify,
                Requisition = contract.Requisition,
                Total = contract.Total,
                PartnerClient = contract.PartnerClient.ToResponseDTO(),
                SmartUser = contract.SmartUser.ToResponseDTO()

            };
        }


        public static ContractInvoiceResponseDTO ToResponseDTO(this ContractInvoice invoice)
        {
            return new ContractInvoiceResponseDTO()
            {
                InvoiceID = invoice.InvoiceID,
                Invoiced = invoice.Billed,
                Paid = invoice.Paid,
                UserID = invoice.UserID,
                BillDate = invoice.BillDate,
                File = invoice.AttachmentFile.ToResponseDTO(),
                InvoiceEnd = invoice.InvoiceEnd,
                InvoiceNumber = invoice.InvoiceNumber,
                InvoiceStart = invoice.InvoiceStart,
                ResumeFileUrl = invoice.ResumeFileUrl,
                TotalInvoiced = invoice.TotalBilled

            };
        }

        public static ContractLimitResponseDTO ToResponseDTO(this ContractLimit limit)
        {
            return new ContractLimitResponseDTO()
            {
                CurrentValue = limit.CurrentValue,
                LimitID = limit.LimitID,
                LimitType = limit.Type.ToResponseDTO(),
                OriginalValue = limit.OriginalValue,
            };
        }


        public static ContractLimitTypeResponseDTO ToResponseDTO(this LimitType limitType)
        {
            return new ContractLimitTypeResponseDTO()
            {
                Name = limitType.Name,
                Unit = limitType.Unit,
                Value = limitType.Value
            };
        }

    }
}
