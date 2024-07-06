using DiamondAssessmentSystem.BusinessObject.DTO;
using System;
using System.Collections.Generic;

namespace DiamondAssessmentSystem.DTOs
{
    public class BookingDto
    {
        public int BookingId { get; set; }
        public DateOnly BookingDate { get; set; }
        public int CustomerId { get; set; }
        public string Status { get; set; }
        public int Quantity { get; set; }
        public string? BookingDetailId { get; set; }
        public int? ConsultantId { get; set; }
        public int? ReceiptId { get; set; }
        public int? SealingId { get; set; }
        public int? CommitmentId { get; set; }
        public CustomerDto Customer { get; set; }
        public FormDto? Commitment { get; set; }
        public StaffDto? Consultant { get; set; }
        public FormDto? Receipt { get; set; }
        public FormDto? Sealing { get; set; }
    }


    public class BookingCreateDto
    {
        public DateOnly BookingDate { get; set; }
        public int CustomerId { get; set; }
        public string Status { get; set; }
        public int Quantity { get; set; }
        public string BookingDetailId { get; set; } // This should be a comma-separated string of BookingDetail IDs
        public int? ConsultantId { get; set; }
        public int? ReceiptId { get; set; }
        public int? SealingId { get; set; }
        public int? CommitmentId { get; set; }
    }
}
