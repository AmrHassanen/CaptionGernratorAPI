using CaptionGenerator.CORE.Dtos;
using CaptionGenerator.CORE.Entities;
using System.Threading.Tasks;

namespace CaptionGenerator.CORE.Interfaces
{
    public interface IServiceUser
    {
        Task<Service> GetServiceByIdAsync(int serviceId);

        Task<ServiceDto> CreateServiceAsync(ServiceDto serviceDto);

        Task<ServiceDto> UpdateServiceAsync(int serviceId, ServiceDto serviceDto);

        Task<bool> DeleteServiceAsync(int serviceId);
    }
}
