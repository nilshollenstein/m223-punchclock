using M223PunchclockDotnet.Model;

namespace M223PunchclockDotnet.Service
{
    public interface IEntryService
    {
        Task<Entry> AddEntry(Entry entry);
        Task<Entry?> DeleteEntry(int id);
        Task<List<Entry>> FindAll();
        Task<Entry?> GetEntryById(int id);
        Task<Entry> UpdateEntry(Entry entry);
    }
}