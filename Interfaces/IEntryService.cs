using M223PunchclockDotnet.Model;

namespace M223PunchclockDotnet.Service
{
    public interface IEntryService
    {
        Task<Entry> AddEntry(Entry entry);
        Task<Entry> DeleteEntry(Entry entry);
        Task<List<Entry>> FindAll();
        Task<Entry?> GetEntryById(int id);
        Task<Entry> UpdateEntry(Entry entry);
    }
}