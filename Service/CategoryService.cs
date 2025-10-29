using M223PunchclockDotnet.Model;
using Microsoft.EntityFrameworkCore;

namespace M223PunchclockDotnet.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly DatabaseContext _databaseContext;

        public CategoryService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _databaseContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _databaseContext.Categories.FindAsync(id);
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            _databaseContext.Categories.Add(category);
            await _databaseContext.SaveChangesAsync();
            return category;
        }

        public async Task<bool> UpdateCategoryAsync(int id, Category updatedCategory)
        {
            var existing = await _databaseContext.Categories.FindAsync(id);
            if (existing == null)
                return false;

            _databaseContext.Entry(existing).CurrentValues.SetValues(updatedCategory);

            existing.Id = id;

            await _databaseContext.SaveChangesAsync();
            return true;
        }

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
