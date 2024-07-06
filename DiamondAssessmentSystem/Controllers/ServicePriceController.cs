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
    public class ServicePriceController : ControllerBase
    {
        private readonly IServicePriceRepository _servicePriceRepository;

        public ServicePriceController(IServicePriceRepository servicePriceRepository)
        {
            _servicePriceRepository = servicePriceRepository;
        }

        // GET: api/ServicePrice
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServicePriceDto>>> GetServicePrices()
        {
            var servicePrices = await _servicePriceRepository.GetServicePricesAsync();
            var servicePriceDtos = servicePrices.Select(servicePrice => new ServicePriceDto
            {
                ServicePriceId = servicePrice.ServicePriceId,
                ServiceType = servicePrice.ServiceType,
                Price = servicePrice.Price,
                Description = servicePrice.Description,
                Duration = servicePrice.Duration
            }).ToList();
            return Ok(servicePriceDtos);
        }

        // GET: api/ServicePrice/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServicePriceDto>> GetServicePrice(int id)
        {
            var servicePrice = await _servicePriceRepository.GetServicePriceByIdAsync(id);

            if (servicePrice == null)
            {
                return NotFound();
            }

            var servicePriceDto = new ServicePriceDto
            {
                ServicePriceId = servicePrice.ServicePriceId,
                ServiceType = servicePrice.ServiceType,
                Price = servicePrice.Price,
                Description = servicePrice.Description,
                Duration = servicePrice.Duration
            };

            return Ok(servicePriceDto);
        }

        // POST: api/ServicePrice
        [HttpPost]
        public async Task<ActionResult<ServicePriceDto>> PostServicePrice(ServicePriceCreateDto servicePriceCreateDto)
        {
            var servicePrice = new ServicePrice
            {
                ServiceType = servicePriceCreateDto.ServiceType,
                Price = servicePriceCreateDto.Price,
                Description = servicePriceCreateDto.Description,
                Duration = servicePriceCreateDto.Duration
            };

            var createdServicePrice = await _servicePriceRepository.CreateServicePriceAsync(servicePrice);
            var createdServicePriceDto = new ServicePriceDto
            {
                ServicePriceId = createdServicePrice.ServicePriceId,
                ServiceType = createdServicePrice.ServiceType,
                Price = createdServicePrice.Price,
                Description = createdServicePrice.Description,
                Duration = createdServicePrice.Duration
            };

            return CreatedAtAction(nameof(GetServicePrice), new { id = createdServicePriceDto.ServicePriceId }, createdServicePriceDto);
        }

        // PUT: api/ServicePrice/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServicePrice(int id, ServicePriceCreateDto servicePriceCreateDto)
        {
            var existingServicePrice = await _servicePriceRepository.GetServicePriceByIdAsync(id);

            if (existingServicePrice == null)
            {
                return NotFound();
            }

            existingServicePrice.ServiceType = servicePriceCreateDto.ServiceType;
            existingServicePrice.Price = servicePriceCreateDto.Price;
            existingServicePrice.Description = servicePriceCreateDto.Description;
            existingServicePrice.Duration = servicePriceCreateDto.Duration;

            var updated = await _servicePriceRepository.UpdateServicePriceAsync(existingServicePrice);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/ServicePrice/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServicePrice(int id)
        {
            var deleted = await _servicePriceRepository.DeleteServicePriceAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
