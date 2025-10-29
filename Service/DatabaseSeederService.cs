using M223PunchclockDotnet.Model;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace M223PunchclockDotnet.Service
{
    public class DatabaseSeederService : IDatabaseSeederService
    {
        private readonly DatabaseContext _databaseContext;

        public DatabaseSeederService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task SeedDb()
        {
            if (_databaseContext.Entries.Any() || _databaseContext.Categories.Any())
                return;

            await _databaseContext.Database.EnsureCreatedAsync();

            if (!_databaseContext.Categories.Any())
                await FillCategoriesAsync();

            if (!_databaseContext.Entries.Any())
                await FillEntriesAsync();

            if (!_databaseContext.Tags.Any())
                await FillTagsAsync();
        }

        private async Task FillCategoriesAsync()
        {
            var categories = new List<Category>
            {
                new Category { Title = "Development" },
                new Category { Title = "Meeting" },
                new Category { Title = "Testing" }
            };

            await _databaseContext.AddRangeAsync(categories);
            await _databaseContext.SaveChangesAsync();
        }

        private async Task FillEntriesAsync()
        {
            var categories = await _databaseContext.Categories.ToListAsync();

            var entries = new List<Entry>
            {
                new Entry
                {
                    CheckIn = DateTime.UtcNow,
                    CheckOut = DateTime.UtcNow.AddHours(4),
                    Category = categories.First(c => c.Title == "Development")
                },
                new Entry
                {
                    CheckIn = DateTime.UtcNow.AddDays(-1),
                    CheckOut = DateTime.UtcNow.AddDays(-1).AddHours(3),
                    Category = categories.First(c => c.Title == "Meeting")
                },
                new Entry
                {
                    CheckIn = DateTime.UtcNow.AddDays(-2),
                    CheckOut = DateTime.UtcNow.AddDays(-2).AddHours(6),
                    Category = categories.First(c => c.Title == "Testing")
                },new Entry
                {
                    CheckIn = DateTime.UtcNow.AddDays(+7),
                    CheckOut = DateTime.UtcNow.AddDays(+7).AddHours(6),
                    Category = categories.First(c => c.Title == "Testing")
                }
            };

            await _databaseContext.AddRangeAsync(entries);
            await _databaseContext.SaveChangesAsync();
        }

        private async Task FillTagsAsync()
        {
            var entries = await _databaseContext.Entries.ToListAsync();

            var tag1 = new Tag { Title = "Urgent" };
            var tag2 = new Tag { Title = "Review" };
            var tag3 = new Tag { Title = "Documentation" };

            // Add relationships
            entries[0].Tags.Add(tag1);
            entries[1].Tags.Add(tag2);
            entries[2].Tags.Add(tag3);
            entries[3].Tags.Add(tag3);

            await _databaseContext.AddRangeAsync(tag1, tag2, tag3);
            await _databaseContext.SaveChangesAsync();
        }
    }
}
