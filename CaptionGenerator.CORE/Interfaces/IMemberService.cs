// IMemberService interface
using CaptionGenerator.CORE.Dtos;
using System.Threading.Tasks;

namespace CaptionGenerator.CORE.Interfaces
{
    public interface IMemberService
    {
        Task<MemberDto> CreateMemberAsync(MemberDto memberDto);
        Task<MemberDto> UpdateMemberAsync(int memberId, MemberDto memberDto);
        Task<bool> DeleteMemberAsync(int memberId);
        Task<MemberDto> GetMemberByIdAsync(int memberId);

    }
}
