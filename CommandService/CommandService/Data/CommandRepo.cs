using CommandService.Models;

namespace CommandService.Data;
public class CommandRepo : ICommandRepo
{
    private readonly AppDbContext _context;

    public CommandRepo(AppDbContext context)
    {
        _context = context;
    }
    public void CreateCommand(int PlatformId, Command command)
    {
        if(command == null)
        {
            throw new ArgumentNullException(nameof(command));
        }
        command.PlatformId = PlatformId;
        _context.Command.Add(command);

    }

    public void CreatePlatform(Platform platform)
    {
        if(platform == null)
        {
            throw new ArgumentNullException(nameof(platform));
        }

        _context.Platforms.Add(platform);
    }

    public Command GetCommand(int platformId, int commandId)
    {
        return _context.Command
            .Where(c => c.Id == commandId && c.PlatformId == platformId).FirstOrDefault();
    }

    public IEnumerable<Command> GetCommandsForPlatform(int platformId)
    {
        return _context.Command
            .Where(c => c.PlatformId == platformId)
            .OrderBy(c => c.Platform.Name);
    }

    public IEnumerable<Platform> GetPlatforms()
    {
        return _context.Platforms.ToList();
    }

    public bool PlatformExists(int platformId)
    {
        return _context.Platforms.Any(p => p.Id == platformId);
    }

    public bool SaveChanges()
    {
        return (_context.SaveChanges()  >= 0);
    }
}
