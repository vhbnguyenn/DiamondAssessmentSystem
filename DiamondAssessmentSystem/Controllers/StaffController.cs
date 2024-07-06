using Microsoft.AspNetCore.Mvc;
using DiamondAssessment.Interfaces;
using DiamondAssessmentSystem.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using DiamondAssessmentSystem.BusinessObject.DTO;
using DiamondAssessmentSystem.Models;

namespace DiamondAssessmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffRepository _staffRepository;

        public StaffController(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        // GET: api/Staff
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffDto>>> GetStaffs()
        {
            var staffs = await _staffRepository.GetStaffsAsync();
            var staffDtos = staffs.Select(staff => new StaffDto
            {
                StaffId = staff.StaffId,
                AccId = staff.AccId,
                Name = staff.Name,
                Email = staff.Email,
                Phone = staff.Phone
            }).ToList();
            return Ok(staffDtos);
        }

        // GET: api/Staff/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StaffDto>> GetStaff(int id)
        {
            var staff = await _staffRepository.GetStaffByIdAsync(id);

            if (staff == null)
            {
                return NotFound();
            }

            var staffDto = new StaffDto
            {
                StaffId = staff.StaffId,
                AccId = staff.AccId,
                Name = staff.Name,
                Email = staff.Email,
                Phone = staff.Phone
            };

            return Ok(staffDto);
        }

        // POST: api/Staff
        [HttpPost]
        public async Task<ActionResult<StaffDto>> PostStaff(StaffDto staffDto)
        {
            var staff = new Staff
            {
                AccId = staffDto.AccId,
                Name = staffDto.Name,
                Email = staffDto.Email,
                Phone = staffDto.Phone
            };

            var createdStaff = await _staffRepository.CreateStaffAsync(staff);
            var createdStaffDto = new StaffDto
            {
                StaffId = createdStaff.StaffId,
                AccId = createdStaff.AccId,
                Name = createdStaff.Name,
                Email = createdStaff.Email,
                Phone = createdStaff.Phone
            };

            return CreatedAtAction(nameof(GetStaff), new { id = createdStaffDto.StaffId }, createdStaffDto);
        }

        // PUT: api/Staff/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStaff(int id, StaffDto staffDto)
        {
            if (id != staffDto.StaffId)
            {
                return BadRequest();
            }

            var staff = new Staff
            {
                StaffId = staffDto.StaffId,
                AccId = staffDto.AccId,
                Name = staffDto.Name,
                Email = staffDto.Email,
                Phone = staffDto.Phone
            };

            var updated = await _staffRepository.UpdateStaffAsync(staff);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Staff/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            var deleted = await _staffRepository.DeleteStaffAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
