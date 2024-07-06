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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
        {
            var customers = await _customerRepository.GetCustomersAsync();
            var customerDtos = customers.Select(customer => new CustomerDto
            {
                CustomerId = customer.CustomerId,
                AccId = customer.AccId,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                IdCard = customer.IdCard,
                Address = customer.Address,
                UnitName = customer.UnitName,
                TaxCode = customer.TaxCode,
                Acc = customer.Acc != null ? new AccountDto
                {
                    Id = customer.Acc.AccId,
                    Username = customer.Acc.Username,
                    Password = customer.Acc.Password,
                    Role = customer.Acc.Role ?? 0
                } : null
            }).ToList();
            return Ok(customerDtos);
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            var customerDto = new CustomerDto
            {
                CustomerId = customer.CustomerId,
                AccId = customer.AccId,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                IdCard = customer.IdCard,
                Address = customer.Address,
                UnitName = customer.UnitName,
                TaxCode = customer.TaxCode,
                Acc = customer.Acc != null ? new AccountDto
                {
                    Id = customer.Acc.AccId,
                    Username = customer.Acc.Username,
                    Password = customer.Acc.Password,
                    Role = customer.Acc.Role ?? 0
                } : null
            };

            return Ok(customerDto);
        }

        // POST: api/Customer
        [HttpPost]
        public async Task<ActionResult<CustomerDto>> PostCustomer(CustomerCreateDto customerCreateDto)
        {
            var customer = new Customer
            {
                AccId = customerCreateDto.AccId,
                Name = customerCreateDto.Name,
                Email = customerCreateDto.Email,
                Phone = customerCreateDto.Phone,
                IdCard = customerCreateDto.IdCard,
                Address = customerCreateDto.Address,
                UnitName = customerCreateDto.UnitName,
                TaxCode = customerCreateDto.TaxCode
            };

            var createdCustomer = await _customerRepository.CreateCustomerAsync(customer);
            var createdCustomerDto = new CustomerDto
            {
                CustomerId = createdCustomer.CustomerId,
                AccId = createdCustomer.AccId,
                Name = createdCustomer.Name,
                Email = createdCustomer.Email,
                Phone = createdCustomer.Phone,
                IdCard = createdCustomer.IdCard,
                Address = createdCustomer.Address,
                UnitName = createdCustomer.UnitName,
                TaxCode = createdCustomer.TaxCode
            };

            return CreatedAtAction(nameof(GetCustomer), new { id = createdCustomerDto.CustomerId }, createdCustomerDto);
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, CustomerCreateDto customerCreateDto)
        {
            var existingCustomer = await _customerRepository.GetCustomerByIdAsync(id);

            if (existingCustomer == null)
            {
                return NotFound();
            }

            existingCustomer.AccId = customerCreateDto.AccId;
            existingCustomer.Name = customerCreateDto.Name;
            existingCustomer.Email = customerCreateDto.Email;
            existingCustomer.Phone = customerCreateDto.Phone;
            existingCustomer.IdCard = customerCreateDto.IdCard;
            existingCustomer.Address = customerCreateDto.Address;
            existingCustomer.UnitName = customerCreateDto.UnitName;
            existingCustomer.TaxCode = customerCreateDto.TaxCode;

            var updated = await _customerRepository.UpdateCustomerAsync(existingCustomer);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var deleted = await _customerRepository.DeleteCustomerAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
