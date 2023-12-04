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
    public class BanksController : Controller
    {
        private readonly IBankRepository _bankRepository;
        private readonly IMapper _mapper;

        public BanksController(IBankRepository bankRepository, IMapper mapper)
        {
            _bankRepository = bankRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Bank>))]
        public IActionResult GetBanks()
        {
            var banks = _mapper.Map<List<BankDto>>(_bankRepository.GetBanks());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(banks);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Bank))]
        public IActionResult GetBank(int id)
        {
            if (!_bankRepository.BankExists(id))
                return NotFound();

            var bank = _mapper.Map<BankDto>(_bankRepository.GetBank(id));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(bank); 
        }

        [HttpGet("bankAccounts/{id}")]
        [ProducesResponseType(200, Type = typeof(ICollection<BankAccount>))]
        public IActionResult GetBankAccounts(int id)
        {
            if (!_bankRepository.BankExists(id))
                return NotFound();

            var bankAccounts = _mapper.Map<ICollection<BankAccountDto>>(_bankRepository.GetBankAccounts(id));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(bankAccounts);
        }

        [HttpGet("bankUsers/{id}")]
        [ProducesResponseType(200, Type = typeof(ICollection<User>))]
        public IActionResult GetBankUsers(int id)
        {
            if (!_bankRepository.BankExists(id))
                return NotFound();

            var bankUsers = _mapper.Map<ICollection<UserDto>>(_bankRepository.GetBankUsers(id));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(bankUsers);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateBank([FromBody] BankDto newBank)
        {
            if (newBank == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            if (_bankRepository.BankExists(newBank.BankName))
            {
                ModelState.AddModelError("", "Bank already exists");
                return StatusCode(422, ModelState);
            }

            var bankMap = _mapper.Map<Bank>(newBank);
            if (!_bankRepository.CreateBank(bankMap))
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
        public IActionResult UpdateBank([FromBody] BankDto newBank)
        {
            if (newBank == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_bankRepository.BankExists(newBank.BankID))
            {
                return NotFound();
            }

            var bankMap = _mapper.Map<Bank>(newBank);
            if (!_bankRepository.UpdateBank(bankMap))
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
        public IActionResult DeleteBank([FromBody] int bankID)
        {
            if (!_bankRepository.BankExists(bankID))
                return NotFound();

            var bank = _bankRepository.GetBank(bankID);
            var bankAccounts = _bankRepository.GetBankAccounts(bankID);

            if (bankAccounts.Count != 0)
            {
                ModelState.AddModelError("", "This bank is currently being used by one or more bank accounts");
                return StatusCode(400, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_bankRepository.DeleteBank(bank))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully deleted");
        }
    }
}
