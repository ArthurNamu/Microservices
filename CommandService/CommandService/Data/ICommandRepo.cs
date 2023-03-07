using CommandService.Models;

namespace CommandService.Data;
public interface ICommandRepo
{
    bool SaveChanges();

    //Platform related
    IEnumerable<Platform> GetPlatforms();
    void CreatePlatform(Platform platform);
    bool PlatformExists(int platformId);

    //commands
    IEnumerable<Command> GetCommandsForPlatform(int platformId);
    Command GetCommand(int platformId, int commandId);
    void CreateCommand(int PlatformId, Command command);

}
