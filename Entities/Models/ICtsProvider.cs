using System.Threading.Tasks;

namespace Entities.Models
{
    public interface ICtsProvider
    {
        Task<ICtsResponse> GetCtsResponseAsync();
    }
}
