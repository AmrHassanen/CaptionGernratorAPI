using CaptionGenerator.CORE.Dtos;
using CaptionGenerator.CORE.Entities;
using CaptionGenerator.CORE.Interfaces;
using CaptionGenerator.EF.Data;
using Microsoft.EntityFrameworkCore;
using Rootics.CORE.Interfaces;
using System;
using System.Threading.Tasks;

namespace CaptionGenerator.EF.Services
{
    public class MemberService : IMemberService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPhotoService _photoService;

        public MemberService(ApplicationDbContext context, IPhotoService photoService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _photoService = photoService ?? throw new ArgumentNullException(nameof(photoService));
        }

        public async Task<MemberDto> CreateMemberAsync(MemberDto memberDto)
        {
            // Validate memberDto and other checks...

            var imageUploadResult = await _photoService.AddPhotoAsync(memberDto.ImageUrl!);
            var backgroundImageUploadResult = await _photoService.AddPhotoAsync(memberDto.BackgroundImageUrl!);

            var imageMemberName = imageUploadResult.SecureUrl.AbsoluteUri; // Assuming PublicId is used as the image name
            var backGroundImageUrl = backgroundImageUploadResult.SecureUrl.AbsoluteUri;

            var member = new Member
            {
                ImageUrl = imageMemberName,
                BackgroundImageUrl = backGroundImageUrl,
                Email = memberDto.Email,
                TeamId = memberDto.TeamId,
                Name = memberDto.Name,
                Links = memberDto.Links,
                Description = memberDto.Description
                
            };

            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            return memberDto;
        }

        public async Task<MemberDto> UpdateMemberAsync(int memberId, MemberDto memberDto)
        {
            // Validate memberDto, existing member, and other checks...

            var existingMember = await _context.Members.FindAsync(memberId);
            if (existingMember == null)
            {
                throw new ArgumentException("Member with the provided Id does not exist.", nameof(memberId));
            }

            var hasNewImageUrl = memberDto.ImageUrl is not null;
            var hasBackgroundImageUrl = memberDto.BackgroundImageUrl is not null;

            if (hasNewImageUrl)
            {
                var imageUploadResult = await _photoService.AddPhotoAsync(memberDto.ImageUrl);
                existingMember.ImageUrl = imageUploadResult.SecureUrl.AbsoluteUri;
            }

            if (hasBackgroundImageUrl)
            {
                var backgroundImageUploadResult = await _photoService.AddPhotoAsync(memberDto.BackgroundImageUrl);
                existingMember.BackgroundImageUrl = backgroundImageUploadResult.SecureUrl.AbsoluteUri;
            }

            existingMember.Email = memberDto.Email;
            existingMember.TeamId = memberDto.TeamId;
            existingMember.Links= memberDto.Links;
            existingMember.Name = memberDto.Name;
            existingMember.Description = memberDto.Description;

            await _context.SaveChangesAsync();

            return memberDto;
        }

        // Implement other methods like DeleteMemberAsync, GetMemberByIdAsync, and GetAllMembersAsync...
        public async Task<bool> DeleteMemberAsync(int memberId)
        {
            var member = await _context.Members.FindAsync(memberId);

            if (member == null)
            {
                return false; // Member with the provided Id does not exist
            }

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Member> GetMemberByIdAsync(int memberId)
        {
            var member = await _context.Members
                .Include(m => m.Team) // Include the Team navigation property
                .FirstOrDefaultAsync(m => m.Id == memberId);

            return member;
        }

        public async Task<List<Member>> GetAllMembersAsync()
        {
            var members = await _context.Members
                .Include(m => m.Team) // Include the Team navigation property
                .ToListAsync();

            return members;
        }
        public async Task<bool> MemberExistsAsync(int memberId)
        {
            var member = await _context.Members.FindAsync(memberId);
            return member != null;
        }

    }
}
