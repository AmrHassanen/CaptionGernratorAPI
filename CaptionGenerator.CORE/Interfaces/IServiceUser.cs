using CaptionGenerator.CORE.Dtos;
using CaptionGenerator.CORE.Entities;
using System.Threading.Tasks;

namespace CaptionGenerator.CORE.Interfaces
{
    public interface IServiceUser
    {
        Task<Service> GetServiceByIdAsync(int serviceId);
        Task<List<Service>> GetAllServicesAsync(); // New method declaration for getting all services
        Task<Service> CreateServiceAsync(ServiceDto serviceDto);
        Task<Service> UpdateServiceAsync(int serviceId, ServiceDto serviceDto);
        Task<bool> DeleteServiceAsync(int serviceId);
    }
}
