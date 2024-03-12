// IEndpointService interface
using CaptionGenerator.CORE.Dtos;
using System.Threading.Tasks;

namespace CaptionGenerator.CORE.Interfaces
{
    public interface IEndpointService
    {
        Task<EndPointDto> CreateEndpointAsync(EndPointDto EndPointDto);
        Task<EndPointDto> UpdateEndpointAsync(int endpointId, EndPointDto EndPointDto);
        Task<bool> DeleteEndpointAsync(int endpointId);
        Task<EndPointDto> GetEndpointByIdAsync(int endpointId);
    }
}
