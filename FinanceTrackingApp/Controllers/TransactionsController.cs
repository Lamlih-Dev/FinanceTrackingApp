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
    public class TransactionsController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public TransactionsController(ITransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Transaction>))]
        public IActionResult GetTransactions()
        {
            var transactions = _mapper.Map<List<TransactionDto>>(_transactionRepository.GetTransactions());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(transactions);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Transaction))]
        public IActionResult GetTransaction(int id)
        {
            if (!_transactionRepository.TransactionExists(id))
                return NotFound();

            var transaction = _mapper.Map<TransactionDto>(_transactionRepository.GetTransaction(id));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(transaction);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateTransaction ([FromBody] TransactionDto newTransaction, int CategoryID, int TypeID, int UserID)
        {
            if (newTransaction == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            if (_transactionRepository.TransactionExists(newTransaction.TransactionName))
            {
                ModelState.AddModelError("", "Transaction already exists");
                return StatusCode(422, ModelState);
            }

            var transactionMap = _mapper.Map<Transaction>(newTransaction);
            if (!_transactionRepository.CreateTransaction(transactionMap, CategoryID, TypeID, UserID))
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
        public IActionResult UpdateTransaction([FromBody] TransactionDto newTransaction, int CategoryID, int TypeID, int UserID)
        {
            if (newTransaction == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_transactionRepository.TransactionExists(newTransaction.TransactionID))
            {
                return NotFound();
            }

            var transactionMap = _mapper.Map<Transaction>(newTransaction);
            if (!_transactionRepository.UpdateTransaction(transactionMap, CategoryID, TypeID, UserID))
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
        public IActionResult DeleteTransaction([FromBody] int transactionID)
        {
            if (!_transactionRepository.TransactionExists(transactionID))
                return NotFound();

            var transaction = _transactionRepository.GetTransaction(transactionID);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_transactionRepository.DeleteTransaction(transaction))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully deleted");
        }
    }
}
