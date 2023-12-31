﻿using FinanceTrackingApp.Models;

namespace FinanceTrackingApp.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int id);
        ICollection<Transaction> GetCategoryTransactions(int id);
        bool CategoryExists(string name);
        bool CategoryExists(int id);
        bool CreateCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(Category category);
        bool Save();
    }
}
