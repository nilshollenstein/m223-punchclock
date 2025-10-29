using M223PunchclockDotnet.Model;

namespace M223PunchclockDotnet.Service
{
    public interface ITagService
    {
        Task<Tag> AddTagAsync(Tag tag);
        Task<bool> DeleteTagById(int id);
        Task<Tag?> GetTagByIdAsync(int id);
        Task<List<Tag>> GetTagsAsync();
        Task<bool> UpdateTagAsync(int id, Tag updatedTag);
    }
}