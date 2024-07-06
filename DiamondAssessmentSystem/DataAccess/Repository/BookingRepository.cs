using DiamondAssessmentSystem.Interfaces;
using DiamondAssessmentSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly DiamondAssessmentSystemContext _context;

        public BookingRepository(DiamondAssessmentSystemContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Booking>> GetBookingsAsync()
        {
            return await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Commitment)
                .Include(b => b.Consultant)
                .Include(b => b.Receipt)
                .Include(b => b.Sealing)
                .ToListAsync();
        }

        public async Task<Booking> GetBookingByIdAsync(int id)
        {
            return await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Commitment)
                .Include(b => b.Consultant)
                .Include(b => b.Receipt)
                .Include(b => b.Sealing)
                .FirstOrDefaultAsync(b => b.BookingId == id);
        }

        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<bool> UpdateBookingAsync(Booking booking)
        {
            _context.Entry(booking).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await BookingExistsAsync(booking.BookingId))
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<bool> DeleteBookingAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return false;
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> BookingExistsAsync(int id)
        {
            return await _context.Bookings.AnyAsync(e => e.BookingId == id);
        }
    }
}
