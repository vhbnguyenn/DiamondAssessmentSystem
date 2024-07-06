using DiamondAssessmentSystem.Interfaces;
using DiamondAssessmentSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Repositories
{
    public class BookingDetailRepository : IBookingDetailRepository
    {
        private readonly DiamondAssessmentSystemContext _context;

        public BookingDetailRepository(DiamondAssessmentSystemContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookingDetail>> GetBookingDetailsAsync()
        {
            return await _context.BookingDetails
                .Include(bd => bd.ServicePrice)
                .Include(bd => bd.Result)
                .ToListAsync();
        }

        public async Task<BookingDetail> GetBookingDetailByIdAsync(int id)
        {
            return await _context.BookingDetails
                .Include(bd => bd.ServicePrice)
                .Include(bd => bd.Result)
                .FirstOrDefaultAsync(bd => bd.BookingDetailId == id);
        }

        public async Task<BookingDetail> CreateBookingDetailAsync(BookingDetail bookingDetail)
        {
            _context.BookingDetails.Add(bookingDetail);
            await _context.SaveChangesAsync();
            return bookingDetail;
        }

        public async Task<bool> UpdateBookingDetailAsync(BookingDetail bookingDetail)
        {
            _context.Entry(bookingDetail).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await BookingDetailExistsAsync(bookingDetail.BookingDetailId))
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<bool> DeleteBookingDetailAsync(int id)
        {
            var bookingDetail = await _context.BookingDetails.FindAsync(id);
            if (bookingDetail == null)
            {
                return false;
            }

            _context.BookingDetails.Remove(bookingDetail);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> BookingDetailExistsAsync(int id)
        {
            return await _context.BookingDetails.AnyAsync(e => e.BookingDetailId == id);
        }
    }
}
