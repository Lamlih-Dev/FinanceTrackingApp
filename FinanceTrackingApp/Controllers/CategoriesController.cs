using AutoMapper;
using FinanceTrackingApp.Dto;
using FinanceTrackingApp.Interfaces;
using FinanceTrackingApp.Models;
using FinanceTrackingApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceTrackingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper) 
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Category>))]
        [ProducesResponseType(400)]
        public IActionResult GetCategories()
        {
            var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categories);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int id)
        {
            if (!_categoryRepository.CategoryExists(id))
                return NotFound();

            var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategory(id));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(category);
        }

        [HttpGet("categoryTransactions/{id}")]
        [ProducesResponseType(200, Type = typeof(ICollection<Transaction>))]
        [ProducesResponseType(400)]
        public IActionResult GetCategoryTransactions(int id)
        {
            if (!_categoryRepository.CategoryExists(id))
                return NotFound();

            var categoryTransactions = _mapper.Map<ICollection<TransactionDto>>(_categoryRepository.GetCategoryTransactions(id));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categoryTransactions);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CategoryDto newCategory)
        {
            if (newCategory == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            if (_categoryRepository.CategoryExists(newCategory.CategoryName))
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            var categoryMap = _mapper.Map<Category>(newCategory);
            if (!_categoryRepository.CreateCategory(categoryMap))
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
        public IActionResult UpdateCategory([FromBody] CategoryDto newCategory)
        {
            if (newCategory == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_categoryRepository.CategoryExists(newCategory.CategoryID))
            {
                return NotFound();
            }

            var categoryMap = _mapper.Map<Category>(newCategory);
            if (!_categoryRepository.UpdateCategory(categoryMap))
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
        public IActionResult DeleteCategory([FromBody] int categoryID)
        {
            if (!_categoryRepository.CategoryExists(categoryID))
                return NotFound();

            var category = _categoryRepository.GetCategory(categoryID);
            var categoryTransactions = _categoryRepository.GetCategoryTransactions(categoryID);

            if (categoryTransactions.Count != 0)
            {
                ModelState.AddModelError("", "This category is currently being used by one or more transactions");
                return StatusCode(400, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_categoryRepository.DeleteCategory(category))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully deleted");
        }
    }
}
