using DiamondAssessmentSystem.BusinessObject.DTO;
using DiamondAssessmentSystem.DTOs;
using DiamondAssessmentSystem.Interfaces;
using DiamondAssessmentSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private readonly IFormRepository _formRepository;

        public FormController(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        // GET: api/Form
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FormDto>>> GetForms()
        {
            var forms = await _formRepository.GetFormsAsync();
            var formDtos = forms.Select(form => new FormDto
            {
                FormId = form.FormId,
                FormType = form.FormType,
                CreateDate = form.CreateDate,
                BookingCommitments = form.BookingCommitments.Select(b => new BookingDto
                {
                    BookingId = b.BookingId,
                    BookingDate = b.BookingDate,
                    CustomerId = b.CustomerId,
                    Status = b.Status,
                    Quantity = b.Quantity,
                    BookingDetailId = b.BookingDetailId,
                    ConsultantId = b.ConsultantId,
                    ReceiptId = b.ReceiptId,
                    SealingId = b.SealingId,
                    CommitmentId = b.CommitmentId
                }).ToList(),
                BookingReceipts = form.BookingReceipts.Select(b => new BookingDto
                {
                    BookingId = b.BookingId,
                    BookingDate = b.BookingDate,
                    CustomerId = b.CustomerId,
                    Status = b.Status,
                    Quantity = b.Quantity,
                    BookingDetailId = b.BookingDetailId,
                    ConsultantId = b.ConsultantId,
                    ReceiptId = b.ReceiptId,
                    SealingId = b.SealingId,
                    CommitmentId = b.CommitmentId
                }).ToList(),
                BookingSealings = form.BookingSealings.Select(b => new BookingDto
                {
                    BookingId = b.BookingId,
                    BookingDate = b.BookingDate,
                    CustomerId = b.CustomerId,
                    Status = b.Status,
                    Quantity = b.Quantity,
                    BookingDetailId = b.BookingDetailId,
                    ConsultantId = b.ConsultantId,
                    ReceiptId = b.ReceiptId,
                    SealingId = b.SealingId,
                    CommitmentId = b.CommitmentId
                }).ToList()
            }).ToList();

            return Ok(formDtos);
        }

        // GET: api/Form/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FormDto>> GetForm(int id)
        {
            var form = await _formRepository.GetFormByIdAsync(id);

            if (form == null)
            {
                return NotFound();
            }

            var formDto = new FormDto
            {
                FormId = form.FormId,
                FormType = form.FormType,
                CreateDate = form.CreateDate,
                BookingCommitments = form.BookingCommitments.Select(b => new BookingDto
                {
                    BookingId = b.BookingId,
                    BookingDate = b.BookingDate,
                    CustomerId = b.CustomerId,
                    Status = b.Status,
                    Quantity = b.Quantity,
                    BookingDetailId = b.BookingDetailId,
                    ConsultantId = b.ConsultantId,
                    ReceiptId = b.ReceiptId,
                    SealingId = b.SealingId,
                    CommitmentId = b.CommitmentId
                }).ToList(),
                BookingReceipts = form.BookingReceipts.Select(b => new BookingDto
                {
                    BookingId = b.BookingId,
                    BookingDate = b.BookingDate,
                    CustomerId = b.CustomerId,
                    Status = b.Status,
                    Quantity = b.Quantity,
                    BookingDetailId = b.BookingDetailId,
                    ConsultantId = b.ConsultantId,
                    ReceiptId = b.ReceiptId,
                    SealingId = b.SealingId,
                    CommitmentId = b.CommitmentId
                }).ToList(),
                BookingSealings = form.BookingSealings.Select(b => new BookingDto
                {
                    BookingId = b.BookingId,
                    BookingDate = b.BookingDate,
                    CustomerId = b.CustomerId,
                    Status = b.Status,
                    Quantity = b.Quantity,
                    BookingDetailId = b.BookingDetailId,
                    ConsultantId = b.ConsultantId,
                    ReceiptId = b.ReceiptId,
                    SealingId = b.SealingId,
                    CommitmentId = b.CommitmentId
                }).ToList()
            };

            return Ok(formDto);
        }

        // POST: api/Form
        [HttpPost]
        public async Task<ActionResult<FormDto>> PostForm(FormCreateDto formCreateDto)
        {
            var form = new Form
            {
                FormType = formCreateDto.FormType,
                CreateDate = formCreateDto.CreateDate
            };

            var createdForm = await _formRepository.CreateFormAsync(form);

            var createdFormDto = new FormDto
            {
                FormId = createdForm.FormId,
                FormType = createdForm.FormType,
                CreateDate = createdForm.CreateDate
            };

            return CreatedAtAction(nameof(GetForm), new { id = createdFormDto.FormId }, createdFormDto);
        }

        // PUT: api/Form/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutForm(int id, FormCreateDto formCreateDto)
        {
            var existingForm = await _formRepository.GetFormByIdAsync(id);

            if (existingForm == null)
            {
                return NotFound();
            }

            existingForm.FormType = formCreateDto.FormType;
            existingForm.CreateDate = formCreateDto.CreateDate;

            var updated = await _formRepository.UpdateFormAsync(existingForm);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Form/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteForm(int id)
        {
            var deleted = await _formRepository.DeleteFormAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
