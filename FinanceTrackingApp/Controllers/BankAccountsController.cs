using AutoMapper;
using FinanceTrackingApp.Dto;
using FinanceTrackingApp.Interfaces;
using FinanceTrackingApp.Models;
using FinanceTrackingApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTrackingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountsController : Controller
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IMapper _mapper;

        public BankAccountsController(IBankAccountRepository bankAccountRepository, IMapper mapper)
        {
            _bankAccountRepository = bankAccountRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<BankAccount>))]
        public IActionResult GetBankAccounts()
        {
            var bankAccounts = _mapper.Map<List<BankAccountDto>>(_bankAccountRepository.GetBankAccounts());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(bankAccounts);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(BankAccount))]
        public IActionResult GetBankAccount(int id)
        {
            if (!_bankAccountRepository.BankAccountExists(id))
                return NotFound();

            var bankAccount = _mapper.Map<BankAccountDto>(_bankAccountRepository.GetBankAccount(id));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(bankAccount);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateBankAccount([FromBody] BankAccountDto newBankAccount)
        {
            if (newBankAccount == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var bankAccountMap = _mapper.Map<BankAccount>(newBankAccount);
            if (!_bankAccountRepository.CreateBankAccount(bankAccountMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateBankAccount([FromBody] BankAccountDto newBankAccount)
        {
            if (newBankAccount == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_bankAccountRepository.BankAccountExists(newBankAccount.BankAccountID))
            {
                return NotFound();
            }

            var bankAccountMap = _mapper.Map<BankAccount>(newBankAccount);
            if (!_bankAccountRepository.UpdateBankAccount(bankAccountMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully updated");
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteBankAccount([FromBody] int bankAccountID)
        {
            if (!_bankAccountRepository.BankAccountExists(bankAccountID))
                return NotFound();

            var bankAccount = _bankAccountRepository.GetBankAccount(bankAccountID);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_bankAccountRepository.DeleteBankAccount(bankAccount))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully deleted");
        }
    }
}
