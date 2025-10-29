using M223PunchclockDotnet.Model;

namespace M223PunchclockDotnet.Service
{
    public interface ICategoryService
    {
        Task<Category> AddCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(int id);
        Task<List<Category>> GetCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(int id);
        Task<bool> UpdateCategoryAsync(int id, Category updatedCategory);
    }
}