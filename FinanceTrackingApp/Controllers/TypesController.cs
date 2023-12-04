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
    public class TypesController : Controller
    {
        private readonly ITypeReposiroty _typeReposiroty;
        private readonly IMapper _mapper;

        public TypesController(ITypeReposiroty typeReposiroty, IMapper mapper)
        {
            _typeReposiroty = typeReposiroty;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Models.Type>))]
        public IActionResult GetTypes()
        {
            var types = _mapper.Map<List<TypeDto>>(_typeReposiroty.GetTypes());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(types);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Models.Type))]
        public IActionResult GetType(int id)
        {
            if (!_typeReposiroty.TypeExists(id))
                return NotFound();

            var type = _mapper.Map<TypeDto>(_typeReposiroty.GetType(id));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(type);
        }

        [HttpGet("typeTransactions/{id}")]
        [ProducesResponseType(200, Type = typeof(ICollection<Transaction>))]
        public IActionResult GetTypeTransations(int id)
        {
            if (!_typeReposiroty.TypeExists(id))
                return NotFound();

            var typeTransactions = _mapper.Map<ICollection<TransactionDto>>(_typeReposiroty.GetTypeTransations(id));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(typeTransactions);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateType([FromBody] TypeDto newType)
        {
            if (newType == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            if (_typeReposiroty.TypeExists(newType.TypeName))
            {
                ModelState.AddModelError("", "Type already exists");
                return StatusCode(422, ModelState);
            }

            var typeMap = _mapper.Map<Models.Type>(newType);
            if (!_typeReposiroty.CreateType(typeMap))
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
        public IActionResult UpdateType([FromBody] TypeDto newType)
        {
            if (newType == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_typeReposiroty.TypeExists(newType.TypeID))
            {
                return NotFound();
            }

            var typeMap = _mapper.Map<Models.Type>(newType);
            if (!_typeReposiroty.UpdateType(typeMap))
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
        public IActionResult DeleteType([FromBody] int typeID)
        {
            if (!_typeReposiroty.TypeExists(typeID))
                return NotFound();

            var type = _typeReposiroty.GetType(typeID);
            var typeTransactions = _typeReposiroty.GetTypeTransations(typeID);

            if (typeTransactions.Count != 0)
            {
                ModelState.AddModelError("", "This tyoe is currently being used by one or more transactions");
                return StatusCode(400, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_typeReposiroty.DeleteType(type))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully deleted");
        }
    }
}
