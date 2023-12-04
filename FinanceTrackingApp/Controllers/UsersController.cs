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
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<User>))]
        [ProducesResponseType(400)]
        public IActionResult GetUsers()
        {
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(int id)
        {
            if (!_userRepository.UserExists(id))
                return NotFound();

            var user = _mapper.Map<UserDto>(_userRepository.GetUser(id));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        [HttpGet("userTransactions/{id}")]
        [ProducesResponseType(200, Type = typeof(ICollection<Transaction>))]
        [ProducesResponseType(400)]
        public IActionResult GetUserTransactions(int id)
        {
            if (!_userRepository.UserExists(id))
                return NotFound();

            var userTransactions = _mapper.Map<ICollection<TransactionDto>>(_userRepository.GetUserTransactions(id));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(userTransactions);
        }

        [HttpGet("userBanks/{id}")]
        [ProducesResponseType(200, Type = typeof(ICollection<Bank>))]
        [ProducesResponseType(400)]
        public IActionResult GetUserBanks(int id)
        {
            if (!_userRepository.UserExists(id))
                return NotFound();

            var userBanks = _mapper.Map<ICollection<BankDto>>(_userRepository.GetUserBanks(id));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(userBanks);
        }

        [HttpGet("userBankAccounts/{id}")]
        [ProducesResponseType(200, Type = typeof(ICollection<BankAccount>))]
        [ProducesResponseType(400)]
        public IActionResult GetUserBankAccounts(int id)
        {
            if (!_userRepository.UserExists(id))
                return NotFound();

            var userBankAccounts = _mapper.Map<ICollection<BankAccountDto>>(_userRepository.GetUserBankAccounts(id));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(userBankAccounts);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] UserDto newUser)
        {
            if (newUser == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            if(_userRepository.UserExists(newUser.Username))
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(422, ModelState);
            }

            var userMap = _mapper.Map<User>(newUser);
            if (!_userRepository.CreateUser(userMap))
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
        public IActionResult UpdateUser([FromBody] UserDto newUser)
        {
            if (newUser == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userRepository.UserExists(newUser.UserID))
            {
                return NotFound();
            }

            var userMap = _mapper.Map<User>(newUser);
            if (!_userRepository.UpdateUser(userMap))
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
        public IActionResult DeleteUser([FromBody] int userID)
        {
            if (!_userRepository.UserExists(userID))
                return NotFound();

            var user = _userRepository.GetUser(userID);
            var userTransactions = _userRepository.GetUserTransactions(userID);
            var userBankAccounts = _userRepository.GetUserBankAccounts(userID);

            if (userTransactions.Count != 0)
            {
                ModelState.AddModelError("", "This user is currently being used by one or more transactions");
                return StatusCode(400, ModelState);
            }

            if (userBankAccounts.Count != 0)
            {
                ModelState.AddModelError("", "This user is currently being used by one or more bank accounts");
                return StatusCode(400, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userRepository.DeleteUser(user))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully deleted");
        }
    }
}
