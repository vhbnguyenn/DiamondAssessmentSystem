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
    public class BookingDetailController : ControllerBase
    {
        private readonly IBookingDetailRepository _bookingDetailRepository;

        public BookingDetailController(IBookingDetailRepository bookingDetailRepository)
        {
            _bookingDetailRepository = bookingDetailRepository;
        }

        // GET: api/BookingDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDetailDto>>> GetBookingDetails()
        {
            var bookingDetails = await _bookingDetailRepository.GetBookingDetailsAsync();
            var bookingDetailDtos = bookingDetails.Select(bookingDetail => new BookingDetailDto
            {
                BookingDetailId = bookingDetail.BookingDetailId,
                ServicePriceId = bookingDetail.ServicePriceId,
                ResultId = bookingDetail.ResultId,
                IsAccepted = bookingDetail.IsAccepted,
                ServicePrice = new ServicePriceDto
                {
                    ServicePriceId = bookingDetail.ServicePrice.ServicePriceId,
                    ServiceType = bookingDetail.ServicePrice.ServiceType,
                    Price = bookingDetail.ServicePrice.Price,
                    Description = bookingDetail.ServicePrice.Description,
                    Duration = bookingDetail.ServicePrice.Duration
                },
                Result = bookingDetail.Result != null ? new ResultDto
                {
                    ResultId = bookingDetail.Result.ResultId,
                    AssessmentStaff = bookingDetail.Result.AssessmentStaff,
                    Origin = bookingDetail.Result.Origin,
                    Shape = bookingDetail.Result.Shape,
                    Measurement = bookingDetail.Result.Measurement,
                    CaratWeight = bookingDetail.Result.CaratWeight,
                    Color = bookingDetail.Result.Color,
                    Clarity = bookingDetail.Result.Clarity,
                    Cut = bookingDetail.Result.Cut,
                    Proportion = bookingDetail.Result.Proportion,
                    Polish = bookingDetail.Result.Polish,
                    Symmetry = bookingDetail.Result.Symmetry,
                    Fluorescence = bookingDetail.Result.Fluorescence,
                    AssessmentNote = bookingDetail.Result.AssessmentNote,
                    ManagerNote = bookingDetail.Result.ManagerNote,
                    IsAccepted = bookingDetail.Result.IsAccepted,
                    CertId = bookingDetail.Result.CertId
                } : null
            }).ToList();

            return Ok(bookingDetailDtos);
        }

        // GET: api/BookingDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDetailDto>> GetBookingDetail(int id)
        {
            var bookingDetail = await _bookingDetailRepository.GetBookingDetailByIdAsync(id);

            if (bookingDetail == null)
            {
                return NotFound();
            }

            var bookingDetailDto = new BookingDetailDto
            {
                BookingDetailId = bookingDetail.BookingDetailId,
                ServicePriceId = bookingDetail.ServicePriceId,
                ResultId = bookingDetail.ResultId,
                IsAccepted = bookingDetail.IsAccepted,
                ServicePrice = new ServicePriceDto
                {
                    ServicePriceId = bookingDetail.ServicePrice.ServicePriceId,
                    ServiceType = bookingDetail.ServicePrice.ServiceType,
                    Price = bookingDetail.ServicePrice.Price,
                    Description = bookingDetail.ServicePrice.Description,
                    Duration = bookingDetail.ServicePrice.Duration
                },
                Result = bookingDetail.Result != null ? new ResultDto
                {
                    ResultId = bookingDetail.Result.ResultId,
                    AssessmentStaff = bookingDetail.Result.AssessmentStaff,
                    Origin = bookingDetail.Result.Origin,
                    Shape = bookingDetail.Result.Shape,
                    Measurement = bookingDetail.Result.Measurement,
                    CaratWeight = bookingDetail.Result.CaratWeight,
                    Color = bookingDetail.Result.Color,
                    Clarity = bookingDetail.Result.Clarity,
                    Cut = bookingDetail.Result.Cut,
                    Proportion = bookingDetail.Result.Proportion,
                    Polish = bookingDetail.Result.Polish,
                    Symmetry = bookingDetail.Result.Symmetry,
                    Fluorescence = bookingDetail.Result.Fluorescence,
                    AssessmentNote = bookingDetail.Result.AssessmentNote,
                    ManagerNote = bookingDetail.Result.ManagerNote,
                    IsAccepted = bookingDetail.Result.IsAccepted,
                    CertId = bookingDetail.Result.CertId
                } : null
            };

            return Ok(bookingDetailDto);
        }

        // POST: api/BookingDetail
        [HttpPost]
        public async Task<ActionResult<BookingDetailDto>> PostBookingDetail(BookingDetailCreateDto bookingDetailCreateDto)
        {
            var bookingDetail = new BookingDetail
            {
                ServicePriceId = bookingDetailCreateDto.ServicePriceId,
                ResultId = bookingDetailCreateDto.ResultId,
                IsAccepted = bookingDetailCreateDto.IsAccepted
            };

            var createdBookingDetail = await _bookingDetailRepository.CreateBookingDetailAsync(bookingDetail);

            var createdBookingDetailDto = new BookingDetailDto
            {
                BookingDetailId = createdBookingDetail.BookingDetailId,
                ServicePriceId = createdBookingDetail.ServicePriceId,
                ResultId = createdBookingDetail.ResultId,
                IsAccepted = createdBookingDetail.IsAccepted,
                ServicePrice = new ServicePriceDto
                {
                    ServicePriceId = createdBookingDetail.ServicePrice.ServicePriceId,
                    ServiceType = createdBookingDetail.ServicePrice.ServiceType,
                    Price = createdBookingDetail.ServicePrice.Price,
                    Description = createdBookingDetail.ServicePrice.Description,
                    Duration = createdBookingDetail.ServicePrice.Duration
                },
                Result = createdBookingDetail.Result != null ? new ResultDto
                {
                    ResultId = createdBookingDetail.Result.ResultId,
                    AssessmentStaff = createdBookingDetail.Result.AssessmentStaff,
                    Origin = createdBookingDetail.Result.Origin,
                    Shape = createdBookingDetail.Result.Shape,
                    Measurement = createdBookingDetail.Result.Measurement,
                    CaratWeight = createdBookingDetail.Result.CaratWeight,
                    Color = createdBookingDetail.Result.Color,
                    Clarity = createdBookingDetail.Result.Clarity,
                    Cut = createdBookingDetail.Result.Cut,
                    Proportion = createdBookingDetail.Result.Proportion,
                    Polish = createdBookingDetail.Result.Polish,
                    Symmetry = createdBookingDetail.Result.Symmetry,
                    Fluorescence = createdBookingDetail.Result.Fluorescence,
                    AssessmentNote = createdBookingDetail.Result.AssessmentNote,
                    ManagerNote = createdBookingDetail.Result.ManagerNote,
                    IsAccepted = createdBookingDetail.Result.IsAccepted,
                    CertId = createdBookingDetail.Result.CertId
                } : null
            };

            return CreatedAtAction(nameof(GetBookingDetail), new { id = createdBookingDetailDto.BookingDetailId }, createdBookingDetailDto);
        }

        // PUT: api/BookingDetail/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookingDetail(int id, BookingDetailCreateDto bookingDetailCreateDto)
        {
            var existingBookingDetail = await _bookingDetailRepository.GetBookingDetailByIdAsync(id);

            if (existingBookingDetail == null)
            {
                return NotFound();
            }

            existingBookingDetail.ServicePriceId = bookingDetailCreateDto.ServicePriceId;
            existingBookingDetail.ResultId = bookingDetailCreateDto.ResultId;
            existingBookingDetail.IsAccepted = bookingDetailCreateDto.IsAccepted;

            var updated = await _bookingDetailRepository.UpdateBookingDetailAsync(existingBookingDetail);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/BookingDetail/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingDetail(int id)
        {
            var deleted = await _bookingDetailRepository.DeleteBookingDetailAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
