using Microsoft.AspNetCore.Mvc;
using DiamondAssessment.Interfaces;
using DiamondAssessmentSystem.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using DiamondAssessmentSystem.BusinessObject.DTO;
using DiamondAssessmentSystem.Models;

namespace DiamondAssessmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly IResultRepository _resultRepository;

        public ResultController(IResultRepository resultRepository)
        {
            _resultRepository = resultRepository;
        }

        // GET: api/Result
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResultDto>>> GetResults()
        {
            var results = await _resultRepository.GetResultsAsync();
            var resultDtos = results.Select(result => new ResultDto
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
            }).ToList();
            return Ok(resultDtos);
        }

        // GET: api/Result/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResultDto>> GetResult(int id)
        {
            var result = await _resultRepository.GetResultByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            var resultDto = new ResultDto
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
            };

            return Ok(resultDto);
        }

        // POST: api/Result
        [HttpPost]
        public async Task<ActionResult<ResultDto>> PostResult(ResultCreateDto resultCreateDto)
        {
            var result = new Result
            {
                AssessmentStaff = resultCreateDto.AssessmentStaff,
                Origin = resultCreateDto.Origin,
                Shape = resultCreateDto.Shape,
                Measurement = resultCreateDto.Measurement,
                CaratWeight = resultCreateDto.CaratWeight,
                Color = resultCreateDto.Color,
                Clarity = resultCreateDto.Clarity,
                Cut = resultCreateDto.Cut,
                Proportion = resultCreateDto.Proportion,
                Polish = resultCreateDto.Polish,
                Symmetry = resultCreateDto.Symmetry,
                Fluorescence = resultCreateDto.Fluorescence,
                AssessmentNote = resultCreateDto.AssessmentNote,
                ManagerNote = resultCreateDto.ManagerNote,
                IsAccepted = resultCreateDto.IsAccepted,
                CertId = resultCreateDto.CertId
            };

            var createdResult = await _resultRepository.CreateResultAsync(result);
            var createdResultDto = new ResultDto
            {
                ResultId = createdResult.ResultId,
                AssessmentStaff = createdResult.AssessmentStaff,
                Origin = createdResult.Origin,
                Shape = createdResult.Shape,
                Measurement = createdResult.Measurement,
                CaratWeight = createdResult.CaratWeight,
                Color = createdResult.Color,
                Clarity = createdResult.Clarity,
                Cut = createdResult.Cut,
                Proportion = createdResult.Proportion,
                Polish = createdResult.Polish,
                Symmetry = createdResult.Symmetry,
                Fluorescence = createdResult.Fluorescence,
                AssessmentNote = createdResult.AssessmentNote,
                ManagerNote = createdResult.ManagerNote,
                IsAccepted = createdResult.IsAccepted,
                CertId = createdResult.CertId
            };

            return CreatedAtAction(nameof(GetResult), new { id = createdResultDto.ResultId }, createdResultDto);
        }

        // PUT: api/Result/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResult(int id, ResultCreateDto resultCreateDto)
        {
            var existingResult = await _resultRepository.GetResultByIdAsync(id);

            if (existingResult == null)
            {
                return NotFound();
            }

            existingResult.AssessmentStaff = resultCreateDto.AssessmentStaff;
            existingResult.Origin = resultCreateDto.Origin;
            existingResult.Shape = resultCreateDto.Shape;
            existingResult.Measurement = resultCreateDto.Measurement;
            existingResult.CaratWeight = resultCreateDto.CaratWeight;
            existingResult.Color = resultCreateDto.Color;
            existingResult.Clarity = resultCreateDto.Clarity;
            existingResult.Cut = resultCreateDto.Cut;
            existingResult.Proportion = resultCreateDto.Proportion;
            existingResult.Polish = resultCreateDto.Polish;
            existingResult.Symmetry = resultCreateDto.Symmetry;
            existingResult.Fluorescence = resultCreateDto.Fluorescence;
            existingResult.AssessmentNote = resultCreateDto.AssessmentNote;
            existingResult.ManagerNote = resultCreateDto.ManagerNote;
            existingResult.IsAccepted = resultCreateDto.IsAccepted;
            existingResult.CertId = resultCreateDto.CertId;

            var updated = await _resultRepository.UpdateResultAsync(existingResult);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Result/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResult(int id)
        {
            var deleted = await _resultRepository.DeleteResultAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
