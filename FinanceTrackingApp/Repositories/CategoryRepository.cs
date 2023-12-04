using FinanceTrackingApp.Data;
using FinanceTrackingApp.Interfaces;
using FinanceTrackingApp.Models;

namespace FinanceTrackingApp.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public bool CategoryExists(string name)
        {
            return _context.Categories.Any(c => c.CategoryName.Trim().ToLower() == name.Trim().ToLower());
        }

        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(c => c.CategoryID == id);
        }

        public bool CreateCategory(Category newCategory)
        {
            _context.Add(newCategory);
            return Save();
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Where(c => c.CategoryID == id).FirstOrDefault();
        }

        public ICollection<Transaction> GetCategoryTransactions(int id)
        {
            return _context.Transactions.Where(t => t.Category.CategoryID == id).ToList();
        }

        public bool UpdateCategory(Category category)
        {
            _context.Update(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _context.Remove(category);
            return Save();
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }
    }
}
