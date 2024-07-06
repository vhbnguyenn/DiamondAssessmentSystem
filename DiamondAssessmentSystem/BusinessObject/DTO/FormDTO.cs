using DiamondAssessmentSystem.DTOs;

namespace DiamondAssessmentSystem.BusinessObject.DTO
{
    public class FormDto
    {
        public int FormId { get; set; }
        public string FormType { get; set; }
        public DateOnly CreateDate { get; set; }
        public ICollection<BookingDto> BookingCommitments { get; set; }
        public ICollection<BookingDto> BookingReceipts { get; set; }
        public ICollection<BookingDto> BookingSealings { get; set; }
    }

    public class FormCreateDto
    {
        public string FormType { get; set; }
        public DateOnly CreateDate { get; set; }
    }
}
