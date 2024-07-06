using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DiamondAssessmentSystem.Interfaces;
using DiamondAssessmentSystem.Models;
using DiamondAssessmentSystem.DTOs;
using DiamondAssessmentSystem.BusinessObject.DTO;

namespace DiamondAssessmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IBookingDetailRepository _bookingDetailRepository;

        public BookingController(IBookingRepository bookingRepository, IBookingDetailRepository bookingDetailRepository)
        {
            _bookingRepository = bookingRepository;
            _bookingDetailRepository = bookingDetailRepository;
        }

        // GET: api/Booking
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetBookings()
        {
            var bookings = await _bookingRepository.GetBookingsAsync();
            var bookingDtos = bookings.Select(booking => new BookingDto
            {
                BookingId = booking.BookingId,
                BookingDate = booking.BookingDate,
                CustomerId = booking.CustomerId,
                Status = booking.Status,
                Quantity = booking.Quantity,
                BookingDetailId = booking.BookingDetailId,
                ConsultantId = booking.ConsultantId,
                ReceiptId = booking.ReceiptId,
                SealingId = booking.SealingId,
                CommitmentId = booking.CommitmentId,
                Customer = new CustomerDto
                {
                    CustomerId = booking.Customer.CustomerId,
                    Name = booking.Customer.Name,
                    Email = booking.Customer.Email,
                    Phone = booking.Customer.Phone,
                    Address = booking.Customer.Address
                },
                Commitment = booking.Commitment != null ? new FormDto
                {
                    FormId = booking.Commitment.FormId,
                    FormType = booking.Commitment.FormType,
                    CreateDate = booking.Commitment.CreateDate
                } : null,
                Consultant = booking.Consultant != null ? new StaffDto
                {
                    StaffId = booking.Consultant.StaffId,
                    Name = booking.Consultant.Name,
                    Email = booking.Consultant.Email,
                    Phone = booking.Consultant.Phone
                } : null,
                Receipt = booking.Receipt != null ? new FormDto
                {
                    FormId = booking.Receipt.FormId,
                    FormType = booking.Receipt.FormType,
                    CreateDate = booking.Receipt.CreateDate
                } : null,
                Sealing = booking.Sealing != null ? new FormDto
                {
                    FormId = booking.Sealing.FormId,
                    FormType = booking.Sealing.FormType,
                    CreateDate = booking.Sealing.CreateDate
                } : null
            }).ToList();
            return Ok(bookingDtos);
        }

        // GET: api/Booking/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDto>> GetBooking(int id)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            var bookingDto = new BookingDto
            {
                BookingId = booking.BookingId,
                BookingDate = booking.BookingDate,
                CustomerId = booking.CustomerId,
                Status = booking.Status,
                Quantity = booking.Quantity,
                BookingDetailId = booking.BookingDetailId,
                ConsultantId = booking.ConsultantId,
                ReceiptId = booking.ReceiptId,
                SealingId = booking.SealingId,
                CommitmentId = booking.CommitmentId,
                Customer = new CustomerDto
                {
                    CustomerId = booking.Customer.CustomerId,
                    Name = booking.Customer.Name,
                    Email = booking.Customer.Email,
                    Phone = booking.Customer.Phone,
                    Address = booking.Customer.Address
                },
                Commitment = booking.Commitment != null ? new FormDto
                {
                    FormId = booking.Commitment.FormId,
                    FormType = booking.Commitment.FormType,
                    CreateDate = booking.Commitment.CreateDate
                } : null,
                Consultant = booking.Consultant != null ? new StaffDto
                {
                    StaffId = booking.Consultant.StaffId,
                    Name = booking.Consultant.Name,
                    Email = booking.Consultant.Email,
                    Phone = booking.Consultant.Phone
                } : null,
                Receipt = booking.Receipt != null ? new FormDto
                {
                    FormId = booking.Receipt.FormId,
                    FormType = booking.Receipt.FormType,
                    CreateDate = booking.Receipt.CreateDate
                } : null,
                Sealing = booking.Sealing != null ? new FormDto
                {
                    FormId = booking.Sealing.FormId,
                    FormType = booking.Sealing.FormType,
                    CreateDate = booking.Sealing.CreateDate
                } : null
            };

            return Ok(bookingDto);
        }

        // POST: api/Booking
        // POST: api/Booking
        [HttpPost]
        public async Task<ActionResult<BookingDto>> PostBooking(BookingCreateDto bookingCreateDto)
        {
            var bookingDetailIds = bookingCreateDto.BookingDetailId.Split(',').Select(id => id.Trim()).Where(id => !string.IsNullOrEmpty(id)).Select(int.Parse).ToList();

            foreach (var bookingDetailId in bookingDetailIds)
            {
                var bookingDetail = await _bookingDetailRepository.GetBookingDetailByIdAsync(bookingDetailId);
                if (bookingDetail == null)
                {
                    return BadRequest($"BookingDetailId {bookingDetailId} is invalid.");
                }
            }

            var booking = new Booking
            {
                BookingDate = bookingCreateDto.BookingDate,
                CustomerId = bookingCreateDto.CustomerId,
                Status = bookingCreateDto.Status,
                Quantity = bookingCreateDto.Quantity,
                BookingDetailId = bookingCreateDto.BookingDetailId,
                ConsultantId = bookingCreateDto.ConsultantId,
                ReceiptId = bookingCreateDto.ReceiptId,
                SealingId = bookingCreateDto.SealingId,
                CommitmentId = bookingCreateDto.CommitmentId
            };

            var createdBooking = await _bookingRepository.CreateBookingAsync(booking);

            var createdBookingDto = new BookingDto
            {
                BookingId = createdBooking.BookingId,
                BookingDate = createdBooking.BookingDate,
                CustomerId = createdBooking.CustomerId,
                Status = createdBooking.Status,
                Quantity = createdBooking.Quantity,
                BookingDetailId = createdBooking.BookingDetailId,
                ConsultantId = createdBooking.ConsultantId,
                ReceiptId = createdBooking.ReceiptId,
                SealingId = createdBooking.SealingId,
                CommitmentId = createdBooking.CommitmentId,
                Customer = new CustomerDto
                {
                    CustomerId = createdBooking.Customer.CustomerId,
                    Name = createdBooking.Customer.Name,
                    Email = createdBooking.Customer.Email,
                    Phone = createdBooking.Customer.Phone,
                    IdCard = createdBooking.Customer.IdCard,
                    Address = createdBooking.Customer.Address,
                    UnitName = createdBooking.Customer.UnitName,
                    TaxCode = createdBooking.Customer.TaxCode
                },
                Commitment = createdBooking.Commitment != null ? new FormDto
                {
                    FormId = createdBooking.Commitment.FormId,
                    FormType = createdBooking.Commitment.FormType,
                    CreateDate = createdBooking.Commitment.CreateDate
                } : null,
                Consultant = createdBooking.Consultant != null ? new StaffDto
                {
                    StaffId = createdBooking.Consultant.StaffId,
                    Name = createdBooking.Consultant.Name,
                    Email = createdBooking.Consultant.Email,
                    Phone = createdBooking.Consultant.Phone
                } : null,
                Receipt = createdBooking.Receipt != null ? new FormDto
                {
                    FormId = createdBooking.Receipt.FormId,
                    FormType = createdBooking.Receipt.FormType,
                    CreateDate = createdBooking.Receipt.CreateDate
                } : null,
                Sealing = createdBooking.Sealing != null ? new FormDto
                {
                    FormId = createdBooking.Sealing.FormId,
                    FormType = createdBooking.Sealing.FormType,
                    CreateDate = createdBooking.Sealing.CreateDate
                } : null
            };

            return CreatedAtAction(nameof(GetBooking), new { id = createdBookingDto.BookingId }, createdBookingDto);
        }

        // PUT: api/Booking/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, BookingCreateDto bookingCreateDto)
        {
            var existingBooking = await _bookingRepository.GetBookingByIdAsync(id);

            if (existingBooking == null)
            {
                return NotFound();
            }

            existingBooking.BookingDate = bookingCreateDto.BookingDate;
            existingBooking.CustomerId = bookingCreateDto.CustomerId;
            existingBooking.Status = bookingCreateDto.Status;
            existingBooking.Quantity = bookingCreateDto.Quantity;
            existingBooking.BookingDetailId = bookingCreateDto.BookingDetailId;
            existingBooking.ConsultantId = bookingCreateDto.ConsultantId;
            existingBooking.ReceiptId = bookingCreateDto.ReceiptId;
            existingBooking.SealingId = bookingCreateDto.SealingId;
            existingBooking.CommitmentId = bookingCreateDto.CommitmentId;

            var bookingDetailIds = bookingCreateDto.BookingDetailId.Split(',').Select(int.Parse).ToList();

            foreach (var bookingDetailId in bookingDetailIds)
            {
                var bookingDetail = await _bookingDetailRepository.GetBookingDetailByIdAsync(bookingDetailId);
                if (bookingDetail == null)
                {
                    return BadRequest($"BookingDetailId {bookingDetailId} is invalid.");
                }
            }

            var updated = await _bookingRepository.UpdateBookingAsync(existingBooking);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Booking/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var deleted = await _bookingRepository.DeleteBookingAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
