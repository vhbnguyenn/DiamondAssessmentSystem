using DiamondAssessmentSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Interfaces
{
    public interface IBookingDetailRepository
    {
        Task<IEnumerable<BookingDetail>> GetBookingDetailsAsync();
        Task<BookingDetail> GetBookingDetailByIdAsync(int id);
        Task<BookingDetail> CreateBookingDetailAsync(BookingDetail bookingDetail);
        Task<bool> UpdateBookingDetailAsync(BookingDetail bookingDetail);
        Task<bool> DeleteBookingDetailAsync(int id);
    }
}
