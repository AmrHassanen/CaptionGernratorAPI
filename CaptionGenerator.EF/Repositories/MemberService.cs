// MemberService implementation
using CaptionGenerator.CORE.Dtos;
using CaptionGenerator.CORE.Entities;
using CaptionGenerator.CORE.Interfaces;
using CaptionGenerator.EF.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CaptionGenerator.EF.Services
{
    public class MemberService : IMemberService
    {
        private readonly ApplicationDbContext _context;

        public MemberService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
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

            var member = new Member
            {
                ImageUrl = memberDto.ImageUrl,
                BackgroundImageUrl = memberDto.BackgroundImageUrl,
                Email = memberDto.Email,
                TeamId = memberDto.TeamId
            };

            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            // Update the MemberDto with the generated Id
            

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

            existingMember.ImageUrl = memberDto.ImageUrl;
            existingMember.BackgroundImageUrl = memberDto.BackgroundImageUrl;
            existingMember.Email = memberDto.Email;


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

        public async Task<MemberDto> GetMemberByIdAsync(int memberId)
        {
            var member = await _context.Members
                .FirstOrDefaultAsync(m => m.Id == memberId);

            if (member == null)
            {
                return null; // Member with the provided Id does not exist
            }

            var memberDto = new MemberDto
            {
                ImageUrl = member.ImageUrl,
                BackgroundImageUrl = member.BackgroundImageUrl,
                Email = member.Email,
                TeamId = member.TeamId
            };

            return memberDto;
        }
    }
}
