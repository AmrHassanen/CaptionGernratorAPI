// IMemberService interface
using CaptionGenerator.CORE.Dtos;
using CaptionGenerator.CORE.Entities;
using System.Threading.Tasks;

namespace CaptionGenerator.CORE.Interfaces
{
    public interface IMemberService
    {
        Task<List<Member>> GetAllMembersAsync();
        Task<MemberDto> CreateMemberAsync(MemberDto memberDto);
        Task<MemberDto> UpdateMemberAsync(int memberId, MemberDto memberDto);
        Task<bool> DeleteMemberAsync(int memberId);
        Task<Member> GetMemberByIdAsync(int memberId);

    }
}
