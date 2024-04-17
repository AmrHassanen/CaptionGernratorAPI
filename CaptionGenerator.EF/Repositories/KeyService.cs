using CaptionGenerator.CORE.Dtos;
using CaptionGenerator.CORE.Entities;
using CaptionGenerator.CORE.Interfaces;
using CaptionGenerator.EF.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Rootics.CORE.Interfaces;
using System;
using System.Threading.Tasks;
namespace CaptionGenerator.EF.Repositories
{
    public class KeyService : IkeyService
    {
        private readonly ApplicationDbContext _context;

        public KeyService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Key> CreateKeyAsync(KeyDto keyDto)
        {
            if (keyDto == null)
            {
                throw new ArgumentNullException(nameof(keyDto));
            }

            var key = new Key
            {
                Limit = keyDto.Limit,
                RateLimit = keyDto.RateLimit
            };

            _context.Keys.Add(key);
            await _context.SaveChangesAsync();

            return key;
        }

        public async Task<bool> DeleteKeyAsync(int keyId)
        {
            var key = await _context.Keys.FindAsync(keyId);

            if (key == null)
            {
                return false; // Key with the provided Id does not exist
            }

            _context.Keys.Remove(key);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Key>> GetKeysByUserIdAsync(string userId)
        {
            var keys = await _context.UserKeys
                .Where(uk => uk.ApplicationUserId == userId)
                .Select(uk => uk.Key)
                .ToListAsync();

            return keys;
        }

        public async Task<Key> UpdateKeyAsync(int keyId, KeyUpdateRequest keyUpdateRequest)
        {
            if (keyUpdateRequest == null)
            {
                throw new ArgumentNullException(nameof(keyUpdateRequest));
            }

            var key = await _context.Keys.FindAsync(keyId);

            if (key == null)
            {
                throw new ArgumentException("Key with the provided Id does not exist.", nameof(keyId));
            }

            key.Limit = keyUpdateRequest.Limit;
            key.RateLimit = keyUpdateRequest.RateLimit;

            await _context.SaveChangesAsync();

            return key;
        }
    }
}
