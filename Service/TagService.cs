using M223PunchclockDotnet.Model;
using Microsoft.EntityFrameworkCore;

namespace M223PunchclockDotnet.Service
{
    public class TagService : ITagService
    {
        private DatabaseContext _databaseContext;

        public TagService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<Tag>> GetTagsAsync()
        {
            return await _databaseContext.Tags.ToListAsync();
        }

        public async Task<Tag?> GetTagByIdAsync(int id)
        {
            return await _databaseContext.Tags.FindAsync(id);
        }

        public async Task<Tag> AddTagAsync(Tag tag)
        {
            _databaseContext.Tags.Add(tag);
            await _databaseContext.SaveChangesAsync();
            return tag;
        }

        public async Task<bool> UpdateTagAsync(int id, Tag updatedTag)
        {
            var existing = await _databaseContext.Tags.FindAsync(id);
            if (existing == null)
                return false;

            _databaseContext.Entry(existing).CurrentValues.SetValues(updatedTag);

            existing.Id = id;

            await _databaseContext.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteTagById(int id)
        {
            var tag = await _databaseContext.Tags.FindAsync(id);
            if (tag == null)
                return false;

            _databaseContext.Tags.Remove(tag);
            await _databaseContext.SaveChangesAsync();
            return true;
        }
    }
}
