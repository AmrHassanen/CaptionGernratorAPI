using CaptionGenerator.CORE.Dtos;
using CaptionGenerator.CORE.Entities;
using CaptionGenerator.CORE.Interfaces;
using CaptionGenerator.EF.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CaptionGenerator.EF.Services
{
    public class MemberService : IMemberService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagePath;

        public MemberService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _webHostEnvironment = webHostEnvironment;
            _imagePath = $"{_webHostEnvironment.WebRootPath}/assets/images/members";
        }

        public async Task<MemberDto> CreateMemberAsync(MemberDto memberDto)
        {
            if (memberDto == null)
            {
                throw new ArgumentNullException(nameof(memberDto));
            }

            // Validate TeamId
            var teamExists = await _context.Teams.AnyAsync(t => t.Id == memberDto.TeamId);
            if (!teamExists)
            {
                throw new ArgumentException("Team with the provided TeamId does not exist.", nameof(memberDto.TeamId));
            }

            var imageMemberName = await SaveCover(memberDto.ImageUrl);
            var backGroundImageUrl = await SaveCover(memberDto.BackgroundImageUrl);

            var member = new Member
            {
                ImageUrl = imageMemberName,
                BackgroundImageUrl = backGroundImageUrl,
                Email = memberDto.Email,
                TeamId = memberDto.TeamId
            };

            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            return memberDto;
        }

        public async Task<MemberDto> UpdateMemberAsync(int memberId, MemberDto memberDto)
        {
            if (memberDto == null)
            {
                throw new ArgumentNullException(nameof(memberDto));
            }

            var existingMember = await _context.Members.FindAsync(memberId);

            if (existingMember == null)
            {
                throw new ArgumentException("Member with the provided Id does not exist.", nameof(memberId));
            }

            // Validate TeamId if provided
            if (memberDto.TeamId != 0)
            {
                var teamExists = await _context.Teams.AnyAsync(t => t.Id == memberDto.TeamId);
                if (!teamExists)
                {
                    throw new ArgumentException("Team with the provided TeamId does not exist.", nameof(memberDto.TeamId));
                }
            }

            var hasNewImageUrl = memberDto.ImageUrl is not null; 
            var hasBackgroundImageUrl = memberDto.BackgroundImageUrl is not null; 

            
            existingMember.Email = memberDto.Email;
            existingMember.TeamId = memberDto.TeamId;

            if (hasNewImageUrl)
            {
                existingMember.ImageUrl = await SaveCover(memberDto.ImageUrl!);
            }

            if (hasBackgroundImageUrl)
            {
                existingMember.BackgroundImageUrl = await SaveCover(memberDto.BackgroundImageUrl!);
            }

            if (memberDto.TeamId != 0)
            {
                existingMember.TeamId = memberDto.TeamId;
            }

            await _context.SaveChangesAsync();

            return memberDto;
        }

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
                .FirstOrDefaultAsync(m => m.Id == memberId);

            if (member == null)
            {
                return null; // Member with the provided Id does not exist
            }      
            return member;
        }

        private async Task<string> SaveCover(IFormFile file)
        {
            var imageUrlName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var imageUrlPath = Path.Combine(_imagePath, imageUrlName);
            using var imageUrlStream = File.Create(imageUrlPath);
            await file.CopyToAsync(imageUrlStream);
            return imageUrlName;
        }

        public async Task<List<Member>> GetAllMembersAsync()
        {
            var members = await _context.Members.ToListAsync();
            return members;
        }
    }
}