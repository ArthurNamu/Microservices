using PlatformService.Models;

namespace PlatformService.Data;

public class PlatformRepo : IPlatformRepo
{
    private readonly AppDBContext _context;

    public PlatformRepo(AppDBContext context)
    {
        _context = context;
    }
    public void CreatePlatform()
    {
        throw new NotImplementedException();
    }

    public Platform GetPlatformById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Platform> GetAllPlatforms()
    {
        throw new NotImplementedException();
    }

    public bool SaveChanges()
    {
        return (_context.SaveChanges() >= 0);
    }
}
