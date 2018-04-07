using System.Threading.Tasks;

namespace ZaklepTo.Infrastructure.Services.Interfaces
{
    public interface IDataInitializer
    {
        Task SeedAsync();
    }
}
