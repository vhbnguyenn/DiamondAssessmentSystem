using Microsoft.AspNetCore.Mvc;
using DiamondAssessmentSystem.DTOs;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using DiamondAssessment.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DiamondAssessmentSystem.BusinessObject.DTO;
using DiamondAssessmentSystem.Models;

namespace DiamondAssessmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _configuration;
        private readonly DiamondAssessmentSystemContext _context; // Add DbContext field

        public AccountController(IAccountRepository accountRepository, IConfiguration configuration, DiamondAssessmentSystemContext context)
        {
            _accountRepository = accountRepository;
            _configuration = configuration;
            _context = context;
        }

        // POST: api/Account/RegisterCustomer
        [HttpPost("RegisterCustomer")]
        public async Task<ActionResult<AccountDto>> RegisterCustomer(RegisterDto registerDto)
        {
            if (await _accountRepository.UserExistsAsync(registerDto.Username))
            {
                return BadRequest("Username already exists.");
            }

            var account = new Account
            {
                Username = registerDto.Username,
                Password = registerDto.Password,
                Role = 1 // Role for customer
            };

            var createdAccount = await _accountRepository.RegisterAsync(account);

            var accountDto = new AccountDto
            {
                Id = createdAccount.AccId, // Map the id property
                Username = createdAccount.Username,
                Password = createdAccount.Password,
                Role = createdAccount.Role ?? 0 // Handle nullable to non-nullable conversion properly
            };

            return CreatedAtAction(nameof(RegisterCustomer), accountDto);
        }

        // POST: api/Account/RegisterAdmin
        [HttpPost("RegisterAdmin")]
        public async Task<ActionResult<AccountDto>> RegisterAdmin(AccountDto registerDto)
        {
            if (await _accountRepository.UserExistsAsync(registerDto.Username))
            {
                return BadRequest("Username already exists.");
            }

            var account = new Account
            {
                Username = registerDto.Username,
                Password = registerDto.Password,
                Role = registerDto.Role // Ensure the Role is assigned correctly
            };

            var createdAccount = await _accountRepository.RegisterAsync(account);

            var accountDto = new AccountDto
            {
                Id = createdAccount.AccId, // Map the id property
                Username = createdAccount.Username,
                Password = createdAccount.Password, // Usually, you wouldn't return the password in the response
                Role = createdAccount.Role ?? 0 // Handle nullable to non-nullable conversion properly
            };

            return CreatedAtAction(nameof(RegisterAdmin), accountDto);
        }

        // POST: api/Account/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (loginDto == null)
            {
                return BadRequest("Invalid login request.");
            }

            var account = await _accountRepository.LoginAsync(loginDto.Username, loginDto.Password);

            if (account == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            string token = GenerateJwtToken(account);

            if (account.Role == 1)
            {
                var customer = await _context.Customers
                                             .Include(c => c.Acc)
                                             .FirstOrDefaultAsync(c => c.AccId == account.AccId);

                if (customer == null)
                {
                    return Unauthorized("Customer not found for the given account.");
                }

                var customerDto = new CustomerDto
                {
                    CustomerId = customer.CustomerId,
                    Name = customer.Name,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    IdCard = customer.IdCard,
                    Address = customer.Address,
                    UnitName = customer.UnitName,
                    TaxCode = customer.TaxCode,
                    AccId = customer.AccId,
                    Acc = new AccountDto
                    {
                        Id = customer.Acc.AccId,
                        Username = customer.Acc.Username,
                        Password = customer.Acc.Password, // Usually, you wouldn't return the password in the response
                        Role = customer.Acc.Role ?? 0
                    }
                };

                var loginResponse = new LoginResponseDto
                {
                    Customer = customerDto,
                    Token = token
                };

                return Ok(loginResponse);
            }
            else
            {
                var staff = await _context.Staff
                                          .Include(s => s.Acc)
                                          .FirstOrDefaultAsync(s => s.AccId == account.AccId);

                if (staff == null)
                {
                    return Unauthorized("Staff not found for the given account.");
                }

                var staffDto = new StaffDto
                {
                    StaffId = staff.StaffId,
                    AccId = staff.AccId,
                    Name = staff.Name,
                    Email = staff.Email,
                    Phone = staff.Phone
                };

                var loginResponse = new
                {
                    Staff = staffDto,
                    Token = token
                };

                return Ok(loginResponse);
            }
        }




        // GET: api/Account
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountDto>>> GetAccounts()
        {
            var accounts = await _accountRepository.GetAccountsAsync();

            var accountDtos = accounts.Select(a => new AccountDto
            {
                Id = a.AccId, // Map the id property
                Username = a.Username,
                Password = a.Password, // Usually, you wouldn't return the password in the response
                Role = a.Role ?? 0 // Handle nullable to non-nullable conversion properly
            }).ToList();

            return Ok(accountDtos);
        }

        // GET: api/Account/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDto>> GetAccount(int id)
        {
            var account = await _accountRepository.GetAccountByIdAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            var accountDto = new AccountDto
            {
                Id = account.AccId, // Map the id property
                Username = account.Username,
                Password = account.Password, // Usually, you wouldn't return the password in the response
                Role = account.Role ?? 0 // Handle nullable to non-nullable conversion properly
            };

            return Ok(accountDto);
        }

        // PUT: api/Account/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, AccountDto accountDto)
        {
            if (id != accountDto.Id)
            {
                return BadRequest();
            }

            var account = new Account
            {
                AccId = id,
                Username = accountDto.Username,
                Password = accountDto.Password,
                Role = accountDto.Role
            };

            var updated = await _accountRepository.UpdateAccountAsync(account);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Account/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var deleted = await _accountRepository.DeleteAccountAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        private string GenerateJwtToken(Account account)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, account.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
