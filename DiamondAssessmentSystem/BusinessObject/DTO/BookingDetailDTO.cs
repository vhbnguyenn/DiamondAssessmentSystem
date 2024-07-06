using DiamondAssessmentSystem.BusinessObject.DTO;

public class BookingDetailDto
{
    public int BookingDetailId { get; set; }
    public int ServicePriceId { get; set; }
    public int? ResultId { get; set; }
    public bool? IsAccepted { get; set; }
    public ServicePriceDto ServicePrice { get; set; }
    public ResultDto? Result { get; set; }
}

public class BookingDetailCreateDto
{
    public int ServicePriceId { get; set; }
    public int? ResultId { get; set; }
    public bool? IsAccepted { get; set; }
}
