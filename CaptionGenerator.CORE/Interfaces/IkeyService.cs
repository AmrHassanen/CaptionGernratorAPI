using CaptionGenerator.CORE.Dtos;
using CaptionGenerator.CORE.Entities;


namespace CaptionGenerator.CORE.Interfaces
{
    public interface IkeyService
    {
        Task<List<Key>> GetKeysByUserIdAsync(string userId);
        Task<Key> CreateKeyAsync(KeyDto keyDto);
        Task<Key> UpdateKeyAsync(int keyId, KeyUpdateRequest keyUpdateRequest);
        Task<bool> DeleteKeyAsync(int keyId);
    }
}
