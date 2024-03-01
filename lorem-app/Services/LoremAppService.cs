using lorem_app.Models;
using Microsoft.EntityFrameworkCore;
using Exception = System.Exception;

namespace lorem_app.Services;

public class LoremAppService : ILoremAppService
{
    private readonly DbContext _dbContext;

    public LoremAppService(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Lorem?> GetById(int id)
    {
        return await _dbContext.Set<Lorem>().FindAsync(id);
    }

    public async Task CreateAsync(Lorem lorem)
    {
        await _dbContext.Set<Lorem>().AddAsync(lorem);
        await _dbContext.SaveChangesAsync();
    }


    public async Task<Lorem> UpdateAsync(int id, string firstName, string lastName)
    {
        var lorem = await _dbContext.Set<Lorem>().FindAsync(id);

        if (lorem == null)
        {
            throw new Exception("lorem not found");
        }

        lorem.FirstName = firstName;
        lorem.LastName = lastName;
        _dbContext.Set<Lorem>().Update(lorem);
        await _dbContext.SaveChangesAsync();
        return lorem;
    }


    public async Task<int> DeleteAsync(Lorem lorem)
    {
        _dbContext.Set<Lorem>().Remove(lorem);
        return await _dbContext.SaveChangesAsync();
    }
}