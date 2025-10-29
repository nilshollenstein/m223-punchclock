using M223PunchclockDotnet.Model;
using Microsoft.EntityFrameworkCore;

namespace M223PunchclockDotnet.Service
{
    public class CategoryService
    {
        private readonly DatabaseContext _databaseContext;

        public CategoryService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        // GET all categories
        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _databaseContext.Categories.ToListAsync();
        }

        // GET one category by id
        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _databaseContext.Categories.FindAsync(id);
        }

        // POST (create new category)
        public async Task<Category> AddCategoryAsync(Category category)
        {
            _databaseContext.Categories.Add(category);
            await _databaseContext.SaveChangesAsync();
            return category;
        }

        // PUT (update existing category)
        public async Task<bool> UpdateCategoryAsync(int id, Category updatedCategory)
        {
            var existing = await _databaseContext.Categories.FindAsync(id);
            if (existing == null)
                return false;

            existing.Title = updatedCategory.Title;
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        // DELETE (remove category)
        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _databaseContext.Categories.FindAsync(id);
            if (category == null)
                return false;

            _databaseContext.Categories.Remove(category);
            await _databaseContext.SaveChangesAsync();
            return true;
        }
    }
}
