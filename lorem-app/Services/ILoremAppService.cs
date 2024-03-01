using lorem_app.Models;

namespace lorem_app.Services;

public interface ILoremAppService
{
    Task<Lorem?> GetById(int id);
    Task CreateAsync(Lorem lorem);
    Task<Lorem> UpdateAsync(int id, string firstName, string lastName);
    Task<int> DeleteAsync(Lorem lorem);
}