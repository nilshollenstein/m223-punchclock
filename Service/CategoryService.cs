using M223PunchclockDotnet.Model;

namespace M223PunchclockDotnet.Service
{
    public class CategoryService
    {
        private DatabaseContext _databaseContext;

        public CategoryService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<Category>> GetCategories()
        {
            return _databaseContext.Categories.ToList();
        }
    }
}
