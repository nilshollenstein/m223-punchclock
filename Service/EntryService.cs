using M223PunchclockDotnet.Model;
using Microsoft.EntityFrameworkCore;

namespace M223PunchclockDotnet.Service
{
    public class EntryService : IEntryService
    {
        private DatabaseContext _databaseContext;

        public EntryService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public Task<List<Entry>> FindAll()
        {
            return _databaseContext.Entries.ToListAsync();
        }

        public async Task<Entry?> GetEntryById(int id)
        {
            return await _databaseContext.Entries.FindAsync(id);
        }

        public async Task<Entry> AddEntry(Entry entry)
        {
            _databaseContext.Entries.Add(entry);
            await _databaseContext.SaveChangesAsync();

            return entry;
        }
        public async Task<Entry?> DeleteEntry(int id)
        {
            var entry = await _databaseContext.Entries.FindAsync(id);
            if (entry == null)
            {
                return null;
            }

            _databaseContext.Entries.Remove(entry);
            await _databaseContext.SaveChangesAsync();

            return entry;
        }

        public async Task<Entry> UpdateEntry(Entry entry)
        {
            var updated = _databaseContext.Entries.Update(entry);
            await _databaseContext.SaveChangesAsync();
            return updated.Entity;
        }
    }
}
