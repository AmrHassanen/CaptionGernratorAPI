using CaptionGenerator.CORE.Dtos;
using System.Threading.Tasks;

namespace CaptionGenerator.CORE.Interfaces
{
    public interface IServiceUser
    {
        Task<ServiceDto> GetServiceByIdAsync(int serviceId);

        Task<ServiceDto> CreateServiceAsync(ServiceDto serviceDto);

        Task<ServiceDto> UpdateServiceAsync(int serviceId, ServiceDto serviceDto);

        Task<bool> DeleteServiceAsync(int serviceId);
    }
}
