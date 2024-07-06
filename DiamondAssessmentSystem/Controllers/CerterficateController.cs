using Microsoft.AspNetCore.Mvc;
using DiamondAssessment.Interfaces;
using DiamondAssessmentSystem.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using DiamondAssessmentSystem.Interfaces;
using DiamondAssessmentSystem.BusinessObject.DTO;
using DiamondAssessmentSystem.Models;

namespace DiamondAssessmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        private readonly ICertificateRepository _certificateRepository;

        public CertificateController(ICertificateRepository certificateRepository)
        {
            _certificateRepository = certificateRepository;
        }

        // GET: api/Certificate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CertificateDto>>> GetCertificates()
        {
            var certificates = await _certificateRepository.GetCertificatesAsync();
            var certificateDtos = certificates.Select(certificate => new CertificateDto
            {
                CertId = certificate.CertId,
                IssueDate = certificate.IssueDate,
                Results = certificate.Results.Select(result => new ResultDto
                {
                    ResultId = result.ResultId,
                    AssessmentStaff = result.AssessmentStaff,
                    Origin = result.Origin,
                    Shape = result.Shape,
                    Measurement = result.Measurement,
                    CaratWeight = result.CaratWeight,
                    Color = result.Color,
                    Clarity = result.Clarity,
                    Cut = result.Cut,
                    Proportion = result.Proportion,
                    Polish = result.Polish,
                    Symmetry = result.Symmetry,
                    Fluorescence = result.Fluorescence,
                    AssessmentNote = result.AssessmentNote,
                    ManagerNote = result.ManagerNote,
                    IsAccepted = result.IsAccepted,
                    CertId = result.CertId
                }).ToList()
            }).ToList();
            return Ok(certificateDtos);
        }

        // GET: api/Certificate/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CertificateDto>> GetCertificate(int id)
        {
            var certificate = await _certificateRepository.GetCertificateByIdAsync(id);

            if (certificate == null)
            {
                return NotFound();
            }

            var certificateDto = new CertificateDto
            {
                CertId = certificate.CertId,
                IssueDate = certificate.IssueDate,
                Results = certificate.Results.Select(result => new ResultDto
                {
                    ResultId = result.ResultId,
                    AssessmentStaff = result.AssessmentStaff,
                    Origin = result.Origin,
                    Shape = result.Shape,
                    Measurement = result.Measurement,
                    CaratWeight = result.CaratWeight,
                    Color = result.Color,
                    Clarity = result.Clarity,
                    Cut = result.Cut,
                    Proportion = result.Proportion,
                    Polish = result.Polish,
                    Symmetry = result.Symmetry,
                    Fluorescence = result.Fluorescence,
                    AssessmentNote = result.AssessmentNote,
                    ManagerNote = result.ManagerNote,
                    IsAccepted = result.IsAccepted,
                    CertId = result.CertId
                }).ToList()
            };

            return Ok(certificateDto);
        }

        // POST: api/Certificate
        [HttpPost]
        public async Task<ActionResult<CertificateDto>> PostCertificate(CertificateCreateDto certificateCreateDto)
        {
            var certificate = new Certificate
            {
                IssueDate = certificateCreateDto.IssueDate.HasValue ? certificateCreateDto.IssueDate.Value : DateOnly.MinValue
            };

            var createdCertificate = await _certificateRepository.CreateCertificateAsync(certificate);

            var createdCertificateDto = new CertificateDto
            {
                CertId = createdCertificate.CertId,
                IssueDate = createdCertificate.IssueDate
            };

            return CreatedAtAction(nameof(GetCertificate), new { id = createdCertificateDto.CertId }, createdCertificateDto);
        }

        // PUT: api/Certificate/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCertificate(int id, CertificateCreateDto certificateCreateDto)
        {
            var existingCertificate = await _certificateRepository.GetCertificateByIdAsync(id);

            if (existingCertificate == null)
            {
                return NotFound();
            }

            existingCertificate.IssueDate = certificateCreateDto.IssueDate.HasValue ? certificateCreateDto.IssueDate.Value : existingCertificate.IssueDate;

            var updated = await _certificateRepository.UpdateCertificateAsync(existingCertificate);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Certificate/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCertificate(int id)
        {
            var deleted = await _certificateRepository.DeleteCertificateAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
